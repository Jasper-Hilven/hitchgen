public class AirControllerFactory {


  public ArrayList<AirController> constructedChildren;
  public HashMap<AirController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public AirControllerFactory(){

    this.constructedChildren= new ArrayList<AirController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<AirController, Integer>();
    for(AirController savingElement : constructedChildren){
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

  public AirController CreateAirController(){
    AirController result= new AirController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public AirController DestroyAirController(AirController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
