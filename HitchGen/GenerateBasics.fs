module GenerateBasics

open Controllers
open JAPI

let GetTypeOfController(controller: Controller) = GetFreeType(GetControllerName(controller))

let GetFieldName(controller: Controller) = 
  let controllerName = GetControllerName(controller)
  controllerName.Substring(0,1).ToLower()+ controllerName.Substring(1)

let GetChildVariables(childControllers: Controller list) = 
  childControllers |> Seq.map (fun cc -> GetVariable(GetFieldName(cc),GetTypeOfController(cc)))

let GetConstructorInitializations(childControllers) =
  childrenNames |> Seq.map(fun childName -> GetFieldAssignment(JavaSyntaxParser.GetFieldName(childName),JavaSyntaxParser.GetFieldName(childName)))
let GetJVariableOfController(c) = {vName = GetFieldName(controllerNames.Item(c)); vType = GetTypeString(controllerNames.Item(c))}

let controllerFiles = controllers |> Seq.map (fun controller -> 
  let children = ControllerChildren controller
  let childrenNames = children |> Seq.map (fun c -> controllerNames.Item(c))
  let childrenFieldDeclarations = GetFieldDeclarations childrenNames
  let childrenConstructorInitializations = GetConstructorInitializations childrenNames 
  let childrenConstructorParameters = 
    children |> Seq.map (fun c -> GetJVariableOfController(c)) |> GetParameterDeclarationDescriptions
  let content = JavaSyntaxParser.GetClassContentDescription(childrenConstructorParameters,childrenConstructorInitializations,childrenFieldDeclarations,[],controllerNames.Item(controller))
  GetClassFile(controllerNames.Item(controller),"",content))

