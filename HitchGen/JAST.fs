module JAST

let indent2Lines lines= lines |> List.map (fun l -> "  " + l)
let indent4Lines lines= lines |> List.map (fun l -> "    " + l)

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
      | Bool -> "Boolean"
      | Void -> "Void"
      | otherwise -> otherwise.GetStringRep()
  member this.GetStringRep() = 
    match this with
      | String -> "String"
      | Bool -> "boolean"
      | Int -> "Int"
      | Void -> "void"
      | List inType-> "HashList<" + inType.GetBoxedStringRep() + ">"
      | Map(k,y) -> "HashMap<" + k.GetBoxedStringRep() + "," + y.GetBoxedStringRep() + ">"
      | Dedicated name -> name

type JVariable(name:string, jVType:JType) = 
  member this.GetStringRepresentation() = jVType.GetStringRep() + " " + name
  member public this.Name = name
  member public this.JType = jVType
and JStatement = 
| DeclarationAssignment of JVariable * JRightHandValue
| VariableAssignment of JVariable * JRightHandValue
| FieldAssignment of JVariable * JRightHandValue
| MultipleStatement of (JStatement list)
| IfThenBlock of JRightHandValue * JStatement * JStatement
| RHVStatement of JRightHandValue
| EmptyStatement
| ReturnStatement of JRightHandValue
| ReturnStatementVoid 
| Foreach of JVariable * JRightHandValue * JStatement
  member this.GetStringRepresentation() = 
    match this with
    | DeclarationAssignment(v,rhv) -> [v.GetStringRepresentation()+ " = " + rhv.GetStringRepresentation() + ";"]
    | VariableAssignment(v,rhv) -> [v.Name + " = " + rhv.GetStringRepresentation() + ";"]
    | FieldAssignment(v,rhv) -> ["this." + v.Name + " = " + rhv.GetStringRepresentation() + ";"]
    | MultipleStatement sm -> if sm.Length.Equals(0) 
                              then [] 
                              else sm |> List.map (fun a-> a.GetStringRepresentation())
                                      |> List.reduce (fun a b -> a @ b)
    | IfThenBlock(c,t,e) -> ["if(" + c.GetStringRepresentation() + "){"]@ indent2Lines(t.GetStringRepresentation())@["}";"else{"] @ indent2Lines(e.GetStringRepresentation()) @ ["}"]
    | RHVStatement rhv -> [rhv.GetStringRepresentation() + ";"]
    | EmptyStatement -> [";"]
    | ReturnStatement rhv -> ["return " + rhv.GetStringRepresentation() +  ";"]
    | ReturnStatementVoid ->["return ;"]
    | Foreach(v,col,st) -> ("for(" + v.GetStringRepresentation() + ": " + col.GetStringRepresentation() + "){")::st.GetStringRepresentation() @ ["}"]
and JValue = 
| JTrue
| JFalse
  member this.GetStringRepresentation(): string =
    match this with
    |JTrue  -> "true"
    |JFalse -> "false"
and JRightHandValue = 
| Value of JValue
| Eval of JVariable
| FieldEval of JVariable
| AccessField of JVariable * JVariable
| ConstrCall of JType * (JRightHandValue list)
| MethodCall of JVariable * JVariable * (JRightHandValue list) 
  member this.GetStringRepresentation() : string= 
    
    let getListedVariables(parameters : JVariable list) =
      let variablesListed = parameters |> List.map (fun (o:JVariable) -> o.GetStringRepresentation())
      if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)
    let getListedRHVs(parameters: JRightHandValue list) = 
      let RHVsListed =  parameters |> List.map (fun o -> o.GetStringRepresentation())
      if RHVsListed.Length.Equals(0) then "" else RHVsListed |> List.reduce (fun a b -> a + ", " + b)

    match this with
    | Eval v-> v.Name
    | FieldEval v-> "this." + v.Name
    | AccessField(l,r) -> l.GetStringRepresentation() + "." + r.GetStringRepresentation()
    | ConstrCall(jType,vList)-> "new " + jType.GetBoxedStringRep() + "(" + getListedRHVs(vList) + ")"
    | MethodCall(jObj,jMeth,parameters) -> jObj.Name + "." + jMeth.Name + "(" + getListedRHVs(parameters) + ")"
    | Value(jVal) -> jVal.GetStringRepresentation()

let getListedVariables(parameters : JVariable list) =
      let variablesListed = parameters |> List.map (fun (o:JVariable) -> o.GetStringRepresentation())
      if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)


let emptyLine = [""]
let doubleEmptyLine = ["";""]

type JMethod(name:string,jType : JType, parameters : JVariable list,statements : JStatement) =
  member this.Print() : string list = 
    let concatVariables = getListedVariables(parameters)
    let declaration = "public " + jType.GetStringRep() + " " + name + "(" + concatVariables + "){"
    let indentedStatements = indent2Lines(statements.GetStringRepresentation()) 
    let ending = ["}"]
    declaration::indentedStatements @ ending

type JConstructor(jType : JType, parameters: JVariable list, statements : JStatement) = 
  member this.getJType() = jType
  member this.getParameters() = parameters
  member this.getStatements() = statements

  member this.Print() : string list =
    let concatVariables = getListedVariables(parameters)
    let declaration = "public " + jType.GetBoxedStringRep() + "(" + concatVariables + "){"
    let indentedStatements = indent2Lines(statements.GetStringRepresentation()) 
    let ending = ["}"]
    declaration:: indentedStatements @  ending

type JClass(jType:JType,constructors : JConstructor list, methods: JMethod list, fields : JVariable list) = 
  member this.FileName = jType.GetStringRep() + ".java"
  member this.Print() = 
    let classDefLine =  "public class " + jType.GetBoxedStringRep() + "{"
    let printField(field : JVariable)= field.GetStringRepresentation() + ";"
    let fieldDeclarations = 
      if fields.Length.Equals(0) then [] else List.map printField fields
      |> indent2Lines
    let constructorsPrinted = constructors |> List.map (fun m -> m.Print())
    let constructorLines = 
      if(constructorsPrinted.Length = 0) then [] else constructorsPrinted |> List.reduce(fun a b -> a @ [""] @ b)
      |> indent2Lines
    let methodsPrinted = methods |> List.map(fun m -> m.Print())
    let methodsLines = 
      if(methods.Length = 0) then [] else methodsPrinted |> List.reduce(fun a b -> a @ [""] @ b)
      |> indent2Lines
    let classClosing = ["}"]
    let fullDescription = 
      let appendLinesIfNotEmpty(lines:string list) = if lines.Length.Equals(0) then [] else lines @ doubleEmptyLine
      classDefLine::doubleEmptyLine @ appendLinesIfNotEmpty(fieldDeclarations) @ appendLinesIfNotEmpty(constructorLines) @ appendLinesIfNotEmpty(methodsLines)@classClosing
    fullDescription