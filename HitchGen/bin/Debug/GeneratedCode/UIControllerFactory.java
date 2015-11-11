public class UIControllerFactory {


  public ArrayList<UIController> constructedChildren;
  public HashMap<UIController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public UIControllerFactory(){

    this.constructedChildren= new ArrayList<UIController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<UIController, Integer>();
    for(UIController savingElement : constructedChildren){
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

  public UIController CreateUIController(){
    UIController result= new UIController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public UIController DestroyUIController(UIController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
