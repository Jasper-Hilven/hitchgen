public class SpaceShip {


  PhysicsController physicsController;
  UIController uIController;
  SystemController systemController;

  public SpaceShip(PhysicsController physicsController, UIController uIController, SystemController systemController){

    this.physicsController = physicsController;
    this.uIController = uIController;
    this.systemController = systemController;

  }

}
