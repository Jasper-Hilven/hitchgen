public class FuelControllerFactory {


  public ArrayList<FuelController> constructedChildren;
  public HashMap<FuelController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public FuelControllerFactory(){

    this.constructedChildren= new ArrayList<FuelController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<FuelController, Integer>();
    for(FuelController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public void RemoveChildNumberCollection(){
    ChildToNumber= null;
    countedFactory= false;
    StoredFactoryChildren= false;
  }

  public FuelController CreateFuelController(){
    FuelController result= new FuelController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public FuelController DestroyFuelController(FuelController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
