public class PhysicsControllerFactory{


  HashList<PhysicsController> generatedPhysicsController;
  HashMap<PhysicsController,Integer> savedMapping;
  boolean savedChildren;


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
  
  public Int GetIdPhysicsController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingPhysicsController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
