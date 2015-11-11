module GenerateFactories
open Controllers
open JavaSyntaxParser
open GenerateBasics


let GetControllerFactoryTypeName controller = controllerNames.Item(controller) + "Factory"

////CHILDRENLIST
let constructedChildren = "constructedChildren"
let ConstructedItemsListDeclaration(controllerType) = GetFieldDeclaration(GetListTypeOf(controllerType),constructedChildren)
let ConstructedItemsListInitialization(controllerType) = GetFieldAssignment(constructedChildren,GetConstructorCall(GetListTypeOf(controllerType),[]))
let AddItemToConstructedItemsList(element) = GetExpressionEvaluation(GetCallOnObject(constructedChildren,"Add",[element]))
let RemoveItemFromConstructedItemsList(element) = GetExpressionEvaluation(GetCallOnObject(constructedChildren,"Remove",[element]))
////CHILDRENNUMBERCAST
let ChildToNumber = "ChildToNumber"
let GetChildNumberMapType(controllerType) = GetMapTypeOf(controllerType,GetBoxedIntType())
let ChildToNumberDeclaration(controllerType) = GetFieldDeclaration(GetChildNumberMapType(controllerType),ChildToNumber)
let ChildToNumberCreation(controllerType) = GetAssignment(ChildToNumber,GetConstructorCall(GetChildNumberMapType(controllerType),[]))
////OPTIMIZE STORING
let CountedFactoryDeclaration() = GetFieldDeclaration("boolean","countedFactory")
let StoredFactoryChildren() = GetFieldDeclaration("boolean","storedFactoryChildren")
////CONSTRUCT A CHILD

let FactoryConstructionMethod(controller, children) = 
  let controllerType = GetTypeString(controllerNames.Item(controller))
  let factMethodContent = 
    let ChildConstructions = 
      let GetChildConstruction(childController,childControllerName) = 
        let ChildFactoryResult = GetCallOnObject(GetFieldName(GetControllerFactoryTypeName(childController)),"Create" + childControllerName,[])
        GetDeclAssignment(CreateJVariable(GetFieldName(childControllerName),GetTypeString(childControllerName)),ChildFactoryResult)
      children |> Seq.map(fun cc -> GetChildConstruction(cc,controllerNames.Item(cc))) |> Seq.toList
    let BuildFactoryResult = 
      let ConstructorParameters = children |> Seq.map( fun c -> GetJVariableOfController(c).vName)
      let constructorCall = GetConstructorCall(controllerType,ConstructorParameters)
      GetVariableDeclarationAssignment(CreateJVariable("result",controllerType), constructorCall)
    let ReturnResult = returnStatement("result")      
    ChildConstructions @ [BuildFactoryResult]@ [AddItemToConstructedItemsList("result")]@[ReturnResult]
  let factmethdeclaration = CreateJVariable("Create"+controllerNames.Item(controller),controllerType)
  GetMethodDeclaration(factmethdeclaration,[],factMethodContent) 
 
////DESTRUCT A CHILD
let FactoryDestructionMethod(controller, children) = 
  let controllerType = GetTypeString(controllerNames.Item(controller))
  let factmethdeclaration = CreateJVariable("Destroy"+controllerNames.Item(controller),controllerType)
  let factMethodContent = 
    let ChildDestructions = 
      let GetChildDestruction(childController,childControllerTypeName) = 
        let childControllerFactoryName = GetFieldName(GetControllerFactoryTypeName(childController))
        let childToDestruct = GetAccessField("toDestroy",GetFieldName(childControllerTypeName))
        let ChildFactoryResult = GetCallOnObject(GetFieldName(GetControllerFactoryTypeName(childController)),"Destroy" + childControllerTypeName,[childToDestruct])
        GetExpressionEvaluation(ChildFactoryResult)
      children |> Seq.map(fun cc -> GetChildDestruction(cc,controllerNames.Item(cc))) |> Seq.toList
    ChildDestructions @ [RemoveItemFromConstructedItemsList("toDestroy")]
  GetMethodDeclaration(factmethdeclaration,[CreateJVariable("toDestroy",controllerType)],factMethodContent) 



////CHILDREN TO JSON
  ////CREATECHILDTONUMBERTOPDOWN 
