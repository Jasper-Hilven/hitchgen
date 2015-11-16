public class SpaceShipFactory{


  HashList<SpaceShip> generatedSpaceShip;
  PhysicsControllerFactory physicsControllerFactory;
  UIControllerFactory uIControllerFactory;
  SystemControllerFactory systemControllerFactory;


  public SpaceShipFactory(PhysicsControllerFactory physicsControllerFactory, UIControllerFactory uIControllerFactory, SystemControllerFactory systemControllerFactory){
    this.physicsControllerFactory= physicsControllerFactory;
    this.uIControllerFactory= uIControllerFactory;
    this.systemControllerFactory= systemControllerFactory;
    this.generatedSpaceShip= new HashList<SpaceShip>();
  }


  public SpaceShip ConstructSpaceShip(){
    PhysicsController physicsController= physicsControllerFactory.ConstructPhysicsController();
    UIController uIController= uIControllerFactory.ConstructUIController();
    SystemController systemController= systemControllerFactory.ConstructSystemController();
    SpaceShip spaceShip= new SpaceShip(physicsController, uIController, systemController);
    generatedSpaceShip.Add(spaceShip);
    return spaceShip;
  }
  
  public SpaceShip DestructSpaceShip(){
    generatedSpaceShip.Remove(spaceShip);
    physicsControllerFactory.DestructPhysicsController(spaceShip.GetPhysicsController());
    uIControllerFactory.DestructUIController(spaceShip.GetUIController());
    systemControllerFactory.DestructSystemController(spaceShip.GetSystemController());
  }


}
