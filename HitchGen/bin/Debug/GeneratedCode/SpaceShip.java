public class SpaceShip{


  PhysicsController physicsController;
  UIController uIController;
  SystemController systemController;


  public SpaceShip(PhysicsController physicsController, UIController uIController, SystemController systemController){
    this.physicsController = physicsController;
    this.uIController = uIController;
    this.systemController = systemController;
  }


  public PhysicsController GetPhysicsController(){
    return this.physicsController;
  }
  
  public UIController GetUIController(){
    return this.uIController;
  }
  
  public SystemController GetSystemController(){
    return this.systemController;
  }


}
