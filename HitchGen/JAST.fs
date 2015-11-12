module JAST

type JType = 
  | String
  | Bool
  | Int
  | List of JType
  | Map of JType*JType
  | Dedicated of string
  | Void

  member this.GetBoxedStringRep() = 
    match this with
      | Int -> "Integer"
      | otherwise -> otherwise.GetStringRep()
  member this.GetStringRep() = 
    match this with
      | String -> "String"
      | Bool -> "Boolean"
      | Int -> "Int"
      | Void -> "Void"
      | List inType-> "HashList<" + inType.GetBoxedStringRep() + ">"
      | Map(k,y) -> "HashMap<" + k.GetBoxedStringRep() + "," + y.GetBoxedStringRep() + ">"
      | Dedicated name -> name

type JVariable(name:string,jVType:JType) = 
  member this.GetStringRepresentation() = jVType.GetStringRep() + " " + name
  member public this.Name = name

and JStatement = 
| VariableAssignment of JVariable * JRightHandValue
| MultipleStatement of JStatement * JStatement
  member this.GetStringRepresentation() = 
    match this with
    | VariableAssignment(v,rhv) -> [v.GetStringRepresentation()+ "= " + rhv.GetStringRepresentation() + ";"]
    | MultipleStatement(l,r) -> l.GetStringRepresentation()@r.GetStringRepresentation()
and JRightHandValue = 
| Eval of JVariable
  member this.GetStringRepresentation() : string= 
    match this with
    | Eval v-> v.Name

let indent2Lines lines= lines |> List.map (fun l -> "  " + l)
let indent4Lines lines= lines |> List.map (fun l -> "    " + l)


type JMethod(name:string,jType : JType, parameters : JVariable list,statements : JStatement list) =
  member this.Print() : string list = 
    let concatVariables = 
      let variablesListed:string list = parameters |> List.map (fun (o:JVariable) -> o.GetStringRepresentation())
      if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)
    let declaration = "  public " + jType.GetStringRep() + " " + name + "(" + concatVariables + "){"
    let indentedStatements = 
      if statements.Length.Equals(0) then [] else 
      (statements |> List.map (fun s->s.GetStringRepresentation()) |> List.reduce (fun s1 s2 -> s1 @ s2) |> indent4Lines )
    let ending = ["}"]
    declaration::indentedStatements @ ending


type JClass(name:string,methods: JMethod list, fields : JVariable list) = 
  member this.Print() = 
    let classDefLine =  "public class " + name + "{"
    let printField(field : JVariable)= "  "+ field.GetStringRepresentation() + ";"
    let fieldDeclarations = if fields.Length.Equals(0) then [] else List.map printField fields
    let methodsPrinted = methods |> List.map(fun m -> m.Print())
    let methodsLines = if(methods.Length = 0) then [] else methodsPrinted |> List.reduce(fun a b -> a @ [""] @ b)
    let classClosing = "}"
    classDefLine::methodsLines @ [classClosing]

  