public class AirControllerFactory {


  public ArrayList<AirController> constructedChildren;
  public HashMap<AirController, Integer> ChildToNumber;

  public AirControllerFactory(){

    this.constructedChildren= new ArrayList<AirController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<AirController, Integer>();
    for(AirController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
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
