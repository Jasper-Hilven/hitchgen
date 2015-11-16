public class GyroControllerFactory{


  HashList<GyroController> generatedGyroController;
  HashMap<GyroController,Integer> savedMapping;
  boolean savedChildren;


  public GyroControllerFactory(){
    this.generatedGyroController= new HashList<GyroController>();
  }


  public GyroController ConstructGyroController(){
    GyroController gyroController= new GyroController();
    generatedGyroController.Add(gyroController);
    return gyroController;
  }
  
  public GyroController DestructGyroController(){
    generatedGyroController.Remove(gyroController);
  }
  
  public Int GetIdGyroController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingGyroController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
