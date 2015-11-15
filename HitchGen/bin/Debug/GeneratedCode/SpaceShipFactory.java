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
    return new SpaceShip(physicsController, uIController, systemController);
  }
  
  public SpaceShip DestructSpaceShip(){
    physicsControllerFactory.DestructPhysicsController(spaceShip.GetPhysicsController());
    uIControllerFactory.DestructUIController(spaceShip.GetUIController());
    systemControllerFactory.DestructSystemController(spaceShip.GetSystemController());
  }


}
