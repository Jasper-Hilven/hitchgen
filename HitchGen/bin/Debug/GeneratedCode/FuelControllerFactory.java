public class FuelControllerFactory{


  ArrayList<FuelController> generatedFuelControllers;
  HashMap<FuelController,Integer> savedMapping;
  boolean savedChildren;


  public FuelControllerFactory(){
    this.generatedFuelControllers = new ArrayList<FuelController>();
  }


  public FuelController ConstructFuelController(){
    FuelController fuelController = new FuelController();
    generatedFuelControllers.Add(fuelController);
    return fuelController;
  }
  
  public FuelController DestructFuelController(){
    generatedFuelControllers.Remove(fuelController);
  }
  
  public int GetIdFuelController(FuelController key){
    return savedMapping.Get(key);
  }
  
  public void SaveFuelController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(FuelController fuelController: generatedFuelControllers){
    }
    saveMap.Put("FuelController", saveList);
  }
  
  public void FinishSavingFuelController(){
    savedMapping = false;
  }


}
