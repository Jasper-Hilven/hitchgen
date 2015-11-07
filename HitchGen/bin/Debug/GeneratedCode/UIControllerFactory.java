public class UIControllerFactory {


  public ArrayList<UIController> constructedChildren;

  public UIControllerFactory(){

    this.constructedChildren= new ArrayList<UIController>();

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
