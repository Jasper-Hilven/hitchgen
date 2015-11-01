public class SpaceShipFactory {


  PhysicsControllerFactory physicsControllerFactory;
  UIControllerFactory uIControllerFactory;
  SystemControllerFactory systemControllerFactory;

  public SpaceShipFactory(physicsControllerFactory, uIControllerFactory, systemControllerFactory){

        this.physicsControllerFactory= physicsControllerFactory;
        this.uIControllerFactory= uIControllerFactory;
        this.systemControllerFactory= systemControllerFactory;

  }

  public SpaceShip CreateSpaceShip(){
    PhysicsController physicsController= physicsControllerFactory.CreatePhysicsController();
    UIController uIController= uIControllerFactory.CreateUIController();
    SystemController systemController= systemControllerFactory.CreateSystemController();
    return new SpaceShip(physicsController, uIController, systemController);
  }

}
