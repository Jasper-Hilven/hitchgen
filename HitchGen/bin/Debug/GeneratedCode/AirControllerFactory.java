public class AirControllerFactory{


  HashList<AirController> generatedAirControllers;
  HashMap<AirController,Integer> savedMapping;
  boolean savedChildren;


  public AirControllerFactory(){
    this.generatedAirControllers = new HashList<AirController>();
  }


  public AirController ConstructAirController(){
    AirController airController = new AirController();
    generatedAirControllers.Add(airController);
    return airController;
  }
  
  public AirController DestructAirController(){
    generatedAirControllers.Remove(airController);
  }
  
  public int GetIdAirController(int key){
    return savedMapping.Get(key);
  }
  
  public void SaveAirController(HashMap<String,HashList<String>> saveList){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(AirController airController: generatedAirControllers){
    }
  }
  
  public void FinishSavingAirController(){
    savedMapping = false;
  }


}
