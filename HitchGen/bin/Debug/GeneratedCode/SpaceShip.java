public class SpaceShip{


  PhysicsController physicsController;
  UIController uIController;
  SystemController systemController;


  public SpaceShip(PhysicsController physicsController, UIController uIController, SystemController systemController){
    this.PhysicsController physicsController=physicsController;
    this.UIController uIController=uIController;
    this.SystemController systemController=systemController;
  }


  public PhysicsController GetPhysicsController(){
    return physicsController;
  }
  
  public UIController GetUIController(){
    return uIController;
  }
  
  public SystemController GetSystemController(){
    return systemController;
  }


}
