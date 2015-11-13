module GenerateBasics

open Controllers
open JAPI

let GetTypeOfController(controller: Controller) = GetFreeType(GetControllerName(controller))

let GetFieldNameType(fieldType: JAST.JType) = 
  let fieldTypeName = fieldType.GetStringRep()
  fieldTypeName.Substring(0,1).ToLower()+ fieldTypeName.Substring(1)
let GetVariableOfType(jType) = GetVariable(GetFieldNameType(jType),jType)

let GetControllerVariable(controller) = GetVariableOfType(GetTypeOfController(controller))

let GetChildVariables(childControllers: Controller list) = 
  childControllers |> List.map GetControllerVariable

let GetConstructorViaFieldInitializations(fieldsToInitialize,constructorType) =
  let content = fieldsToInitialize |> List.map(fun o -> GetSetField(o,GetVariableEval(o))) |> GetCollectStatement
  GetConstructorDeclaration(constructorType,fieldsToInitialize,content)

let GetControllerOfClass(controller) = 
  let controllerType = GetTypeOfController(controller)
  let childVariables = GetChildVariables(Controllers.GetControllerChildren(controller))
  let constructorMethod = GetConstructorViaFieldInitializations(childVariables,controllerType)
  GetClass(controllerType,[constructorMethod],[],childVariables)

let controllerFiles = controllers |> List.map GetControllerOfClass

