module GenerateFactories
open Controllers
open JAST
open JAPI
open APIExtensions
open GenerateBasics

///METHOD HEADERS
let GetControllerFactoryType(controller)= GetFreeType(GetControllerName(controller) + "Factory")
let GetControllerFactoryVariable(controller)= GetVariableOfType(GetControllerFactoryType(controller))
let constructMethodName(controller) = "Construct" + GetControllerVariable(controller).JType.GetStringRep()
let constructMethodVariable(controller) = GetVariable(constructMethodName(controller),GetTypeOfController(controller))
let destructMethodName(controller) = "Destruct" + GetControllerVariable(controller).JType.GetStringRep()
let destructMethodVariable(controller) = GetVariable(destructMethodName(controller),GetVoidType())
let childrenCollectionVariable(controller) = GetVariable("generated" + GetControllerName(controller),GetListTypeOf(GetTypeOfController(controller)))
let isSavedVariable() = GetVariable("savedChildren",GetBooleanType())
let savedMapping(controller) = GetVariable("savedMapping",GetMapTypeOf(GetTypeOfController(controller),GetIntType()))
let GetIdMethodVariable(controller) = GetVariable("GetId" + GetControllerName(controller),GetIntType())
let StartSavingMethodVariable(controller) = GetVariable("StartSaving" + GetControllerName(controller),GetVoidType())


let constructChildren(controller) = 
  let controllerVariable = GetControllerVariable(controller)
  let getChildConstruction(childController) = GetDeclAssignment(GetControllerVariable(childController),GetCallOnObject(GetControllerFactoryVariable(childController),constructMethodVariable(childController),[]))
  let children = GetControllerChildren(controller)
  let childrenRHVs = children |> List.map (GetControllerVariable >> GetVariableEval)
  let childrenConstructions = children |> List.map getChildConstruction
  let mContent = 
    GetCollectStatement(
      childrenConstructions @ 
      [GetDeclAssignment(controllerVariable, GetConstructorCall(controllerVariable.JType,childrenRHVs));
      ListAddTo(childrenCollectionVariable(controller),GetVariableEval(controllerVariable));
      GetReturnStatement(GetVariableEval(controllerVariable))])
  GetMethodDeclaration(constructMethodName(controller),controllerVariable.JType,[],mContent)

let destructChildren(controller : Controller) = 
  let controllerVariable = GetControllerVariable(controller)
  let getChildDestruction(childController:Controller) = 
    let getChildVariable = GetGetterCall(controllerVariable,GetControllerVariable(childController))
    GetCallOnObject(GetControllerFactoryVariable(childController),destructMethodVariable(childController),[getChildVariable])
  let children = GetControllerChildren(controller)
  let childrenDestructions: JStatement list = GetControllerChildren(controller) |> List.map (getChildDestruction >> GetRHVstatement)
  let mContent = GetCollectStatement(ListRemove(childrenCollectionVariable(controller),GetVariableEval(controllerVariable))::childrenDestructions)
  GetMethodDeclaration(destructMethodName(controller),controllerVariable.JType,[],mContent)

let GetIdOfChild(controller : Controller) = 
  let keyParameter = GetVariable("key",GetIntType())
  let mContent = GetReturnStatement(MapGet(savedMapping(controller),GetVariableEval(keyParameter)))
  GetMethodDeclaration(GetIdMethodVariable(controller).Name,GetIdMethodVariable(controller).JType,[keyParameter],mContent)

let startSaving(controller : Controller) = 
  let children = GetControllerChildren(controller)
  let childrenFactoryVariables = GetControllerChildren(controller) |> List.map GetControllerFactoryVariable
  let childSavementCalls = childrenFactoryVariables |> List.map (fun cFV -> GetRHVstatement(GetCallOnObject(cFV,StartSavingMethodVariable(controller),[])))
  let returnIfVisited = GetIfThenElseBlock(GetVariableEval(savedMapping(controller)),GetReturnStatementVoid(),GetAssignment(savedMapping(controller),GetTrue()))
  GetMethodDeclaration(StartSavingMethodVariable(controller).Name,StartSavingMethodVariable(controller).JType,[],MultipleStatement(returnIfVisited::childSavementCalls))

let GetFactoryClass(controller) =
  let controllerFactoryType = GetControllerFactoryType(controller)
  let controllerVariable = GetControllerVariable(controller)
  let childrenVariables = GetChildVariables(GetControllerChildren(controller))
  let childrenFactoryVariables = GetControllerChildren(controller) |> List.map GetControllerFactoryVariable
  let factoryConstructor =
    let basicConstructor = GetConstructorFieldInitializations(childrenFactoryVariables, controllerFactoryType)
    AppendToConstructor(basicConstructor, GetSetField(childrenCollectionVariable(controller), GetConstructorCall(childrenCollectionVariable(controller).JType,[])))
  let methods = [constructChildren(controller);destructChildren(controller);GetIdOfChild(controller);startSaving(controller)]
  let fields = [childrenCollectionVariable(controller);savedMapping(controller); isSavedVariable()] @ childrenFactoryVariables
  GetClass(controllerFactoryType,[factoryConstructor],methods,fields)

let controllerFactoryFiles = controllers |> List.map GetFactoryClass