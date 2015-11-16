public class ElectroControllerFactory{


  HashList<ElectroController> generatedElectroController;


  public ElectroControllerFactory(){
    this.generatedElectroController= new HashList<ElectroController>();
  }


  public ElectroController ConstructElectroController(){
    ElectroController electroController= new ElectroController();
    generatedElectroController.Add(electroController);
    return electroController;
  }
  
  public ElectroController DestructElectroController(){
    generatedElectroController.Remove(electroController);
  }


}
