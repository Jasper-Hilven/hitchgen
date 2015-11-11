public class ElectroControllerFactory {


  public ArrayList<ElectroController> constructedChildren;
  public HashMap<ElectroController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public ElectroControllerFactory(){

    this.constructedChildren= new ArrayList<ElectroController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<ElectroController, Integer>();
    for(ElectroController savingElement : constructedChildren){
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

  public ElectroController CreateElectroController(){
    ElectroController result= new ElectroController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public ElectroController DestroyElectroController(ElectroController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
