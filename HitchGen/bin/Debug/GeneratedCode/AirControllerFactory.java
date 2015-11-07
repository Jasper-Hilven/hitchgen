public class AirControllerFactory {


  public ArrayList<AirController> constructedChildren;

  public AirControllerFactory(){

    this.constructedChildren= new ArrayList<AirController>();

  }

  public AirController CreateAirController(){
    AirController result= new AirController();
    constructedChildren.Add(result);
    return result;
  }

  public AirController DestroyAirController(AirController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
