module PropertyDefinitions
open PropertyLanguage
open ControllerDefinitions

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
   .Add(Controller.EngineController, [])

