module JAPI

open JAST
///TYPES        
let GetListTypeOf(elementType:JType) = JType.List(elementType)
let GetMapTypeOf(keyType:JType,valueType:JType) = JType.Map(keyType,valueType)
let GetFreeType(freeType:string) = Dedicated(freeType)
///VARIABLES
let GetVariable(vName:string,vType: JType) = JVariable(vName, vType)     
///EXPRESSION
let GetFieldEval(field:JVariable) = FieldEval(field) 

////STATEMENT
let GetIfThenBlock(condition : JRightHandValue, thenBlock : JStatement) = IfThenBlock(condition,thenBlock,EmptyStatement) 
let GetIfThenElseBlock(condition : JRightHandValue, thenBlock : JStatement, elseBlock : JStatement) = IfThenBlock(condition,thenBlock,elseBlock) 
let GetAssignment(assignTo : JVariable, valueToAssign : JRightHandValue) = VariableAssignment(assignTo,valueToAssign) 
let GetDeclAssignment(assignTo : JVariable, valueToAssign : JRightHandValue) =  DeclarationAssignment(assignTo,valueToAssign)
let GetReturnStatement(returningValue: JRightHandValue) = ReturnStatement(returningValue)
let GetForeach(iterateElement:JVariable,collection : JRightHandValue,innerExpression : JStatement) = Foreach(iterateElement,collection,innerExpression)

//OO
let GetAccessField(owner:JVariable, fieldName:JVariable) =AccessField(owner,fieldName)
let GetConstructorCall(classType: JType, parameters : JVariable list) = ConstrCall(classType,parameters)
let GetCallOnObject(calledObject: JVariable, methodRef: JVariable, parameters: JVariable list) = MethodCall(calledObject,methodRef,parameters)

////METHODDECLARATION

let GetMethodDeclaration(name:string,jType : JType, parameters : JVariable list,statements : JStatement list) = JMethod(name,jType,parameters, statements) 
