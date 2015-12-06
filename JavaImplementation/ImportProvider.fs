namespace JavaImplementation
open AST
open LanguageInterface.ImplementationInterface
open JBasics
module ImportProvider = 

  let rec private getTypeImports (jType: JType) : Set<JType>= 
    match jType with
      | String -> Set.empty
      | Bool -> Set.empty
      | Int -> Set.empty
      | Void -> Set.empty
      | List iType -> Set.empty.Add(jType)
      | Map(k,v)-> Set.empty.Add(jType)
      | Dedicated(name, import) -> Set.empty.Add(jType)
  let rec private getImportFromType(jType: JType) = 
    match jType with
      | String -> Set.empty
      | Bool -> Set.empty
      | Int -> Set.empty
      | Void -> Set.empty
      | List iType -> Set.add AListPath (getImportFromType iType)
      | Map(k,v)-> Set.empty.Add(HMapPath) + (getImportFromType k) + (getImportFromType v)
      | Dedicated(name, import) -> Set.empty.Add(JModule.Child(import,name))
  

  let private getVariableImports (jVariable: JVariable)= getTypeImports(jVariable.JType)
  
  let rec GetTypeImportsFieldAccess (jAF: JAccessField ) = 
    (getRHVImports jAF.JOn) + getVariableImports(jAF.JField)

  and private getRHVImports (jRV: JRightHandValue) = 
    match jRV with
    | Value v -> Set.empty
    | Eval jV -> getVariableImports jV 
    | FieldEval jV -> getVariableImports jV 
    | AccessField aF-> GetTypeImportsFieldAccess aF
    | ConstrCall(t,rhvs) -> getTypeImports(t) + (rhvs |> List.map getRHVImports |> Set.unionMany)
    | MethodCall(jRHV,jV,jRHVs) -> getRHVImports(jRHV) + getVariableImports(jV) + ( jRHVs |>List.map getRHVImports |> Set.unionMany)
    | OwnMethodCall(m,parameters) -> getVariableImports(m)
    | OperatorCall(jLRHV,jOper,jRRHV) -> getRHVImports(jLRHV) + getRHVImports(jRRHV)
    
  let rec private getStatementImports (jStatement: JStatement)= 
    match jStatement with
      | JStatement.DeclarationAssignment(jV,jRv) -> getVariableImports(jV) + getRHVImports(jRv)
      | VariableAssignment (jVar,jRHV) -> getVariableImports(jVar) + getRHVImports(jRHV)
      | FieldAssignment(jAc,jRHV) -> GetTypeImportsFieldAccess(jAc) + getRHVImports(jRHV)
      | MultipleStatement(statements) -> statements |> List.map getStatementImports |> Set.unionMany
      | IfThenBlock(cond,ifBlock,thenBlock) -> getRHVImports(cond)+getStatementImports(ifBlock) + getStatementImports(thenBlock)
      | RHVStatement(jRHV) -> getRHVImports(jRHV)
      | EmptyStatement -> Set.empty
      | ReturnStatement rHv-> getRHVImports(rHv)
      | ReturnStatementVoid -> Set.empty
      | Foreach(iter,coll,action)-> getVariableImports(iter) + getRHVImports(coll) + getStatementImports(action)

  let private getMethodImports (jMethod: JMethod)= 
    let importsParameters = jMethod.Parameters |> List.map getVariableImports |> Set.unionMany
    let importsStatements = getStatementImports jMethod.Statements 
    (getTypeImports jMethod.JType) + importsParameters + importsStatements
  let private getConstructorImports(jConstructor: JConstructor)= 
    let importsParameters = jConstructor.Parameters |> List.map getVariableImports |> Set.unionMany
    let importsStatements = getStatementImports jConstructor.Statements 
    (getTypeImports jConstructor.JType) + importsParameters + importsStatements
     
  let public GetClassImports (jClass: JClass)= 
    let fieldImports = jClass.Fields |> List.map getVariableImports
    let methodImports = jClass.Methods |> List.map getMethodImports
    let constructorImports = jClass.Constructors |> List.map getConstructorImports
    let allImports = (Set.empty::fieldImports) @ methodImports @ constructorImports |> Set.unionMany
    let notClassItself = allImports |> Set.filter (fun m -> jClass.JType |> m.Equals |> not)
    notClassItself |> Set.map getImportFromType |> Set.unionMany