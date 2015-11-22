public class SpaceShipFactory{


  ArrayList<SpaceShip> generatedSpaceShips;
  HashMap<SpaceShip,Integer> savedMapping;
  boolean savedChildren;
  PhysicsControllerFactory physicsControllerFactory;
  UIControllerFactory uIControllerFactory;
  SystemControllerFactory systemControllerFactory;


  public SpaceShipFactory(PhysicsControllerFactory physicsControllerFactory, UIControllerFactory uIControllerFactory, SystemControllerFactory systemControllerFactory){
    this.physicsControllerFactory = physicsControllerFactory;
    this.uIControllerFactory = uIControllerFactory;
    this.systemControllerFactory = systemControllerFactory;
    this.generatedSpaceShips = new ArrayList<SpaceShip>();
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
  
  public int GetIdSpaceShip(SpaceShip key){
    return savedMapping.Get(key);
  }
  
  public void SaveSpaceShip(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    physicsControllerFactory.SavePhysicsController(saveMap);
    uIControllerFactory.SaveUIController(saveMap);
    systemControllerFactory.SaveSystemController(saveMap);
    for(SpaceShip spaceShip: generatedSpaceShips){
      int indexPhysicsController = physicsControllerFactory.GetIdPhysicsController(spaceShip.GetPhysicsController());
      int indexUIController = uIControllerFactory.GetIdUIController(spaceShip.GetUIController());
      int indexSystemController = systemControllerFactory.GetIdSystemController(spaceShip.GetSystemController());
    }
    saveMap.Put("SpaceShip", saveList);
  }
  
  public void FinishSavingSpaceShip(){
    savedMapping = false;
    physicsControllerFactory.FinishSavingPhysicsController();
    uIControllerFactory.FinishSavingUIController();
    systemControllerFactory.FinishSavingSystemController();
  }


}
