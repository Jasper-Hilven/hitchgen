module JavaSyntaxParser


type JVariable ={vName : string; vType : string}
type fieldLine = string
let GetFieldAssignment(fieldName:string,fieldValue:string) = "    this." + fieldName + " = " + fieldValue + ";"
let GetParameterDescriptions(variables: JVariable seq) = 
  let variableText = variables |> Seq.map (fun jv -> jv.vType + " "+jv.vName) |> Seq.toList 
  if(variableText.Length > 0 ) then variableText |> Seq.reduce(fun x y -> x + ", " + y) else ""
let GetFieldDeclaration(typeString,nameString) = "  " + typeString + " " + nameString + ";"
let GetFieldName(controllerName:string) = if controllerName.Length.Equals(0) then controllerName else controllerName.Substring(0,1).ToLower()+ controllerName.Substring(1)
let GetTypeString(controllerName:string) = controllerName
let GetFieldDeclarations childrenNames = childrenNames |> Seq.map (
    fun childName -> 
      let childFieldName = GetFieldName(childName)
      let childType = GetTypeString(childName)
      GetFieldDeclaration(childType,childFieldName))

let GetClassContentDescription(constructorParameter: string, constructorAssignments: string seq, fieldDeclarations: string seq, className: string) = 
  let classHeader = ["public class " + className + " {"]
  let constructorHeader = ["  public " + className + "(" + constructorParameter+ "){"]
  let constructorTail = ["  }"]
  let classTail = ["}"]
  let singleWhiteLine = [""]
  let doubleWhiteLine = singleWhiteLine@ singleWhiteLine
  classHeader @ doubleWhiteLine @
  (fieldDeclarations |> Seq.toList) @ singleWhiteLine @
  constructorHeader @ singleWhiteLine @ (constructorAssignments |> Seq.toList) @ singleWhiteLine @ constructorTail @ singleWhiteLine @ 
  classTail
type ClassFile = {Name : string; ContainedNameSpace : string; ClassContent :string seq}
let GetClassFile(name : string, containedNameSpace : string, classContent : string seq) = {Name = name + ".java"; ContainedNameSpace = containedNameSpace; ClassContent = classContent}