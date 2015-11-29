module ControllerDefinitions

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

let private controllersAndProperties = 
  [SpaceShip,"SpaceShip";
  PhysicsController,"PhysicsController";
  UIController,"UIController";
  SystemController,"SystemController";
  EngineController,"EngineController"; 
  GyroController,"GyroController";
  FuelController,"FuelController";
  AirController,"AirController";
  ElectroController,"ElectroController"] |> Map.ofList
let public GetActiveControllers = controllersAndProperties |> Map.toList |> List.map fst 

let private controllerNames = controllersAndProperties
let private controllerHierarchy = Map.empty.Add(SpaceShip,[PhysicsController;UIController;SystemController]).Add(SystemController,[EngineController;GyroController;FuelController;ElectroController;AirController])
let public GetControllerChildren parent = if controllerHierarchy.ContainsKey(parent) then controllerHierarchy.Item(parent) else [] 
let public GetControllerName controller = controllerNames.Item(controller)
let BController = 3

type Controller with
  member this.Name = GetControllerName(this)
  member this.Children = GetControllerChildren(this)