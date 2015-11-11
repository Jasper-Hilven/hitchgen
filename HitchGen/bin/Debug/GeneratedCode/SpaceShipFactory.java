public class SpaceShipFactory {


  public PhysicsControllerFactory physicsControllerFactory;
  public UIControllerFactory uIControllerFactory;
  public SystemControllerFactory systemControllerFactory;
  public ArrayList<SpaceShip> constructedChildren;
  public HashMap<SpaceShip, Integer> ChildToNumber;
  public boolean countedFactory;
  public boolean storedFactoryChildren;

  public SpaceShipFactory(physicsControllerFactory, uIControllerFactory, systemControllerFactory){

    this.physicsControllerFactory= physicsControllerFactory;
    this.uIControllerFactory= uIControllerFactory;
    this.systemControllerFactory= systemControllerFactory;
    this.constructedChildren= new ArrayList<SpaceShip>();

  }

  public int MakeChildToNumber(int currentCount){
    if(countedFactory){
      return currentCount;
    }
    countedFactory= true;
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

  public void RemoveChildNumberCollection(){
    ChildToNumber= null;
    physicsControllerFactory.RemoveChildNumberCollection();
    uIControllerFactory.RemoveChildNumberCollection();
    systemControllerFactory.RemoveChildNumberCollection();
    countedFactory= false;
    StoredFactoryChildren= false;
  }

  public SpaceShip CreateSpaceShip(){
    PhysicsController physicsController= physicsControllerFactory.CreatePhysicsController();
    UIController uIController= uIControllerFactory.CreateUIController();
    SystemController systemController= systemControllerFactory.CreateSystemController();
    SpaceShip result= new SpaceShip(physicsController, uIController, systemController);
    constructedChildren.Add(result);
    return result;
  }

  public String GenerateJSon(){
    if(storedFactoryChildren){
      return "[]";
    }
    storedFactoryChildren= true;
  }

  public SpaceShip DestroySpaceShip(SpaceShip toDestroy){
    physicsControllerFactory.DestroyPhysicsController(toDestroy.physicsController);
    uIControllerFactory.DestroyUIController(toDestroy.uIController);
    systemControllerFactory.DestroySystemController(toDestroy.systemController);
    constructedChildren.Remove(toDestroy);
  }

}
