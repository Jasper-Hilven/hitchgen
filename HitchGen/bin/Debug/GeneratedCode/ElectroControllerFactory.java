public class ElectroControllerFactory{


  HashList<ElectroController> generatedElectroController;


  public ElectroControllerFactory(){
    this.generatedElectroController= new HashList<ElectroController>();
  }


  public ElectroController ConstructElectroController(){
    return new ElectroController();
  }
  
  public ElectroController DestructElectroController(){
  }


}
