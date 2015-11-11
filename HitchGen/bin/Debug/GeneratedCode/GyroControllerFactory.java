public class GyroControllerFactory {


  public ArrayList<GyroController> constructedChildren;
  public HashMap<GyroController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public GyroControllerFactory(){

    this.constructedChildren= new ArrayList<GyroController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<GyroController, Integer>();
    for(GyroController savingElement : constructedChildren){
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

  public GyroController CreateGyroController(){
    GyroController result= new GyroController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public GyroController DestroyGyroController(GyroController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
