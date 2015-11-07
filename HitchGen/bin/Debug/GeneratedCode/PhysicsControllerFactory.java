public class PhysicsControllerFactory {


  public ArrayList<PhysicsController> constructedChildren;

  public PhysicsControllerFactory(){

    this.constructedChildren= new ArrayList<PhysicsController>();

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
