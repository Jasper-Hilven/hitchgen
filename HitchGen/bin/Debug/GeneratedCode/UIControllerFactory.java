public class UIControllerFactory{


  HashList<UIController> generatedUIController;


  public UIControllerFactory(){
    this.generatedUIController= new HashList<UIController>();
  }


  public UIController ConstructUIController(){
    return new UIController();
  }
  
  public UIController DestructUIController(){
  }


}
