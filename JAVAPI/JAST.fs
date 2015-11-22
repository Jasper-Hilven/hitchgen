module JAST
open LanguageInterface.TokenProvider

type ILJava = 
  interface ILanguage with
    member this.LanguageName = "Java"

open LanguageInterface.TokenProvider
type JModule = 
  | SPath of string
  member this.Print = match this with | SPath s -> s
  interface ILModule<ILJava>
type  JType = 
  | String
  | Bool
  | Int
  | List of JType
  | Map of JType*JType
  | Dedicated of string * JModule
  | Void
  interface ILType<ILJava>


type JVariable(name:string, jVType:JType) = 
  member public this.Name = name
  member public this.JType = jVType
  interface ILVariable<ILJava> with
    member this.Eval = JRightHandValue.Eval(this) :> ILRHV<ILJava>

  
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
  interface ILStatement<ILJava>
and JValue = 
  | JString of string
  | JInt of int
  | JTrue
  | JFalse
  | EmptyString
  interface ILValue<ILJava> with
    member this.Eval = JRightHandValue.Value(this) :> ILRHV<ILJava>
 
and JRightHandValue = 
  | Value of JValue
  | Eval of JVariable
  | FieldEval of JVariable
  | AccessField of JVariable * JVariable
  | ConstrCall of JType * (JRightHandValue list)
  | MethodCall of JRightHandValue * JVariable * (JRightHandValue list) 
  | OwnMethodCall of JVariable * (JRightHandValue list)
  | OperatorCall of JRightHandValue * JOperator *JRightHandValue
  interface ILRHV<ILJava> with
    member this.Statement = JStatement.RHVStatement(this) :> ILStatement<ILJava>

and JOperator = 
  | ConcatString



let emptyLine = [""]
let doubleEmptyLine = ["";""]

type JMethod(name:string,jType : JType, parameters : JVariable list,statements : JStatement) =
  member public this.Name = name
  member public this.JType = jType
  member public this.Parameters = parameters
  member public this.Statements = statements

type JConstructor(jType : JType, parameters: JVariable list, statements : JStatement) = 
  member this.JType = jType
  member this.Parameters = parameters
  member this.Statements = statements


type JClass(jType:JType,constructors : JConstructor list, methods: JMethod list, fields : JVariable list) = 
  member this.JType = jType
  member this.Vonstructors = constructors
  member this.Methods = methods
  member this.Fields = fields