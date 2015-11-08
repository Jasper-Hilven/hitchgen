public class FuelControllerFactory {


  public ArrayList<FuelController> constructedChildren;
  public HashMap<FuelController, Integer> ChildToNumber;

  public FuelControllerFactory(){

    this.constructedChildren= new ArrayList<FuelController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<FuelController, Integer>();
    for(FuelController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
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
