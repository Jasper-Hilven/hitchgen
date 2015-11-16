public class FuelControllerFactory{


  HashList<FuelController> generatedFuelController;


  public FuelControllerFactory(){
    this.generatedFuelController= new HashList<FuelController>();
  }


  public FuelController ConstructFuelController(){
    FuelController fuelController= new FuelController();
    generatedFuelController.Add(fuelController);
    return fuelController;
  }
  
  public FuelController DestructFuelController(){
    generatedFuelController.Remove(fuelController);
  }


}
