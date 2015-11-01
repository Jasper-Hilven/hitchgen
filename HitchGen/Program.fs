open Controllers
open Properties
open JavaSyntaxParser
open System
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


let GetControllerFactoryName controller = controllerNames.Item(controller) + "Factory"

let controllerFactoryFiles = controllers |> Seq.map (fun controller -> 
  let children = ControllerChildren controller
  let controllerType = GetTypeString(controllerNames.Item(controller))
  let childrenNames = children |> Seq.map (fun c -> controllerNames.Item(c))
  let childrenFactoryNames = children |> Seq.map GetControllerFactoryName
  let childrenFieldDeclarations = GetFieldDeclarations childrenFactoryNames
  let factmethdeclaration = CreateJVariable("Create"+controllerNames.Item(controller),controllerType)
  let GetChildConstruction(childController,childControllerName) = 
    let ChildFactoryResult = GetCallOnObject(GetFieldName(GetControllerFactoryName(childController)),"Create" + childControllerName,[])
    GetDeclAssignment(CreateJVariable(GetFieldName(childControllerName),GetTypeString(childControllerName)),ChildFactoryResult)
  let ChildConstructions = children |> Seq.map(fun cc -> GetChildConstruction(cc,controllerNames.Item(cc))) |> Seq.toList
  let ConstructorParameters = children |> Seq.map( fun c -> GetJVariableOfController(c).vName) 
  let FactoryResultReturn = returnStatement(GetConstructorCall(controllerType,ConstructorParameters)) 
  let factMethodContent = ChildConstructions @ [FactoryResultReturn]
  let FactoryMethod = GetMethodDeclaration(factmethdeclaration,[],factMethodContent) 
  let childrenConstructorInitializations = GetConstructorInitializations childrenFactoryNames 
  let childrenConstructorParameters = children |> Seq.map (fun c -> GetFieldName(GetControllerFactoryName c)) |> JavaSyntaxParser.GetParameterDescriptions
  let content = JavaSyntaxParser.GetClassContentDescription(childrenConstructorParameters,childrenConstructorInitializations,childrenFieldDeclarations,FactoryMethod,GetControllerFactoryName controller)
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