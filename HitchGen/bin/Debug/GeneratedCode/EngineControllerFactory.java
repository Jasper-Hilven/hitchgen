public class EngineControllerFactory{


  HashList<EngineController> generatedEngineController;


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


}
