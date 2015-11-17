public class PhysicsControllerFactory{


  HashList<PhysicsController> generatedPhysicsControllers;
  HashMap<PhysicsController,Integer> savedMapping;
  boolean savedChildren;


  public PhysicsControllerFactory(){
    this.generatedPhysicsControllers = new HashList<PhysicsController>();
  }


  public PhysicsController ConstructPhysicsController(){
    PhysicsController physicsController = new PhysicsController();
    generatedPhysicsControllers.Add(physicsController);
    return physicsController;
  }
  
  public PhysicsController DestructPhysicsController(){
    generatedPhysicsControllers.Remove(physicsController);
  }
  
  public int GetIdPhysicsController(int key){
    return savedMapping.Get(key);
  }
  
  public void SavePhysicsController(HashMap<String,HashList<String>> saveList){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(PhysicsController physicsController: generatedPhysicsControllers){
    }
  }
  
  public void FinishSavingPhysicsController(){
    savedMapping = false;
  }


}
