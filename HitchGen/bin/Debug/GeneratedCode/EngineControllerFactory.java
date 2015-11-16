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
  
  public Int GetIdEngineController(Int key){
    return savedMapping.Get(key);
  }
  
  public void SaveEngineController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(EngineController engineController: generatedEngineControllers){
    ;
    }
  }
  
  public void FinishSavingEngineController(){
    savedMapping = false;
  }


}
