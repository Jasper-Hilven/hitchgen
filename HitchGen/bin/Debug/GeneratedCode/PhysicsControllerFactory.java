public class PhysicsControllerFactory {


  public ArrayList<PhysicsController> constructedChildren;
  public HashMap<PhysicsController, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public PhysicsControllerFactory(){

    this.constructedChildren= new ArrayList<PhysicsController>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
    ChildToNumber= new HashMap<PhysicsController, Integer>();
    for(PhysicsController savingElement : constructedChildren){
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

  public PhysicsController CreatePhysicsController(){
    PhysicsController result= new PhysicsController();
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public PhysicsController DestroyPhysicsController(PhysicsController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
