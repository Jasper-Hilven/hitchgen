public class EngineControllerFactory {


  public ArrayList<EngineController> constructedChildren;
  public HashMap<EngineController, Integer> ChildToNumber;

  public EngineControllerFactory(){

    this.constructedChildren= new ArrayList<EngineController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<EngineController, Integer>();
    for(EngineController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
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
