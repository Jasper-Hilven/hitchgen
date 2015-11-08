public class UIControllerFactory {


  public ArrayList<UIController> constructedChildren;
  public HashMap<UIController, Integer> ChildToNumber;

  public UIControllerFactory(){

    this.constructedChildren= new ArrayList<UIController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<UIController, Integer>();
    for(UIController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public UIController CreateUIController(){
    UIController result= new UIController();
    constructedChildren.Add(result);
    return result;
  }

  public UIController DestroyUIController(UIController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
