namespace JavaImplementation
open JAST
open LanguageInterface.ImplementationInterface

module JAPI =

  exception StringRepNotExistingExisting of string
  
  
  type JProvider() = 
    interface ITokenProvider<ILJava> with
  
      member this.TypeList baseType = JType.List(baseType :?> JType) :> ILType<ILJava> 
      member this.TypeMap keyType valueType = JType.Map(keyType :?> JType,valueType:?> JType) :> ILType<ILJava>
      member this.TypeFree name ilModule = JType.Dedicated(name, ilModule :?> JModule) :> ILType<ILJava>
      member this.TypeVoid = JType.Void  :> ILType<ILJava>
      member this.TypeString= JType.String :> ILType<ILJava>
      member this.TypeBool= JType.Bool   :> ILType<ILJava>
      member this.TypeInt= JType.Int    :> ILType<ILJava>
      member this.Type2String (t:ILType<ILJava>) : string=
        match (t :?> JType) with
          | JType.Dedicated (n,m) -> n
          | _ -> raise(StringRepNotExistingExisting("only allowed for dedicated types"))
      member this.ValueTrue= JValue.JTrue :> ILValue<ILJava>
      member this.ValueFalse= JValue.JFalse :> ILValue<ILJava>
      member this.ValueString sVal = JValue.JString(sVal) :> ILValue<ILJava>
      member this.ValueInt iVal = JValue.JInt(iVal) :> ILValue<ILJava>
          
      member this.Variable name jType = JVariable(name, jType :?> JType) :> ILVariable<ILJava>
      
      member this.StIfBlock condition statement = IfThenBlock(condition :?> JRightHandValue, statement :?> JStatement, EmptyStatement):> ILStatement<ILJava>
      member this.StIfElseBlock condition ifStatement elseStatement = IfThenBlock(condition :?> JRightHandValue, ifStatement :?> JStatement, elseStatement :?> JStatement):> ILStatement<ILJava> 
      member this.StAssignment jVariable jValue = VariableAssignment(jVariable :?> JVariable,jValue:?> JRightHandValue) :> ILStatement<ILJava>
      member this.StMultSt statements = JStatement.MultipleStatement(statements |> List.map (fun o -> o :?> JStatement)) :> ILStatement<ILJava>
      member this.StDeclAssignVariable jVariable jValue = JStatement.DeclarationAssignment(jVariable :?> JVariable,jValue:?> JRightHandValue) :> ILStatement<ILJava>
      member this.StReturn jValue = ReturnStatement(jValue :?> JRightHandValue) :> ILStatement<ILJava>
      member this.StReturnVoid = ReturnStatementVoid :> ILStatement<ILJava>
      member this.StForeach jIterator jCollection jContent = Foreach(jIterator :?>JVariable,jCollection :?>JRightHandValue,jContent :?> JStatement) :> ILStatement<ILJava>
      
      member this.OoAccessField jRHV jField = AccessField(jRHV :?> JRightHandValue, jField :?> JVariable) :> ILFieldAccess<ILJava>
      member this.OoConstructorCall jType jParameters = ConstrCall(jType :?> JType, jParameters |> List.map (fun o -> o :?> JRightHandValue)) :> ILRHV<ILJava>
      member this.OoThis = JVariable("this",JType.Void) :> ILVariable<ILJava>
      member this.OoMethodCall (jObject:ILRHV<ILJava>) (jMethod:ILVariable<ILJava>) (jParameters:ILRHV<ILJava> list)= 
        MethodCall(jObject :?> JRightHandValue, jMethod :?> JVariable,jParameters |> List.map (fun o -> o :?> JRightHandValue)) :> ILRHV<ILJava>
    
      
      member this.ClMethodDecl (decl: ILVariable<ILJava>) (jParams: ILVariable<ILJava> list) (jStatements: ILStatement<ILJava>) =
        let declV = decl :?> JVariable
        new JMethod(declV.Name ,declV.JType ,jParams|> List.map (fun o -> o :?> JVariable),jStatements :?> JStatement) :> ILMethod<ILJava>
      member this.ClConstructorDecl (jType : ILType<ILJava>) (jParams: ILVariable<ILJava> list) (jStatements: ILStatement<ILJava>) = 
        new JConstructor(jType :?> JType, jParams|> List.map (fun o -> o :?> JVariable), jStatements :?> JStatement) :> ILConstructor<ILJava>
      member this.ClClassDecl (jType: ILType<ILJava>) (constructors: ILConstructor<ILJava> list) (methods: ILMethod<ILJava> list)  (fields: ILVariable<ILJava> list)=
        new JClass(jType:?>JType,constructors |> List.map (fun o -> o :?> JConstructor), methods |> List.map (fun o -> o :?> JMethod),fields |> List.map (fun o -> o :?> JVariable)) :> ILClass<ILJava>
      
      member this.ModuleRoot = JModule.Root :> ILModule<ILJava>
      member this.ModuleChild parent name = JModule.Child(parent :?> JModule,name) :> ILModule<ILJava>
    
  type JPrinter = JClassPrinter.JClassPrinter