module JavaSyntaxParser

//////TYPES
type JType = string
type JVariable ={vName : string; vType : JType}
let CreateJVariable(vName,vType) = {vName = vName; vType =vType}
let GetTypeString(controllerName:string) = controllerName
///EXPRESSION
let GetFieldExpression(fieldName:string) = "    this." + fieldName

////STATEMENT
let GetAssignment(AssignTo : string, valueToAssign : string) = "    " + AssignTo + "= " + valueToAssign + ";" 
let GetDeclAssignment(AssignTo : JVariable, valueToAssign : string) = GetAssignment(AssignTo.vType + " " + AssignTo.vName,valueToAssign)
let returnStatement(returningValue: string) = "    return " + returningValue + ";"

//////FIELD
let GetFieldDeclaration(typeString,nameString) = "  " + typeString + " " + nameString + ";"

let GetFieldName(controllerName:string) = if controllerName.Length.Equals(0) then controllerName else controllerName.Substring(0,1).ToLower()+ controllerName.Substring(1)

let GetFieldAssignment(fieldName:string,fieldValue:string) = GetAssignment(GetFieldExpression(fieldName),fieldValue)
let GetFieldDeclarations childrenNames = childrenNames |> Seq.map (
    fun childName -> 
      let childFieldName = GetFieldName(childName)
      let childType = GetTypeString(childName)
      GetFieldDeclaration(childType,childFieldName))
//VARIABLES
let GetVariableDeclarationAssignment(toCreate : JVariable, assignedValue : string) = "    " + toCreate.vType + " "+ toCreate.vName + "= " + assignedValue + ";"
//METHODCALL

let GetParameterDescriptions(variables: string seq) = 
  let variableText = variables |> Seq.toList 
  if(variableText.Length > 0 ) then variableText |> Seq.reduce(fun x y -> x + ", " + y) else ""


let GetConstructorCall(toCreateType: JType, parameters : string seq) = "new " + toCreateType + "(" + GetParameterDescriptions(parameters) + ")"
let GetCallOnObject(calledObject: string, methodName: string, parameters: string seq) = calledObject + "." + methodName + "(" + GetParameterDescriptions(parameters) + ")" 

////METHODDECLARATION
let GetParameterDeclarationDescriptions(variables: JVariable seq) = variables |> Seq.map (fun jv -> jv.vType + " "+jv.vName) |> GetParameterDescriptions

let GetMethodDeclaration(declaration : JVariable , parameters: JVariable seq,content : string seq) = 
  let methodHeader = "  public " + declaration.vType + " "+ declaration.vName + "(" + GetParameterDeclarationDescriptions(parameters) + "){"
  [methodHeader] @ (content |> Seq.toList) @ ["  }"]  
//CLASS
let GetClassContentDescription(constructorParameter: string,
                               constructorAssignments: string seq,
                               fieldDeclarations: string seq,
                               methods : string seq,
                               className: string) = 
  let classHeader = ["public class " + className + " {"]
  let constructorHeader = ["  public " + className + "(" + constructorParameter+ "){"]
  let constructorTail = ["  }"]
  let classTail = ["}"]
  let singleWhiteLine = [""]
  let doubleWhiteLine = singleWhiteLine@ singleWhiteLine
  classHeader @ doubleWhiteLine @
  (fieldDeclarations |> Seq.toList) @ singleWhiteLine @
  constructorHeader @ singleWhiteLine @ (constructorAssignments |> Seq.toList) @ singleWhiteLine @ constructorTail @ singleWhiteLine @ 
  (methods |> Seq.toList) @ singleWhiteLine @
  classTail
type ClassFile = {Name : string; ContainedNameSpace : string; ClassContent :string seq}
let GetClassFile(name : string, containedNameSpace : string, classContent : string seq) = {Name = name + ".java"; ContainedNameSpace = containedNameSpace; ClassContent = classContent}