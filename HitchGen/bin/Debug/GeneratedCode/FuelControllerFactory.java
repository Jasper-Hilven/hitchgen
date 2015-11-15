public class FuelControllerFactory{


  HashList<FuelController> generatedFuelController;


  public FuelControllerFactory(){
    this.generatedFuelController= new HashList<FuelController>();
  }


  public FuelController ConstructFuelController(){
    return new FuelController();
  }
  
  public FuelController DestructFuelController(){
  }


}
