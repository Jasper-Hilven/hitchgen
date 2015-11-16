public class AirControllerFactory{


  HashList<AirController> generatedAirController;


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


}
