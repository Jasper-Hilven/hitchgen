public class PhysicsControllerFactory{


  HashList<PhysicsController> generatedPhysicsController;


  public PhysicsControllerFactory(){
    this.generatedPhysicsController= new HashList<PhysicsController>();
  }


  public PhysicsController ConstructPhysicsController(){
    return new PhysicsController();
  }
  
  public PhysicsController DestructPhysicsController(){
  }


}
