public class SpaceShipFactory {


  public PhysicsControllerFactory physicsControllerFactory;
  public UIControllerFactory uIControllerFactory;
  public SystemControllerFactory systemControllerFactory;
  public ArrayList<SpaceShip> constructedChildren;
  public HashMap<SpaceShip, Integer> ChildToNumber;

  public SpaceShipFactory(physicsControllerFactory, uIControllerFactory, systemControllerFactory){

    this.physicsControllerFactory= physicsControllerFactory;
    this.uIControllerFactory= uIControllerFactory;
    this.systemControllerFactory= systemControllerFactory;
    this.constructedChildren= new ArrayList<SpaceShip>();

  }

  public int MakeChildToNumber(int currentCount){
    currentCount= physicsControllerFactory.MakeChildToNumber(currentCount);
    currentCount= uIControllerFactory.MakeChildToNumber(currentCount);
    currentCount= systemControllerFactory.MakeChildToNumber(currentCount);
    ChildToNumber= new HashMap<SpaceShip, Integer>();
    for(SpaceShip savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
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
