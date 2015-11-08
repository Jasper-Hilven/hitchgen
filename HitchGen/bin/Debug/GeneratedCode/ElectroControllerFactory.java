public class ElectroControllerFactory {


  public ArrayList<ElectroController> constructedChildren;
  public HashMap<ElectroController, Integer> ChildToNumber;

  public ElectroControllerFactory(){

    this.constructedChildren= new ArrayList<ElectroController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<ElectroController, Integer>();
    for(ElectroController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public ElectroController CreateElectroController(){
    ElectroController result= new ElectroController();
    constructedChildren.Add(result);
    return result;
  }

  public ElectroController DestroyElectroController(ElectroController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
