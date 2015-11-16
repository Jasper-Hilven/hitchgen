public class FuelControllerFactory{


  HashList<FuelController> generatedFuelController;
  HashMap<FuelController,Integer> savedMapping;
  boolean savedChildren;


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
  
  public Int GetIdFuelController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingFuelController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
