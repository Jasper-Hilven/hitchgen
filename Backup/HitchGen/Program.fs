open Controllers
open JavaSyntaxParser
open System
open GenerateBasics
open GenerateFactories
open PropertyGeneration
  

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
  let def = ParseProperties(Controller.PhysicsController)
  0