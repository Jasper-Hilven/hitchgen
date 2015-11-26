namespace JavaImplementation
module ImportProvider = 0

(*open JAST
let rec GetImports(jType: JType) : string list = 
    match jType with
      | String -> []
      | Bool -> []
      | Int -> []
      | Void -> []
      | List iType -> "java.util.ArrayList"::GetImports(iType)
      | Map(k,v)-> "java.util.HashMap"::GetImports(k) @ GetImports(v)
      | Dedicated(name, import) -> 
        match import with
            | Root -> ""
            | Child (m,n) -> 

            *)