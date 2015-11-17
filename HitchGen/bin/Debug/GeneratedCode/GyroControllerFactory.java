public class GyroControllerFactory{


  HashList<GyroController> generatedGyroControllers;
  HashMap<GyroController,Integer> savedMapping;
  boolean savedChildren;


  public GyroControllerFactory(){
    this.generatedGyroControllers = new HashList<GyroController>();
  }


  public GyroController ConstructGyroController(){
    GyroController gyroController = new GyroController();
    generatedGyroControllers.Add(gyroController);
    return gyroController;
  }
  
  public GyroController DestructGyroController(){
    generatedGyroControllers.Remove(gyroController);
  }
  
  public int GetIdGyroController(int key){
    return savedMapping.Get(key);
  }
  
  public void SaveGyroController(HashMap<String,HashList<String>> saveList){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(GyroController gyroController: generatedGyroControllers){
    }
  }
  
  public void FinishSavingGyroController(){
    savedMapping = false;
  }


}
