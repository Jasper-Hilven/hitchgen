﻿module Controllers

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

let controllersAndProperties = 
  [SpaceShip,"SpaceShip";
  PhysicsController,"PhysicsController";
  UIController,"UIController";
  SystemController,"SystemController";
  EngineController,"EngineController"; 
  GyroController,"GyroController";
  FuelController,"FuelController";
  AirController,"AirController";
  ElectroController,"ElectroController"] |> Map.ofList
let controllers = controllersAndProperties |> Map.toList |> List.map fst 
let controllerNames = controllersAndProperties
let controllerHierarchy = Map.empty
                             .Add(SpaceShip,[PhysicsController;UIController;SystemController])
                             .Add(SystemController,[EngineController;GyroController;FuelController;ElectroController;AirController])

let GetControllerChildren parent = if controllerHierarchy.ContainsKey(parent) then controllerHierarchy.Item(parent) else [] 
let GetControllerName controller = controllerNames.Item(controller)


type Controller with
  member this.Name = GetControllerName(this)
  member this.Children = GetControllerChildren(this)