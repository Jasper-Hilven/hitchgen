namespace LanguageInterface
//open ImplementationInterface

module API =
  0
  (*
  type APIProvider<'L> (tokenProvider: ITokenProvider<'L>) =
    member internal this.TokenProvider : ITokenProvider<'L>= tokenProvider 
    member this.ModuleRoot = LModule(this,tokenProvider.ModuleRoot)
    member this.LType (name:string) (lModule: LModule<'L>) = new LType<'L>(this,tokenProvider.TypeFree name lModule.ilModule)
    member this.ClMethodDeclV (declaration: LVariable<'L>) (parameters: LVariable<'L> list) (statement: LStatement<'L>)= new LMethodDecl<'L>(declaration, parameters, statement)
    member this.ClMethodDecl (name: string) (lType: LType<'L>) (parameters: LVariable<'L> list) (statement: LStatement<'L>)= 
      new LMethodDecl<'L>(new LVariable<'L>(this,tokenProvider.Variable name  lType.ILType), parameters, statement)
    member this.This = new LVariable<'L>(this, tokenProvider.OoThis)
    member this.StReturn exp = (new LStatement<'L> (this, this.TokenProvider.StReturn exp))
  and LModule<'L>(provider:APIProvider<'L>,ilModule: ILModule<'L>) = 
    member internal this.ilModule = ilModule
    member public this.Child name = LModule(provider,provider.TokenProvider.ModuleChild ilModule name)
  
  and LType<'L>(provider: APIProvider<'L>,ilType: ILType<'L>)=
    member internal this.ILType = ilType
    member this.FieldName = 
      let name = provider.TokenProvider.Type2String(ilType)
      if name.Length.Equals(0) then "" else name.Substring(0,1).ToLower() + name.Substring(1)
    member this.Name = provider.TokenProvider.Type2String(ilType)
    member this.Variable:LVariable<'L> = new LVariable<'L>(provider, provider.TokenProvider.Variable this.FieldName ilType)
  
  and LValue<'L>(provider: APIProvider<'L>, ilValue: ILValue<'L>)=
    member this.Eval = new LRHV<'L>(provider, ilValue.Eval)
  and LFieldAccess(provider: APIProvider<'L>, fAccess:ILFieldAccess<'L>)=
    member this.Eval = new LRHV<'L>(provider, fAccess.Eval)
  and LVariable<'L>(provider: APIProvider<'L>, ilVariable: ILVariable<'L>)=
    member this.Provider = provider
    member this.Eval = new LRHV<'L>(provider, ilVariable.Eval)
    member this.LType = new LType<'L>(provider,ilVariable.LType)
    member internal this.Variable = ilVariable
    member this.AccessField (field: LVariable<'L>) = 
      LFieldAccess(provider, provider.TokenProvider.OoAccessField ilVariable.Eval field.Variable)
    member this.SetTo (newValue: LRHV<'IL>) = new LStatement<'IL>(provider, provider.TokenProvider.StAssignment ilVariable newValue.IlRHV)
    member this.SetToV (newValue: LVariable<'IL>) = new LStatement<'IL>(provider, provider.TokenProvider.StAssignment ilVariable newValue.Eval.IlRHV)

  and LStatement<'L>(provider: APIProvider<'L>, ilStatement: ILStatement<'L>)=
    member internal this.Statement = ilStatement
    member this.Append(next: LStatement<'L>) = new LStatement<'L>(provider, provider.TokenProvider.StMultSt [ilStatement;next.Statement])
  
  and LRHV<'L>(provider: APIProvider<'L>, ilRHV: ILRHV<'L>)=
    member this.IlRHV = ilRHV
    member this.Statement = new LStatement<'L>(provider,ilRHV.Statement)
   // member this.AccessField (field: LVariable<'L>) = 
   //   new LFieldAccess<'L>(provider, provider.TokenProvider.OoAccessField ilRHV field.Variable)
  and LMethodDecl<'L>(description: LVariable<'L>,parameters: LVariable<'L> list, content: LStatement<'L>)=
    member this.Content = content
    member this.Description = description
    member this.Parameters = parameters
//  and LClassDecl<'L> (provider: APIProvider<'L>)=
  *)
    
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