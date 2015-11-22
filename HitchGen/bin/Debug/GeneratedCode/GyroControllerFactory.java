public class GyroControllerFactory{


  ArrayList<GyroController> generatedGyroControllers;
  HashMap<GyroController,Integer> savedMapping;
  boolean savedChildren;


  public GyroControllerFactory(){
    this.generatedGyroControllers = new ArrayList<GyroController>();
  }


  public GyroController ConstructGyroController(){
    GyroController gyroController = new GyroController();
    generatedGyroControllers.Add(gyroController);
    return gyroController;
  }
  
  public GyroController DestructGyroController(){
    generatedGyroControllers.Remove(gyroController);
  }
  
  public int GetIdGyroController(GyroController key){
    return savedMapping.Get(key);
  }
  
  public void SaveGyroController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(GyroController gyroController: generatedGyroControllers){
    }
    saveMap.Put("GyroController", saveList);
  }
  
  public void FinishSavingGyroController(){
    savedMapping = false;
  }


}
