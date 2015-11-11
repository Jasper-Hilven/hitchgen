module GenerateBasics
open Controllers
open JavaSyntaxParser


let GetConstructorInitializations childrenNames = childrenNames |> Seq.map(
    fun childName -> JavaSyntaxParser.GetFieldAssignment(JavaSyntaxParser.GetFieldName(childName),JavaSyntaxParser.GetFieldName(childName)))
let GetJVariableOfController(c) = {vName = GetFieldName(controllerNames.Item(c)); vType = GetTypeString(controllerNames.Item(c))}

let controllerFiles = controllers |> Seq.map (fun controller -> 
  let children = ControllerChildren controller
  let childrenNames = children |> Seq.map (fun c -> controllerNames.Item(c))
  let childrenFieldDeclarations = GetFieldDeclarations childrenNames
  let childrenConstructorInitializations = GetConstructorInitializations childrenNames 
  let childrenConstructorParameters = children |> 
    Seq.map (fun c -> GetJVariableOfController(c)) |> GetParameterDeclarationDescriptions
  let content = JavaSyntaxParser.GetClassContentDescription(childrenConstructorParameters,childrenConstructorInitializations,childrenFieldDeclarations,[],controllerNames.Item(controller))
  GetClassFile(controllerNames.Item(controller),"",content))

