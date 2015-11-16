public class AirControllerFactory{


  HashList<AirController> generatedAirController;
  HashMap<AirController,Integer> savedMapping;
  boolean savedChildren;


  public AirControllerFactory(){
    this.generatedAirController= new HashList<AirController>();
  }


  public AirController ConstructAirController(){
    AirController airController= new AirController();
    generatedAirController.Add(airController);
    return airController;
  }
  
  public AirController DestructAirController(){
    generatedAirController.Remove(airController);
  }
  
  public Int GetIdAirController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingAirController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
