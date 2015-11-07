public class EngineControllerFactory {


  public ArrayList<EngineController> constructedChildren;

  public EngineControllerFactory(){

    this.constructedChildren= new ArrayList<EngineController>();

  }

  public EngineController CreateEngineController(){
    EngineController result= new EngineController();
    constructedChildren.Add(result);
    return result;
  }

  public EngineController DestroyEngineController(EngineController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
