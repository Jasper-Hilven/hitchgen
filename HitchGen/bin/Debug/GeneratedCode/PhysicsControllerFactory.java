public class PhysicsControllerFactory {


  public ArrayList<PhysicsController> constructedChildren;
  public HashMap<PhysicsController, Integer> ChildToNumber;

  public PhysicsControllerFactory(){

    this.constructedChildren= new ArrayList<PhysicsController>();

  }

  public int MakeChildToNumber(int currentCount){
    ChildToNumber= new HashMap<PhysicsController, Integer>();
    for(PhysicsController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public PhysicsController CreatePhysicsController(){
    PhysicsController result= new PhysicsController();
    constructedChildren.Add(result);
    return result;
  }

  public PhysicsController DestroyPhysicsController(PhysicsController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
