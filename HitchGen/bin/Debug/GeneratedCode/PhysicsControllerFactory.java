public class PhysicsControllerFactory{


  ArrayList<PhysicsController> generatedPhysicsControllers;
  HashMap<PhysicsController,Integer> savedMapping;
  boolean savedChildren;


  public PhysicsControllerFactory(){
    this.generatedPhysicsControllers = new ArrayList<PhysicsController>();
  }


  public PhysicsController ConstructPhysicsController(){
    PhysicsController physicsController = new PhysicsController();
    generatedPhysicsControllers.Add(physicsController);
    return physicsController;
  }
  
  public PhysicsController DestructPhysicsController(){
    generatedPhysicsControllers.Remove(physicsController);
  }
  
  public int GetIdPhysicsController(PhysicsController key){
    return savedMapping.Get(key);
  }
  
  public void SavePhysicsController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(PhysicsController physicsController: generatedPhysicsControllers){
    }
    saveMap.Put("PhysicsController", saveList);
  }
  
  public void FinishSavingPhysicsController(){
    savedMapping = false;
  }


}
