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
  let rec private GetImportFromType(jType: JType) = 
    match jType with
      | String -> Set.empty
      | Bool -> Set.empty
      | Int -> Set.empty
      | Void -> Set.empty
      | List iType -> Set.add AListPath (GetImportFromType iType)
      | Map(k,v)-> Set.empty.Add(HMapPath) + (GetImportFromType k) + (GetImportFromType v)
      | Dedicated(name, import) -> Set.empty.Add(JModule.Child(import,name))
  

  let GetVariableImports (jVariable: JVariable)= getTypeImports(jVariable.JType)
  
  let rec GetTypeImportsFieldAccess (jAF: JAccessField ) = 
    (getRHVImports jAF.JOn) + GetVariableImports(jAF.JField)

  and private getRHVImports (jRV: JRightHandValue) = 
    match jRV with
    | Value v -> Set.empty
    | Eval jV -> GetVariableImports jV 
    | FieldEval jV -> GetVariableImports jV 
    | AccessField aF-> GetTypeImportsFieldAccess aF
    | ConstrCall(t,rhvs) -> getTypeImports(t) + (rhvs |> List.map getRHVImports |> Set.unionMany)
    | MethodCall(jRHV,jV,jRHVs) -> getRHVImports(jRHV) + GetVariableImports(jV) + ( jRHVs |>List.map getRHVImports |> Set.unionMany)
    | OwnMethodCall(m,parameters) -> GetVariableImports(m)
    | OperatorCall(jLRHV,jOper,jRRHV) -> getRHVImports(jLRHV) + getRHVImports(jRRHV)
    
  let rec private getStatementImports (jStatement: JStatement)= 
    match jStatement with
      | JStatement.DeclarationAssignment(jV,jRv) -> GetVariableImports(jV) + getRHVImports(jRv)
      | VariableAssignment (jVar,jRHV) -> GetVariableImports(jVar) + getRHVImports(jRHV)
      | FieldAssignment(jAc,jRHV) -> GetTypeImportsFieldAccess(jAc) + getRHVImports(jRHV)
      | MultipleStatement(statements) -> statements |> List.map getStatementImports |> Set.unionMany
      | IfThenBlock(cond,ifBlock,thenBlock) -> getRHVImports(cond)+getStatementImports(ifBlock) + getStatementImports(thenBlock)
      | RHVStatement(jRHV) -> getRHVImports(jRHV)
      | EmptyStatement -> Set.empty
      | ReturnStatement rHv-> getRHVImports(rHv)
      | ReturnStatementVoid -> Set.empty
      | Foreach(iter,coll,action)-> GetVariableImports(iter) + getRHVImports(coll) + getStatementImports(action)

  let private getMethodImports (jMethod: JMethod)= 
    let importsParameters = jMethod.Parameters |> List.map GetVariableImports |> Set.unionMany
    let importsStatements = getStatementImports jMethod.Statements 
    (getTypeImports jMethod.JType) + importsParameters + importsStatements
  
  let private getConstructorImports(jConstructor: JConstructor)= 
    let importsParameters = jConstructor.Parameters |> List.map GetVariableImports |> Set.unionMany
    let importsStatements = getStatementImports jConstructor.Statements 
    (getTypeImports jConstructor.JType) + importsParameters + importsStatements
     
  let public GetClassImports (jClass: JClass)= 
    let fieldImports = jClass.Fields |> List.map GetVariableImports
    let methodImports = jClass.Methods |> List.map getMethodImports
    let constructorImports = jClass.Constructors |> List.map getConstructorImports
    let allImports = (Set.empty::fieldImports) @ methodImports @ constructorImports |> Set.unionMany
    let compareClassToModule = (fun m -> jClass.JType |> getTypeImports |> m.Equals |> not)
    let notClassItself = allImports |> Set.filter compareClassToModule
    notClassItself |> Set.map GetImportFromType |> Set.unionMany
    