public class UIControllerFactory{


  ArrayList<UIController> generatedUIControllers;
  HashMap<UIController,Integer> savedMapping;
  boolean savedChildren;


  public UIControllerFactory(){
    this.generatedUIControllers = new ArrayList<UIController>();
  }


  public UIController ConstructUIController(){
    UIController uIController = new UIController();
    generatedUIControllers.Add(uIController);
    return uIController;
  }
  
  public UIController DestructUIController(){
    generatedUIControllers.Remove(uIController);
  }
  
  public int GetIdUIController(UIController key){
    return savedMapping.Get(key);
  }
  
  public void SaveUIController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(UIController uIController: generatedUIControllers){
    }
    saveMap.Put("UIController", saveList);
  }
  
  public void FinishSavingUIController(){
    savedMapping = false;
  }


}
