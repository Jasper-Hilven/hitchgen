module Controllers


type Controller =
| SpaceShip
| PhysicsController
| UIController
| SystemController
| EngineController
| GyroController
| FuelController
| AirController
| ElectroController

let controllersAndProperties = [SpaceShip,"SpaceShip";PhysicsController,"PhysicsController";
                   UIController,"UIController";SystemController,"SystemController";
                   EngineController,"EngineController"; GyroController,"GyroController";
                   FuelController,"FuelController";AirController,"AirController";
                   ElectroController,"ElectroController"] |> Map.ofList
let controllers : seq<Controller> = controllersAndProperties|> Map.toSeq |> Seq.map fst 
let controllerNames:Map<Controller,string> = controllersAndProperties
let controllerHierarchy = Map.empty
                             .Add(SpaceShip,[PhysicsController;UIController;SystemController])
                             .Add(SystemController,[EngineController;GyroController;FuelController;ElectroController;AirController])
let ControllerChildren parent = if controllerHierarchy.ContainsKey(parent) then controllerHierarchy.Item(parent) else [] 
