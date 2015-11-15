module JAPI

open JAST

/// TYPES
let GetListTypeOf(elementType:JType) = JType.List(elementType)
let GetMapTypeOf(keyType:JType,valueType:JType) = JType.Map(keyType,valueType)
let GetFreeType(freeType:string) = Dedicated(freeType)
let GetVoidType() = JType.Void

/// VARIABLES
let GetVariable(vName:string,vType: JType) = JVariable(vName, vType)

/// EXPRESSION
let GetFieldEval(field:JVariable) = FieldEval(field) 
let GetVariableEval(var: JVariable) = Eval(var)

/// STATEMENT
let GetIfThenBlock(condition : JRightHandValue, thenBlock : JStatement) = IfThenBlock(condition,thenBlock,EmptyStatement) 
let GetIfThenElseBlock(condition : JRightHandValue, thenBlock : JStatement, elseBlock : JStatement) = IfThenBlock(condition,thenBlock,elseBlock) 
let GetAssignment(assignTo : JVariable, valueToAssign : JRightHandValue) = VariableAssignment(assignTo,valueToAssign) 
let GetCollectStatement(statements : JStatement list) = MultipleStatement(statements)
let GetDeclAssignment(assignTo : JVariable, valueToAssign : JRightHandValue) =  DeclarationAssignment(assignTo,valueToAssign)
let GetRHVstatement(calculatedValue : JRightHandValue) = RHVStatement(calculatedValue)
let GetReturnStatement(returningValue: JRightHandValue) = ReturnStatement(returningValue)
let GetForeach(iterateElement:JVariable,collection : JRightHandValue,innerExpression : JStatement) = Foreach(iterateElement,collection,innerExpression)
let GetSetField(field : JVariable, valueToAssign : JRightHandValue) = FieldAssignment(field,valueToAssign)

/// OO
let GetAccessField(owner:JVariable, fieldName:JVariable) =AccessField(owner,fieldName)
let GetConstructorCall(classType: JType, parameters) = ConstrCall(classType,parameters)
let GetCallOnObject(calledObject: JVariable, methodRef: JVariable, parameters) = MethodCall(calledObject,methodRef,parameters)

/// METHODDECLARATION
let GetMethodDeclaration(name:string,jType : JType, parameters : JVariable list,statements : JStatement) = JMethod(name,jType,parameters, statements) 
let GetConstructorDeclaration(jType: JType, parameters: JVariable list, statements: JStatement) =  JConstructor(jType, parameters, statements)

/// CLASSDECLARATION
let GetClass(name, constructors, methods, fields) = JClass(name,constructors, methods, fields)


