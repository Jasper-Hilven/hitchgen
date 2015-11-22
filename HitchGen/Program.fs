open GenerateBasics
open GenerateFactories
open System

[<EntryPoint>]
let main argv = 
  (*
  let allFiles = controllerFactoryFiles @ controllerFiles
  
  Console.WriteLine(Environment.CurrentDirectory)
  
  let directoryInfo = IO.Directory.CreateDirectory("GeneratedCode")
  
  IO.Directory.Delete(directoryInfo.FullName,true);
  
  IO.Directory.CreateDirectory("GeneratedCode") |> ignore
  allFiles |> List.iter (fun (f) -> 
    use fs = IO.File.CreateText("GeneratedCode\\" + f.FileName)
    f.Print() |> List.iter (fun l -> fs.WriteLine(l)))   *)
  0