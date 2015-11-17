public class FuelControllerFactory{


  HashList<FuelController> generatedFuelControllers;
  HashMap<FuelController,Integer> savedMapping;
  boolean savedChildren;


  public FuelControllerFactory(){
    this.generatedFuelControllers = new HashList<FuelController>();
  }


  public FuelController ConstructFuelController(){
    FuelController fuelController = new FuelController();
    generatedFuelControllers.Add(fuelController);
    return fuelController;
  }
  
  public FuelController DestructFuelController(){
    generatedFuelControllers.Remove(fuelController);
  }
  
  public int GetIdFuelController(int key){
    return savedMapping.Get(key);
  }
  
  public void SaveFuelController(HashMap<String,HashList<String>> saveList){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(FuelController fuelController: generatedFuelControllers){
    }
  }
  
  public void FinishSavingFuelController(){
    savedMapping = false;
  }


}
