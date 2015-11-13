public class SpaceShipFactory{


    PhysicsControllerFactory physicsControllerFactory;
    UIControllerFactory uIControllerFactory;
    SystemControllerFactory systemControllerFactory;


  publicSpaceShipFactory(PhysicsControllerFactory physicsControllerFactory, UIControllerFactory uIControllerFactory, SystemControllerFactory systemControllerFactory){
  
      this.physicsControllerFactory= physicsControllerFactory;
      this.uIControllerFactory= uIControllerFactory;
      this.systemControllerFactory= systemControllerFactory;
  
  }




}
