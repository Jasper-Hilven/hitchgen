module ClassPrinter
open JAST
type JClassPrinter = class end

let rec GetBoxedStringRep(jType:JType) = 
    match jType with
      | Int -> "Integer"
      | Bool -> "Boolean"
      | Void -> "Void"
      | otherwise -> GetStringRep(otherwise)
and GetStringRep(jType:JType) = 
    match jType with
      | String -> "String"
      | Bool -> "boolean"
      | Int -> "int"
      | Void -> "void"
      | List inType-> "ArrayList<" + GetBoxedStringRep(inType) + ">"
      | Map(k,y) -> "HashMap<" + GetBoxedStringRep(k) + "," + GetBoxedStringRep(y) + ">"
      | Dedicated(name, import)-> name

let  GetVariableStringRepresentation(v: JVariable) = GetStringRep(v.JType) + " " + v.Name

let indent2Lines lines= lines |> List.map (fun l -> "  " + l)
let indent4Lines lines= lines |> List.map (fun l -> "    " + l)

let GetValueStringRepresentation(jValue: JValue): string =
    match jValue with
    |JTrue  -> "true"
    |JFalse -> "false"
    |EmptyString->"\"\""
    |JString s-> "\"" + s + "\""
    |JInt i-> i.ToString()

let GetOperatorStringRepresentation(jOperator: JOperator) : string= 
    "+"

let getListedVariables(parameters : JVariable list) =
      let variablesListed = parameters |> List.map (fun (o:JVariable) -> GetVariableStringRepresentation(o))
      if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)


let rec getListedRHVs(parameters: JRightHandValue list) = 
  let rHVsListed =  parameters |> List.map (fun o -> GetRHVStringRepresentation(o))
  if rHVsListed.Length.Equals(0) then "" else rHVsListed |> List.reduce (fun a b -> a + ", " + b)


and GetRHVStringRepresentation(rhv: JRightHandValue) = 
  match rhv with
    | Eval v-> v.Name
    | FieldEval v-> "this." + v.Name
    | AccessField(l,r) -> GetVariableStringRepresentation(l) + "." + GetVariableStringRepresentation(r)
    | ConstrCall(jType,vList)-> "new " + GetBoxedStringRep(jType) + "(" + getListedRHVs(vList) + ")"
    | OwnMethodCall(jMeth,parameters) -> "this."+ jMeth.Name + "(" + getListedRHVs(parameters) + ")"
    | MethodCall(jObj,jMeth,parameters) -> GetRHVStringRepresentation(jObj) + "." + jMeth.Name + "(" + getListedRHVs(parameters) + ")"
    | Value(jVal) -> GetValueStringRepresentation(jVal)
    | OperatorCall(left,oper,right) -> GetRHVStringRepresentation(left) + GetOperatorStringRepresentation(oper) + " " +  GetRHVStringRepresentation(right)

let rec GetStStringRepresentation(jSt: JStatement) = 
    match jSt with
    | DeclarationAssignment(v,rhv) -> [GetVariableStringRepresentation(v)+ " = " + GetRHVStringRepresentation(rhv) + ";"]
    | VariableAssignment(v,rhv) -> [v.Name + " = " + GetRHVStringRepresentation(rhv) + ";"]
    | FieldAssignment(v,rhv) -> ["this." + v.Name + " = " + GetRHVStringRepresentation(rhv) + ";"]
    | MultipleStatement sm -> if sm.Length.Equals(0) 
                              then [] 
                              else sm |> List.map (fun a-> GetStStringRepresentation(a))
                                      |> List.reduce (fun a b -> a @ b)
    | IfThenBlock(c,t,e) -> ["if(" + GetRHVStringRepresentation(c) + "){"]@ indent2Lines(GetStStringRepresentation(t))@["}";"else{"] @ indent2Lines(GetStStringRepresentation(e)) @ ["}"]
    | RHVStatement rhv -> [GetRHVStringRepresentation( rhv) + ";"]
    | EmptyStatement -> [";"]
    | ReturnStatement rhv -> ["return " + GetRHVStringRepresentation(rhv) +  ";"]
    | ReturnStatementVoid ->["return ;"]
    | Foreach(v,col,st) -> ("for(" + GetVariableStringRepresentation(v) + ": " + GetRHVStringRepresentation(col) + "){")::indent2Lines(GetStStringRepresentation(st)) @ ["}"]
let printMethod(jMethod:JMethod) : string list = 
  let concatVariables = getListedVariables(jMethod.Parameters)
  let declaration = "public " + GetStringRep(jMethod.JType) + " " + jMethod.Name + "(" + concatVariables + "){"
  let indentedStatements = indent2Lines(GetStStringRepresentation(jMethod.Statements)) 
  let ending = ["}"]
  declaration::indentedStatements @ ending

let printConstructor(jConstructor: JConstructor) = 
  let concatVariables = getListedVariables(jConstructor.Parameters)
  let declaration = "public " + GetBoxedStringRep(jConstructor.JType) + "(" + concatVariables + "){"
  let indentedStatements = indent2Lines(GetStStringRepresentation(jConstructor.Statements)) 
  let ending = ["}"]
  declaration:: indentedStatements @  ending

let GetClassName(jClass: JClass) =   GetBoxedStringRep(jClass.JType) + ".java"

let printClass(jClass: JClass) = 
    let classDefLine =  "public class " + GetBoxedStringRep(jClass.JType) + "{"
    let printField(field : JVariable)= GetVariableStringRepresentation(field) + ";"
    let fieldDeclarations = 
      if jClass.Fields.Length.Equals(0) then [] else List.map printField jClass.Fields
      |> indent2Lines
    let constructorsPrinted = jClass.Constructors |> List.map (fun m -> printConstructor(m))
    let constructorLines = 
      if(constructorsPrinted.Length = 0) then [] else constructorsPrinted |> List.reduce(fun a b -> a @ [""] @ b)
      |> indent2Lines
    let methodsPrinted = jClass.Methods |> List.map(fun m -> printMethod(m))
    let methodsLines = 
      if(jClass.Methods.Length = 0) then [] else methodsPrinted |> List.reduce(fun a b -> a @ [""] @ b)
      |> indent2Lines
    let classClosing = ["}"]
    let fullDescription = 
      let appendLinesIfNotEmpty(lines:string list) = if lines.Length.Equals(0) then [] else lines @ doubleEmptyLine
      classDefLine::doubleEmptyLine @ appendLinesIfNotEmpty(fieldDeclarations) @ appendLinesIfNotEmpty(constructorLines) @ appendLinesIfNotEmpty(methodsLines)@classClosing
    fullDescription