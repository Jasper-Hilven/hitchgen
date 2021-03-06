﻿module PropertyLanguage
open ControllerDefinitions
type Prop =
//Physics
| Mass
| Position
| Speed
| Impulses
| Orientation
| Rotation
| RotationSpeed
| RotationImpulses
| PassedRotationTime
| IsRotationTurn
//Fuel
| Fuel
| FuelCapacity
//Electricity
| ElectricEnergy
| ElectricMaximum
//Air
| Air
| AirMaximum

let spaceShipPhysicsMembers = 
  [Prop.Mass, Prop.Position, Prop.Speed, Prop.Impulses, Prop.PassedRotationTime, Prop.IsRotationTurn, Prop.Orientation, Prop.Rotation, Prop.RotationImpulses]

type PropContentLangConsts = 
| SingleRotationTime

type PropContentLang =
| Prev of PropContentLang
| Oper of Operation
| Value of Prop
| BoolVal of BoolValue
| TPF
| CollectCumulative of Operation
| CurrentCumulative
| AddToCumulative
| Const of PropContentLangConsts
| If of BoolValue * PropContentLang * PropContentLang

and Operation =
| Mult of PropContentLang * PropContentLang
| Add of PropContentLang * PropContentLang
| Subtr of PropContentLang * PropContentLang
| Div of PropContentLang * PropContentLang
  member this.GetOperators() = 
    match this with
      | Mult(p1,p2) -> [p1;p2]
      | Add (p1,p2) -> [p1;p2]
      | Subtr (p1,p2) -> [p1;p2]
      | Div (p1,p2) -> [p1;p2]

and BeginValue = 
|Null
|Bool of BoolValue
|UnitQuaternion
and BoolValue = 
  | True
  | False
  | Cast of PropContentLang
  | GreaterThan of PropContentLang * PropContentLang
  | Eq of PropContentLang * PropContentLang
  member this.GetOperators() = 
    match this with
    |  True -> []
    |  False-> []
    |  Cast p -> [p]
    |  GreaterThan(a,b) -> [a;b]
    |  Eq(a,b) -> [a;b]
  
type Initialization = 
|  InitialOrGiven of BeginValue
|  Initial of BeginValue

type UpdateFunction = 
|  TimeUpdateFunction of PropContentLang

type PropertyValueDef(initialization  :Initialization, updateFunction : UpdateFunction) = 
  member this.initialization = initialization
  member this.updateFunction = updateFunction


