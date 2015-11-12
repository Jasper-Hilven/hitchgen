module PropertyGeneration
open PropertyDefinitions
open PropertyLanguage

exception ControllerNotDefined of string

let defs = PropertyDefinitions.propertyValueDefinitions

let GetEmptyRelations() : (Prop*Prop) list = []
let SetComesBefore (relations :  (Prop*Prop) list, before, after) = (before,after) :: relations
let GetSingleRelation(before, after)  = [(before,after)]

let ParseProperties(controller) = 
  
  let GetOrdersFromValueDef(currentProperty : Prop, valueDef: PropertyValueDef) = 
    match valueDef.updateFunction with 
    |TimeUpdateFunction langElement  ->
      
      let rec GetOrderRec(element : PropContentLang) = 
        
        let mapGetReduce(operators) = 
          let recList = operators |> List.map GetOrderRec 
          if recList.IsEmpty then GetEmptyRelations() else List.reduce (fun a b -> a @ b) recList
        
        match element with
        | Prev e -> GetEmptyRelations()
        | Oper operation-> operation.GetOperators() |> mapGetReduce
        | Value value-> GetSingleRelation(currentProperty,value)
        | BoolVal boolVal -> boolVal.GetOperators() |> mapGetReduce
        | TPF -> GetEmptyRelations()
        | CollectCumulative operation -> operation.GetOperators() |> mapGetReduce
        | CurrentCumulative -> GetEmptyRelations()
        | AddToCumulative -> GetEmptyRelations()
        | Const _ -> GetEmptyRelations()
        | If (condition,trueBranch,falseBranch)-> [BoolVal(condition);trueBranch;falseBranch] |> mapGetReduce
      
      GetOrderRec(langElement)
  
  match propertyValueDefinitions.TryFind(controller) with
    | None -> raise(ControllerNotDefined("Controller not yet in map")) 
    | Some properties -> 
      let rec ParseAllRemainingProperties(propertyList : (Prop*PropertyValueDef) list) = 
        match propertyList with
        | [] -> []
        | (prop,propValDef)::rest -> 
        let solveFunction(element) = [0]
        ParseAllRemainingProperties(rest) @ GetOrdersFromValueDef(prop,propValDef)
      ParseAllRemainingProperties(properties)

//Define order of calculations while trying to group items of same Controller


