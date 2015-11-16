public class UIControllerFactory{


  HashList<UIController> generatedUIController;


  public UIControllerFactory(){
    this.generatedUIController= new HashList<UIController>();
  }


  public UIController ConstructUIController(){
    UIController uIController= new UIController();
    generatedUIController.Add(uIController);
    return uIController;
  }
  
  public UIController DestructUIController(){
    generatedUIController.Remove(uIController);
  }


}
