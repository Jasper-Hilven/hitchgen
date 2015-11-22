module GenerateBasics
(*
open Controllers
open JAPI
open APIExtensions
let GetNameSpaceOfController(controller :Controller) = "com.jasperhilven.controllers"
let GetTypeOfController(controller: Controller) = GetFreeType(GetControllerName(controller), GetNameSpaceOfController(controller))

let GetControllerVariable(controller) = GetVariableOfType(GetTypeOfController(controller))

let GetChildVariables(childControllers: Controller list) = 
  childControllers |> List.map GetControllerVariable

let GetControllerOfClass(controller) = 
  let controllerType = GetTypeOfController(controller)
  let childVariables = GetChildVariables(Controllers.GetControllerChildren(controller))
  let constructorMethod = GetConstructorFieldInitializations(childVariables,controllerType)
  let getters = childVariables |> List.map GetGetter
  GetClass(controllerType,[constructorMethod],getters,childVariables)

let controllerFiles = controllers |> List.map GetControllerOfClass

*)