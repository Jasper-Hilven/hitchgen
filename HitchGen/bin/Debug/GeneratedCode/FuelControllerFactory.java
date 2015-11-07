public class FuelControllerFactory {


  public ArrayList<FuelController> constructedChildren;

  public FuelControllerFactory(){

    this.constructedChildren= new ArrayList<FuelController>();

  }

  public FuelController CreateFuelController(){
    FuelController result= new FuelController();
    constructedChildren.Add(result);
    return result;
  }

  public FuelController DestroyFuelController(FuelController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
