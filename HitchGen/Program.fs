open Controllers
open Properties
open JavaSyntaxParser
open System
open GenerateBasics
open GenerateFactories

  

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