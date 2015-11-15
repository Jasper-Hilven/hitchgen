public class EngineControllerFactory{


  HashList<EngineController> generatedEngineController;


  public EngineControllerFactory(){
    this.generatedEngineController= new HashList<EngineController>();
  }


  public EngineController ConstructEngineController(){
    return new EngineController();
  }
  
  public EngineController DestructEngineController(){
  }


}
