public class UIControllerFactory{


  HashList<UIController> generatedUIController;
  HashMap<UIController,Integer> savedMapping;
  boolean savedChildren;


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
  
  public Int GetIdUIController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingUIController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