let childToNumberCount(controllerType : JType, children : Controller seq) = 
  let methodDeclaration = CreateJVariable("MakeChildToNumber", GetIntType())
  let methodContent = 
    let checkBoolForDummies = GetIfThenBlock("countedFactory",[returnStatement("currentCount")])@ [GetAssignment("countedFactory","true")]
    let handleChildFactories = 
      children |> Seq.map (fun controller -> GetAssignment("currentCount", GetCallOnObject(GetFieldName(GetControllerFactoryTypeName(controller)),"MakeChildToNumber",["currentCount"]))) |> Seq.toList
    let handleOwnCreations = 
      let foreachContent = 
        [GetExpressionEvaluation( GetCallOnObject("ChildToNumber","Add",["savingElement";"currentCount"])) ;
        GetAssignment("currentCount","currentCount + 1")]
      [ChildToNumberCreation(controllerType)] @
      GetForeach(CreateJVariable("savingElement", controllerType), constructedChildren, foreachContent)
    let returnNewCurrentCount = [returnStatement("currentCount")]
    checkBoolForDummies @ handleChildFactories @ handleOwnCreations @ returnNewCurrentCount 
  GetMethodDeclaration(methodDeclaration,[CreateJVariable("currentCount",GetIntType())],methodContent)
  ////GENJSON
    ////
let JsonFromChildren(controllerType: JType, children: Controller seq) = 
  let methodDeclaration = CreateJVariable("GenerateJSon", GetStringType())
  let methodContent = 
    let checkBoolForRepetition = GetIfThenBlock("storedFactoryChildren",[returnStatement("\"[]\"")])@ [GetAssignment("storedFactoryChildren","true")]
    checkBoolForRepetition
  GetMethodDeclaration(methodDeclaration,[],methodContent)
  
  ////DESTROYCHILDTONUMBERTOPDOWN
let removeNumberCount(controllerType : JType, children : Controller seq) = 
  let methodDeclaration = CreateJVariable("RemoveChildNumberCollection",GetVoidType())
  let methodContent = 
    let resetCountedChildren = [GetAssignment("countedFactory","false");GetAssignment("StoredFactoryChildren","false")]
    let handleChildFactories = children |> Seq.map(fun controller -> GetExpressionEvaluation(GetCallOnObject(GetFieldName(GetControllerFactoryTypeName(controller)),"RemoveChildNumberCollection",[]))) |> Seq.toList
    [GetAssignment("ChildToNumber","null")] @ handleChildFactories @ resetCountedChildren
  GetMethodDeclaration(methodDeclaration,[],methodContent)

 



let controllerFactoryFiles = controllers |> Seq.map (fun controller -> 
  let controllerType = GetTypeString(controllerNames.Item(controller))
  let children = ControllerChildren controller
  let childrenNames = children |> Seq.map (fun c -> controllerNames.Item(c))
  let childrenFactoryNames = children |> Seq.map GetControllerFactoryTypeName
  let fieldDeclarations = (GetFieldDeclarations(childrenFactoryNames) |> Seq.toList) @ 
    [ConstructedItemsListDeclaration(controllerType);ChildToNumberDeclaration(controllerType);CountedFactoryDeclaration();StoredFactoryChildren()]
  let childrenConstructorInitializations = ((GetConstructorInitializations(childrenFactoryNames)) |> Seq.toList) @ 
                                           [ConstructedItemsListInitialization(controllerType)] 
  let childrenConstructorParameters = children |> Seq.map (fun c -> GetFieldName(GetControllerFactoryTypeName c)) |> JavaSyntaxParser.GetParameterDescriptions
  let methods = childToNumberCount(controllerType,children) @ [""] @
                removeNumberCount(controllerType, children)@ [""] @
                FactoryConstructionMethod(controller, children)@ [""] @
                JsonFromChildren(controllerType,children) @ [""] @
                FactoryDestructionMethod(controller, children)
  let content = JavaSyntaxParser.GetClassContentDescription(
    childrenConstructorParameters,
    childrenConstructorInitializations,
    fieldDeclarations,
    methods,
    GetControllerFactoryTypeName controller)
  GetClassFile(GetControllerFactoryTypeName(controller),"",content))
