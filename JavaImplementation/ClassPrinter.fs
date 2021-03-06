﻿namespace JavaImplementation
open JavaImplementation.AST
open LanguageInterface.ImplementationInterface
open ImportProvider
module JClassPrinter =

  type JClassResult(classPath, classContent, className: string) = 
    interface IClassResult with
      member this.ClassPath = classPath
      member this.ClassContent = classContent
      member this.ClassName = className
      member this.ClassExtension = ".java"
  
  type JClassPrinter() =
    let rec printTypeBoxed(jType:JType) = 
      match jType with
        | Int -> "Integer"
        | Bool -> "Boolean"
        | Void -> "Void"
        | otherwise -> printType(otherwise)
    and printType (jType:JType) = 
      match jType with
        | String -> "String"
        | Bool -> "boolean"
        | Int -> "int"
        | Void -> "void"
        | List inType-> "ArrayList<" + printTypeBoxed(inType) + ">"
        | Map(k,y) -> "HashMap<" + printTypeBoxed(k) + "," + printTypeBoxed(y) + ">"
        | Dedicated(name, import)-> name
  
    let printVariable (v: JVariable) = printType(v.JType) + " " + v.Name
  
    let indent2Lines lines= lines |> List.map (fun l -> "  " + l)
    let indent4Lines lines= lines |> List.map (fun l -> "    " + l)
  
    let printValue (jValue: JValue): string =
      match jValue with
      |JTrue  -> "true"
      |JFalse -> "false"
      |EmptyString->"\"\""
      |JString s-> "\"" + s + "\""
      |JInt i-> i.ToString()
  
    let printOperator (jOperator: JOperator) : string= 
      "+"
  
    let printListedVariables (parameters : JVariable list) =
        let variablesListed = parameters |> List.map (fun (o:JVariable) -> printVariable(o))
        if variablesListed.Length.Equals(0) then "" else variablesListed |> List.reduce (fun a b -> a + ", " + b)
  
  
    let rec printListedRHVs(parameters: JRightHandValue list) = 
      let rHVsListed =  parameters |> List.map (fun o -> printRHV(o))
      if rHVsListed.Length.Equals(0) then "" else rHVsListed |> List.reduce (fun a b -> a + ", " + b)
  
    and printAccessField(af: JAccessField)= printRHV((af :> ILFieldAccess<ILJava>).On:?> JRightHandValue) + "." + ((af :> ILFieldAccess<ILJava>).Field :?> JVariable).Name 
    and printRHV(rhv: JRightHandValue) = 
      match rhv with
        | Eval v-> v.Name
        | FieldEval v-> "this." + v.Name
        | AccessField(aF) -> printAccessField(aF)
        | ConstrCall(jType,vList)-> "new " + printTypeBoxed(jType) + "(" + printListedRHVs(vList) + ")"
        | OwnMethodCall(jMeth,parameters) -> "this."+ jMeth.Name + "(" + printListedRHVs(parameters) + ")"
        | MethodCall(jObj,jMeth,parameters) -> printRHV(jObj) + "." + jMeth.Name + "(" + printListedRHVs(parameters) + ")"
        | Value(jVal) -> printValue(jVal)
        | OperatorCall(left,oper,right) -> printRHV(left) + printOperator(oper) + " " +  printRHV(right)
  
    let rec printStatement(jSt: JStatement) = 
      match jSt with
      | DeclarationAssignment(v,rhv) -> [printVariable(v)+ " = " + printRHV(rhv) + ";"]
      | VariableAssignment(v,rhv) -> [v.Name + " = " + printRHV(rhv) + ";"]
      | JStatement.FieldAssignment(v,rhv) -> [printAccessField(v) + "=" + printRHV(rhv)+ ";"]
      | MultipleStatement sm -> sm |> List.map printStatement|> List.concat
      | IfThenBlock(c,t,e) -> ["if(" + printRHV(c) + "){"]@ indent2Lines(printStatement(t))@["}";"else{"] @ indent2Lines(printStatement(e)) @ ["}"]
      | RHVStatement rhv -> [printRHV( rhv) + ";"]
      | EmptyStatement -> [";"]
      | ReturnStatement rhv -> ["return " + printRHV(rhv) +  ";"]
      | ReturnStatementVoid ->["return ;"]
      | Foreach(v,col,st) -> ("for(" + printVariable(v) + ": " + printRHV(col) + "){")::indent2Lines(printStatement(st)) @ ["}"]
  
    let printMethod(jMethod:JMethod) : string list = 
      let concatVariables = printListedVariables(jMethod.Parameters)
      let declaration = "public " + printType(jMethod.JType) + " " + jMethod.Name + "(" + concatVariables + "){"
      let indentedStatements = indent2Lines(printStatement(jMethod.Statements)) 
      let ending = ["}"]
      declaration::indentedStatements @ ending
      
    let printConstructor(jConstructor: JConstructor) = 
      let concatVariables = printListedVariables(jConstructor.Parameters)
      let declaration = "public " + printTypeBoxed(jConstructor.JType) + "(" + concatVariables + "){"
      let indentedStatements = indent2Lines(printStatement(jConstructor.Statements)) 
      let ending = ["}"]
      declaration:: indentedStatements @ ending
  
    let getClassName(jClass: JClass) =   printTypeBoxed(jClass.JType) + ".java"
  
    let getImportLines (jClass: JClass)= (GetClassImports jClass) |> Set.map (fun m -> "import " + m.Print + ";") |> Set.toList
    let getPackageLine (jClass: JClass) = 
      match jClass.JType with 
        |JType.Dedicated(name,jModule) -> "package " + jModule.Print  + ";"
        | _ -> raise(new System.Exception("Only classes of dedicated types allowed"))
    let printClass(jClass: JClass) = 
      let packageLine = [getPackageLine jClass]
      let importLines = getImportLines jClass
      let classDefLine =  ["public class " + printTypeBoxed(jClass.JType) + "{"]
      let printField(field : JVariable)= printVariable(field) + ";"
      let fieldDeclarations = if jClass.Fields.Length.Equals(0) then [] else List.map printField jClass.Fields |> indent2Lines
      let constructorsPrinted = jClass.Constructors |> List.map (fun m -> printConstructor(m))
      let constructorLines = if(constructorsPrinted.Length = 0) then [] else constructorsPrinted |> List.reduce(fun a b -> a @ [""] @ b) |> indent2Lines
      let methodsPrinted = jClass.Methods |> List.map(fun m -> printMethod(m))
      let methodsLines = if(jClass.Methods.Length = 0) then [] else methodsPrinted |> List.reduce(fun a b -> a @ [""] @ b) |> indent2Lines
      let classClosing = ["}"]
      let appendLinesIfNotEmpty(lines:string list) = if lines.Length.Equals(0) then [] else lines @ doubleEmptyLine
      let printPieces = [packageLine;importLines;classDefLine;fieldDeclarations;constructorLines;methodsLines;classClosing]
      printPieces |> List.map appendLinesIfNotEmpty |> List.concat
  
    interface IClassPrinter<ILJava> with
      member this.PrintAllClasses (classes: ILClass<ILJava> list)  (path: string) = 
        let gradlePath = "src\\main\\java\\"
        classes |> List.map (fun c-> let cCast = c :?> JClass in JClassResult(path + gradlePath, printClass(cCast),printType(cCast.JType)) :> IClassResult)
      member this.PrepareProject path =
        let content = "apply plugin: 'java'"
        let fileName = "build.gradle"
        use fs = System.IO.File.CreateText (path + fileName)
        fs.WriteLine content