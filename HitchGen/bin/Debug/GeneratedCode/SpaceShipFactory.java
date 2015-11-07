public class SpaceShipFactory {


  public PhysicsControllerFactory physicsControllerFactory;
  public UIControllerFactory uIControllerFactory;
  public SystemControllerFactory systemControllerFactory;
  public ArrayList<SpaceShip> constructedChildren;

  public SpaceShipFactory(physicsControllerFactory, uIControllerFactory, systemControllerFactory){

    this.physicsControllerFactory= physicsControllerFactory;
    this.uIControllerFactory= uIControllerFactory;
    this.systemControllerFactory= systemControllerFactory;
    this.constructedChildren= new ArrayList<SpaceShip>();

  }

  public SpaceShip CreateSpaceShip(){
    PhysicsController physicsController= physicsControllerFactory.CreatePhysicsController();
    UIController uIController= uIControllerFactory.CreateUIController();
    SystemController systemController= systemControllerFactory.CreateSystemController();
    SpaceShip result= new SpaceShip(physicsController, uIController, systemController);
    constructedChildren.Add(result);
    return result;
  }

  public SpaceShip DestroySpaceShip(SpaceShip toDestroy){
    physicsControllerFactory.DestroyPhysicsController(toDestroy.physicsController);
    uIControllerFactory.DestroyUIController(toDestroy.uIController);
    systemControllerFactory.DestroySystemController(toDestroy.systemController);
    constructedChildren.Remove(toDestroy);
  }

}
