public class GyroControllerFactory {


  public ArrayList<GyroController> constructedChildren;
  public HashMap<GyroController, Integer> ChildToNumber;

  public GyroControllerFactory(){

    this.constructedChildren= new ArrayList<GyroController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<GyroController, Integer>();
    for(GyroController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public GyroController CreateGyroController(){
    GyroController result= new GyroController();
    constructedChildren.Add(result);
    return result;
  }

  public GyroController DestroyGyroController(GyroController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
