public class AirControllerFactory{


  ArrayList<AirController> generatedAirControllers;
  HashMap<AirController,Integer> savedMapping;
  boolean savedChildren;


  public AirControllerFactory(){
    this.generatedAirControllers = new ArrayList<AirController>();
  }


  public AirController ConstructAirController(){
    AirController airController = new AirController();
    generatedAirControllers.Add(airController);
    return airController;
  }
  
  public AirController DestructAirController(){
    generatedAirControllers.Remove(airController);
  }
  
  public int GetIdAirController(AirController key){
    return savedMapping.Get(key);
  }
  
  public void SaveAirController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(AirController airController: generatedAirControllers){
    }
    saveMap.Put("AirController", saveList);
  }
  
  public void FinishSavingAirController(){
    savedMapping = false;
  }


}
