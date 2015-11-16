public class SpaceShipFactory{


  HashList<SpaceShip> generatedSpaceShips;
  HashMap<SpaceShip,Integer> savedMapping;
  boolean savedChildren;
  PhysicsControllerFactory physicsControllerFactory;
  UIControllerFactory uIControllerFactory;
  SystemControllerFactory systemControllerFactory;


  public SpaceShipFactory(PhysicsControllerFactory physicsControllerFactory, UIControllerFactory uIControllerFactory, SystemControllerFactory systemControllerFactory){
    this.physicsControllerFactory = physicsControllerFactory;
    this.uIControllerFactory = uIControllerFactory;
    this.systemControllerFactory = systemControllerFactory;
    this.generatedSpaceShips = new HashList<SpaceShip>();
  }


  public SpaceShip ConstructSpaceShip(){
    PhysicsController physicsController = physicsControllerFactory.ConstructPhysicsController();
    UIController uIController = uIControllerFactory.ConstructUIController();
    SystemController systemController = systemControllerFactory.ConstructSystemController();
    SpaceShip spaceShip = new SpaceShip(physicsController, uIController, systemController);
    generatedSpaceShips.Add(spaceShip);
    return spaceShip;
  }
  
  public SpaceShip DestructSpaceShip(){
    generatedSpaceShips.Remove(spaceShip);
    physicsControllerFactory.DestructPhysicsController(spaceShip.GetPhysicsController());
    uIControllerFactory.DestructUIController(spaceShip.GetUIController());
    systemControllerFactory.DestructSystemController(spaceShip.GetSystemController());
  }
  
  public Int GetIdSpaceShip(Int key){
    return savedMapping.Get(key);
  }
  
  public void SaveSpaceShip(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    physicsControllerFactory.SavePhysicsController();
    uIControllerFactory.SaveUIController();
    systemControllerFactory.SaveSystemController();
    for(SpaceShip spaceShip: generatedSpaceShips){
    ;
    }
  }
  
  public void FinishSavingSpaceShip(){
    savedMapping = false;
    physicsControllerFactory.FinishSavingPhysicsController();
    uIControllerFactory.FinishSavingUIController();
    systemControllerFactory.FinishSavingSystemController();
  }


}
