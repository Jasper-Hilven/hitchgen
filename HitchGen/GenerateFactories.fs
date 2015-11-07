module GenerateFactories
open Controllers
open JavaSyntaxParser
open GenerateBasics


let GetControllerFactoryName controller = controllerNames.Item(controller) + "Factory"

////CHILDRENLIST

let ConstructedItemsListDeclaration(controllerType) = GetFieldDeclaration(GetListTypeOf(controllerType),"constructedChildren")
let ConstructedItemsListInitialization(controllerType) = GetFieldAssignment("constructedChildren",GetConstructorCall(GetListTypeOf(controllerType),[]))
let AddItemToConstructedItemsList(element) = GetExpressionEvaluation(GetCallOnObject( "constructedChildren","Add",[element]))
let RemoveItemFromConstructedItemsList(element) = GetExpressionEvaluation(GetCallOnObject( "constructedChildren","Remove",[element]))

////CONSTRUCT A CHILD

let FactoryConstructionMethod(controller, children) = 
  let controllerType = GetTypeString(controllerNames.Item(controller))
  let factMethodContent = 
    let ChildConstructions = 
      let GetChildConstruction(childController,childControllerName) = 
        let ChildFactoryResult = GetCallOnObject(GetFieldName(GetControllerFactoryName(childController)),"Create" + childControllerName,[])
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
        let childControllerFactoryName = GetFieldName(GetControllerFactoryName(childController))
        let childToDestruct = GetAccessField("toDestroy",GetFieldName(childControllerTypeName))
        let ChildFactoryResult = GetCallOnObject(GetFieldName(GetControllerFactoryName(childController)),"Destroy" + childControllerTypeName,[childToDestruct])
        GetExpressionEvaluation(ChildFactoryResult)
      children |> Seq.map(fun cc -> GetChildDestruction(cc,controllerNames.Item(cc))) |> Seq.toList
    ChildDestructions @ [RemoveItemFromConstructedItemsList("toDestroy")]
  GetMethodDeclaration(factmethdeclaration,[CreateJVariable("toDestroy",controllerType)],factMethodContent) 

////CHILDREN TO XML
 
 
 
////XML TO CHILDREN 
 



let controllerFactoryFiles = controllers |> Seq.map (fun controller -> 
  let controllerType = GetTypeString(controllerNames.Item(controller))
  let children = ControllerChildren controller
  let childrenNames = children |> Seq.map (fun c -> controllerNames.Item(c))
  let childrenFactoryNames = children |> Seq.map GetControllerFactoryName
  let fieldDeclarations = (GetFieldDeclarations(childrenFactoryNames) |> Seq.toList) @ [ConstructedItemsListDeclaration(controllerType)]
  let childrenConstructorInitializations = ((GetConstructorInitializations(childrenFactoryNames)) |> Seq.toList) @ [ConstructedItemsListInitialization(controllerType)] 
  let childrenConstructorParameters = children |> Seq.map (fun c -> GetFieldName(GetControllerFactoryName c)) |> JavaSyntaxParser.GetParameterDescriptions
  let content = JavaSyntaxParser.GetClassContentDescription(
    childrenConstructorParameters,
    childrenConstructorInitializations,
    fieldDeclarations,
    FactoryConstructionMethod(controller, children) @[""] @ FactoryDestructionMethod(controller, children),
    GetControllerFactoryName controller)
  GetClassFile(GetControllerFactoryName(controller),"",content))
