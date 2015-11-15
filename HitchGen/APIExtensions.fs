module APIExtensions
open JAPI
open JAST


let GetFieldNameType(fieldType: JAST.JType) = 
  let fieldTypeName = fieldType.GetStringRep()
  fieldTypeName.Substring(0,1).ToLower()+ fieldTypeName.Substring(1)
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

