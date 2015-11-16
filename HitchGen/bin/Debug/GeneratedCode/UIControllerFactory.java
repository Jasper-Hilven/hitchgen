public class UIControllerFactory{


  HashList<UIController> generatedUIControllers;
  HashMap<UIController,Integer> savedMapping;
  boolean savedChildren;


  public UIControllerFactory(){
    this.generatedUIControllers = new HashList<UIController>();
  }


  public UIController ConstructUIController(){
    UIController uIController = new UIController();
    generatedUIControllers.Add(uIController);
    return uIController;
  }
  
  public UIController DestructUIController(){
    generatedUIControllers.Remove(uIController);
  }
  
  public Int GetIdUIController(Int key){
    return savedMapping.Get(key);
  }
  
  public void SaveUIController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(UIController uIController: generatedUIControllers){
    ;
    }
  }
  
  public void FinishSavingUIController(){
    savedMapping = false;
  }


}
