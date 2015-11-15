public class AirControllerFactory{


  HashList<AirController> generatedAirController;


  public AirControllerFactory(){
    this.generatedAirController= new HashList<AirController>();
  }


  public AirController ConstructAirController(){
    return new AirController();
  }
  
  public AirController DestructAirController(){
  }


}
