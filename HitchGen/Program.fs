open GenerateBasics
open GenerateFactories
open System
open LanguageInterface.ImplementationInterface
open LanguageInterface.API
open JavaImplementation.JAPI
open ControllerDefinitions
type UType = ILJava
  
[<EntryPoint>]
let main argv = 

  let apiProvider = new APIProvider<UType>(new JavaImplementation.JAPI.JProvider() :> ITokenProvider<UType>)
  let classPrinter = new JPrinter() :> IClassPrinter<UType>
  
  let controllers = ControllerDefinitions.GetActiveControllers |> List.map (fun c -> new LController<UType>(c, apiProvider))

  let controllerClasses = controllers |> List.map (fun c -> c.GenerateClass().Class) 
  let classesPrinted = classPrinter.PrintAllClasses controllerClasses "" 
  Console.WriteLine(Environment.CurrentDirectory)
  
  let directoryInfo = IO.Directory.CreateDirectory("GeneratedCode")
  
  IO.Directory.Delete(directoryInfo.FullName,true);
  
  IO.Directory.CreateDirectory(directoryInfo.Name) |> ignore
  classesPrinted |> List.iter (fun (cP) ->
    let containingFolder =  directoryInfo.Name + "\\" + cP.ClassPath
    IO.Directory.CreateDirectory containingFolder |> ignore
    use fs = IO.File.CreateText(containingFolder + cP.ClassName + cP.ClassExtension)
    cP.ClassContent |> List.iter (fun l -> fs.WriteLine(l)))
  classPrinter.PrepareProject (directoryInfo.FullName + "\\")
  0