public class GyroControllerFactory{


  HashList<GyroController> generatedGyroController;


  public GyroControllerFactory(){
    this.generatedGyroController= new HashList<GyroController>();
  }


  public GyroController ConstructGyroController(){
    return new GyroController();
  }
  
  public GyroController DestructGyroController(){
  }


}
