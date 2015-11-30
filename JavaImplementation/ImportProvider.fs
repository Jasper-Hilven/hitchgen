namespace JavaImplementation
open AST
open JBasics
module ImportProvider = 


  let rec GetTypeImports (jType: JType) : Set<JModule>= 
      match jType with
        | String -> Set.empty
        | Bool -> Set.empty
        | Int -> Set.empty
        | Void -> Set.empty
        | List iType -> Set.add AListPath (GetTypeImports iType)
        | Map(k,v)-> Set.union (GetTypeImports k) (GetTypeImports v) |> Set.add HMapPath
        | Dedicated(name, import) -> Set.empty.Add(import)
    