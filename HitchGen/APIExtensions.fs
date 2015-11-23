module APIExtensions
open LanguageInterface.TokenProvider

type LType<'L>(provider: ITokenProvider<'L>,ilType: ILType<'L>)=
  member this.FieldName = 
    let name = provider.Type2String(ilType)
    if name.Length.Equals(0) then "" else name.Substring(0,1).ToLower() + name.Substring(1)
  member this.Name = provider.Type2String(ilType)

and LValue<'L>(provider: ITokenProvider<'L>, ilValue: ILValue<'L>)=
  member this.Eval = ilValue.Eval

and LVariable<'L>(provider: ITokenProvider<'L>, ilVariable: ILVariable<'L>)=
  member this.Eval = ilVariable.Eval
  member internal this.Variable = ilVariable
  member this.Field (field: LVariable<'L>) = 
    LRHV(provider, provider.OoAccessField ilVariable.Eval field.Variable)
and LStatement<'L>(provider: ITokenProvider<'L>, ilStatement: ILStatement<'L>)=
  member internal this.Statement = ilStatement
  member this.Append(next: LStatement<'L>) = new LStatement<'L>(provider, provider.StMultSt [ilStatement;next.Statement])
and LRHV<'L>(provider: ITokenProvider<'L>, ilRHV: ILRHV<'L>)=
  member this.Statement = new LStatement<'L>(provider,ilRHV.Statement)
  member this.Field (field: LVariable<'L>) = 
    LRHV(provider, provider.OoAccessField ilRHV field.Variable)




(*

let GetFieldNameType<'L>(fieldType: ILType<'L>) = 
  let fieldTypeName = fieldType.GetStringRep()
  fieldTypeName.Substring(0,1).ToLower() + fieldTypeName.Substring(1)
let GetCapitalName(word: string) = if word.Length.Equals(0) then word else word.Substring(0,1).ToUpper()+ word.Substring(1)

let GetVariableOfType(jType) = GetVariable(GetFieldNameType(jType),jType)

let GetGetterCall(objectToCall,field: JVariable) = GetCallOnObject(objectToCall,GetVariable("Get" + GetCapitalName(field.Name),field.JType),[])
let GetGetter(field: JVariable) = 
  GetMethodDeclaration("Get" + GetCapitalName(field.Name),field.JType,[],GetReturnStatement(GetFieldEval(field)))

let AppendToConstructor(oldConstructor: JConstructor, furtherStatements:JStatement) = 
  let updatedStatements = GetCollectStatement([oldConstructor.getStatements();furtherStatements])
  GetConstructorDeclaration(oldConstructor.getJType(),oldConstructor.getParameters(),updatedStatements)

let GetConstructorFieldInitializations(fieldsToInitialize, constructorType) =
  let content = fieldsToInitialize |> List.map(fun o -> GetSetField(o,GetVariableEval(o))) |> GetCollectStatement
  GetConstructorDeclaration(constructorType,fieldsToInitialize,content)
  *)