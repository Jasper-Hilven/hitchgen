public class PhysicsControllerFactory{


  HashList<PhysicsController> generatedPhysicsController;


  public PhysicsControllerFactory(){
    this.generatedPhysicsController= new HashList<PhysicsController>();
  }


  public PhysicsController ConstructPhysicsController(){
    PhysicsController physicsController= new PhysicsController();
    generatedPhysicsController.Add(physicsController);
    return physicsController;
  }
  
  public PhysicsController DestructPhysicsController(){
    generatedPhysicsController.Remove(physicsController);
  }


}
