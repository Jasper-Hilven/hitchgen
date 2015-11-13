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
| DeclarationAssignment of JVariable * JRightHandValue
| VariableAssignment of JVariable * JRightHandValue
| MultipleStatement of JStatement * JStatement
| IfThenBlock of JRightHandValue * JStatement * JStatement
| EmptyStatement
| ReturnStatement of JRightHandValue
| Foreach of JVariable * JRightHandValue * JStatement
  member this.GetStringRepresentation() = 
    match this with
    | DeclarationAssignment(v,rhv) -> [v.GetStringRepresentation()+ "= " + rhv.GetStringRepresentation() + ";"]
    | VariableAssignment(v,rhv) -> [v.Name + "= " + rhv.GetStringRepresentation() + ";"]
    | MultipleStatement(l,r) -> l.GetStringRepresentation()@r.GetStringRepresentation()
    | IfThenBlock(c,t,e) -> ["if(" + c.GetStringRepresentation() + "){"]@t.GetStringRepresentation()@["else"] @ e.GetStringRepresentation() @ ["}"]
    | EmptyStatement -> [";"]
    | ReturnStatement rhv -> ["return " + rhv.GetStringRepresentation() +  ";"]
    | Foreach(v,col,st) -> ("for(" + v.GetStringRepresentation() + ": " + col.GetStringRepresentation() + "){")::st.GetStringRepresentation() @ [";"]

and JRightHandValue = 
| Eval of JVariable
| FieldEval of JVariable
| AccessField of JVariable * JVariable
| ConstrCall of JType * (JVariable list)
| MethodCall of JVariable * JVariable * (JVariable list) 
  member this.GetStringRepresentation() : string= 
    
    let getListedVariables(parameters : JVariable list) =
      let variablesListed = parameters |> List.map (fun (o:JVariable) -> o.GetStringRepresentation())
      if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)

    match this with
    | Eval v-> v.Name
    | FieldEval v-> "this." + v.Name
    | AccessField(l,r) -> l.GetStringRepresentation() + "." + r.GetStringRepresentation()
    | ConstrCall(jType,vList)-> "new " + jType.GetBoxedStringRep() + "(" + getListedVariables(vList) + ")"
    | MethodCall(jObj,jMeth,parameters) -> jObj.Name + "." + jMeth.Name + "(" + getListedVariables(parameters) + ")"

let getListedVariables(parameters : JVariable list) =
      let variablesListed = parameters |> List.map (fun (o:JVariable) -> o.GetStringRepresentation())
      if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)


let indent2Lines lines= lines |> List.map (fun l -> "  " + l)
let indent4Lines lines= lines |> List.map (fun l -> "    " + l)


type JMethod(name:string,jType : JType, parameters : JVariable list,statements : JStatement) =
  member this.Print() : string list = 
    let concatVariables = getListedVariables(parameters)
    let declaration = "public " + jType.GetStringRep() + " " + name + "(" + concatVariables + "){"
    let indentedStatements = indent4Lines(statements.GetStringRepresentation()) 
    let ending = ["}"]
    declaration::indentedStatements @ ending

type JConstructor(jType : JType, parameters: JVariable list, statements : JStatement) = 
  member this.Print() : string list =
    let concatVariables = getListedVariables(parameters)
    let declaration = "public" + jType.GetBoxedStringRep() + "(" + concatVariables + "){"
    let indentedStatements = indent4Lines(statements.GetStringRepresentation()) 
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

  