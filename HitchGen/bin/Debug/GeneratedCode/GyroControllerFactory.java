public class GyroControllerFactory{


  HashList<GyroController> generatedGyroController;


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


}
