public class EngineControllerFactory {


  public ArrayList<EngineController> constructedChildren;
  public HashMap<EngineController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public EngineControllerFactory(){

    this.constructedChildren= new ArrayList<EngineController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<EngineController, Integer>();
    for(EngineController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public void RemoveChildNumberCollection(){
    ChildToNumber= null;
    countedFactory= false;
    StoredFactoryChildren= false;
  }

  public EngineController CreateEngineController(){
    EngineController result= new EngineController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public EngineController DestroyEngineController(EngineController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
