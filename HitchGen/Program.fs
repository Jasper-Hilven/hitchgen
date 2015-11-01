open Controllers
open Properties
open JavaSyntaxParser
open System
let GetConstructorInitializations childrenNames = childrenNames |> Seq.map(
    fun childName -> JavaSyntaxParser.GetFieldAssignment(JavaSyntaxParser.GetFieldName(childName),JavaSyntaxParser.GetFieldName(childName)))

let controllerFiles = controllers |> Seq.map (fun controller -> 
  let children = ControllerChildren controller
  let childrenNames = children |> Seq.map (fun c -> controllerNames.Item(c))
  let childrenFieldDeclarations = GetFieldDeclarations childrenNames
  let childrenConstructorInitializations = GetConstructorInitializations childrenNames 
  let childrenConstructorParameters = children |> 
    Seq.map (fun c -> {vName = GetFieldName(controllerNames.Item(c)); vType = GetTypeString(controllerNames.Item(c))}) |> 
    JavaSyntaxParser.GetParameterDescriptions
  let content = JavaSyntaxParser.GetClassContentDescription(childrenConstructorParameters,childrenConstructorInitializations,childrenFieldDeclarations,controllerNames.Item(controller))
  GetClassFile(controllerNames.Item(controller),"",content))

let GetControllerFactoryName controller = controllerNames.Item(controller) + "Factory"
let controllerFactoryFiles = controllers |> Seq.map (fun controller -> 
  let children = ControllerChildren controller
  let childrenNames = children |> Seq.map GetControllerFactoryName
  let childrenFieldDeclarations = GetFieldDeclarations childrenNames
  let childrenConstructorInitializations = GetConstructorInitializations childrenNames 
  let childrenConstructorParameters = children |> 
    Seq.map (fun c -> {vName = GetFieldName(GetControllerFactoryName c); vType = GetTypeString( GetControllerFactoryName c)}) |> 
    JavaSyntaxParser.GetParameterDescriptions
  let content = JavaSyntaxParser.GetClassContentDescription(childrenConstructorParameters,childrenConstructorInitializations,childrenFieldDeclarations,GetControllerFactoryName controller)
  GetClassFile(GetControllerFactoryName(controller),"",content))

[<EntryPoint>]
let main argv = 
  let allFiles = Seq.append controllerFiles controllerFactoryFiles
  Console.WriteLine(Environment.CurrentDirectory)
  let directoryInfo = IO.Directory.CreateDirectory("GeneratedCode")
  IO.Directory.Delete(directoryInfo.FullName,true);
  IO.Directory.CreateDirectory("GeneratedCode") |> ignore
  allFiles |> Seq.iter (fun (f:ClassFile) -> 
    use fs = IO.File.CreateText("GeneratedCode\\" + f.Name)
    f.ClassContent |> Seq.iter (fun l -> fs.WriteLine(l)))
  0