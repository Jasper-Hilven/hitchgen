public class EngineControllerFactory{


  HashList<EngineController> generatedEngineControllers;
  HashMap<EngineController,Integer> savedMapping;
  boolean savedChildren;


  public EngineControllerFactory(){
    this.generatedEngineControllers = new HashList<EngineController>();
  }


  public EngineController ConstructEngineController(){
    EngineController engineController = new EngineController();
    generatedEngineControllers.Add(engineController);
    return engineController;
  }
  
  public EngineController DestructEngineController(){
    generatedEngineControllers.Remove(engineController);
  }
  
  public int GetIdEngineController(int key){
    return savedMapping.Get(key);
  }
  
  public void SaveEngineController(HashMap<String,HashList<String>> saveList){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(EngineController engineController: generatedEngineControllers){
    }
  }
  
  public void FinishSavingEngineController(){
    savedMapping = false;
  }


}
