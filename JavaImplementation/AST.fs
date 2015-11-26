namespace JavaImplementation
module JAST
open LanguageInterface.ImplementationInterface

  
type JModule = 
  | Root
  | Child of JModule * string
  interface ILModule<ILJava>

type JType = 
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
    member this.Name = name
    member this.LType = this.JType :> ILType<ILJava>
  
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
  | AccessField of JRightHandValue * JVariable
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
  interface ILMethod<ILJava>
type JConstructor(jType : JType, parameters: JVariable list, statements : JStatement) = 
  member this.JType = jType
  member this.Parameters = parameters
  member this.Statements = statements
  interface ILConstructor<ILJava>

type JClass(jType:JType,constructors : JConstructor list, methods: JMethod list, fields : JVariable list) = 
  member this.JType = jType
  member this.Constructors = constructors
  member this.Methods = methods
  member this.Fields = fields
  interface ILClass<ILJava>