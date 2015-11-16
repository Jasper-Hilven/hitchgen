public class EngineControllerFactory{


  HashList<EngineController> generatedEngineController;
  HashMap<EngineController,Integer> savedMapping;
  boolean savedChildren;


  public EngineControllerFactory(){
    this.generatedEngineController= new HashList<EngineController>();
  }


  public EngineController ConstructEngineController(){
    EngineController engineController= new EngineController();
    generatedEngineController.Add(engineController);
    return engineController;
  }
  
  public EngineController DestructEngineController(){
    generatedEngineController.Remove(engineController);
  }
  
  public Int GetIdEngineController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingEngineController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
