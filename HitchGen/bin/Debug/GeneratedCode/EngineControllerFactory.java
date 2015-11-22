public class EngineControllerFactory{


  ArrayList<EngineController> generatedEngineControllers;
  HashMap<EngineController,Integer> savedMapping;
  boolean savedChildren;


  public EngineControllerFactory(){
    this.generatedEngineControllers = new ArrayList<EngineController>();
  }


  public EngineController ConstructEngineController(){
    EngineController engineController = new EngineController();
    generatedEngineControllers.Add(engineController);
    return engineController;
  }
  
  public EngineController DestructEngineController(){
    generatedEngineControllers.Remove(engineController);
  }
  
  public int GetIdEngineController(EngineController key){
    return savedMapping.Get(key);
  }
  
  public void SaveEngineController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(EngineController engineController: generatedEngineControllers){
    }
    saveMap.Put("EngineController", saveList);
  }
  
  public void FinishSavingEngineController(){
    savedMapping = false;
  }


}
