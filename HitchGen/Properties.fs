module Properties
open Controllers
type Prop =
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

| Fuel
| FuelCapacity

| ElectricEnergy
| ElectricMaximum

| Air
| AirMaximum

let spaceShipPhysicsMembers = 
  [Prop.Mass, Prop.Position, Prop.Speed, Prop.Impulses, Prop.PassedRotationTime, 
   Prop.IsRotationTurn, Prop.Orientation, Prop.Rotation, Prop.RotationImpulses]

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

type Initialization = 
|  InitialOrGiven of BeginValue
|  Initial of BeginValue

type UpdateFunction = 
|  TimeUpdateFunction of PropContentLang

type PropertyValueDef(initialization :Initialization, updateFunction : UpdateFunction) = 
  member this.initialization = initialization
  member this.updateFunction = updateFunction

let propertyValueDefinitions = 
  Map.empty.Add(
    Controller.PhysicsController,  
    [Prop.Position, PropertyValueDef(Initialization.InitialOrGiven(BeginValue.Null),UpdateFunction.TimeUpdateFunction(Oper(Add(Prev(Value(Position)),Oper(Mult(TPF,Value(Speed))))))); 
     Prop.Speed, PropertyValueDef(Initialization.InitialOrGiven(BeginValue.Null), UpdateFunction.TimeUpdateFunction(Oper(Add(Prev(Value(Speed)),Oper(Div(Value(Impulses),Value(Mass)))))));
     Prop.Impulses, PropertyValueDef(Initialization.Initial(BeginValue.Null),UpdateFunction.TimeUpdateFunction(CollectCumulative(Operation.Add(CurrentCumulative,AddToCumulative))));
     Prop.Mass, PropertyValueDef(Initialization.Initial(Null),UpdateFunction.TimeUpdateFunction(CollectCumulative(Operation.Add(CurrentCumulative,AddToCumulative))));
     Prop.PassedRotationTime, PropertyValueDef(
       Initialization.Initial(Null),
       UpdateFunction.TimeUpdateFunction(If(GreaterThan(Oper(Add(Prev(Value(PassedRotationTime)),TPF)),Const(SingleRotationTime)), 
                                            Oper(Subtr(Oper(Add(Prev(Value(PassedRotationTime)),TPF)),Const(SingleRotationTime))),
                                            Oper(Add(Prev(Value(PassedRotationTime)),TPF)))));
     Prop.IsRotationTurn, PropertyValueDef(
       Initialization.Initial(Bool(False)),
       UpdateFunction.TimeUpdateFunction(BoolVal(Eq(
                                                    Oper(Add(Prev(Value(Prop.PassedRotationTime)),TPF)),
                                                    Oper(Add(Value(PassedRotationTime),Const(SingleRotationTime)))))));
    Prop.Rotation, PropertyValueDef(Initial(UnitQuaternion),
                                    TimeUpdateFunction(If(BoolValue.Cast(Value(Prop.IsRotationTurn)),
                                                          Oper(Mult(Prev(Value(Prop.Rotation)),Value(Prop.RotationSpeed))),
                                                          Prev(Value(Prop.Rotation)))));
   Prop.RotationSpeed, PropertyValueDef(InitialOrGiven(UnitQuaternion), TimeUpdateFunction(Oper(Mult(Prev(Value(Prop.RotationSpeed)),Value(RotationImpulses)))));
   Prop.RotationImpulses, PropertyValueDef(Initial(BeginValue.UnitQuaternion),TimeUpdateFunction(Prev(Value(Prop.RotationImpulses))))])
   .Add(Controller.EngineController,[])

