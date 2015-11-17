public class SystemControllerFactory{


  HashList<SystemController> generatedSystemControllers;
  HashMap<SystemController,Integer> savedMapping;
  boolean savedChildren;
  EngineControllerFactory engineControllerFactory;
  GyroControllerFactory gyroControllerFactory;
  FuelControllerFactory fuelControllerFactory;
  ElectroControllerFactory electroControllerFactory;
  AirControllerFactory airControllerFactory;


  public SystemControllerFactory(EngineControllerFactory engineControllerFactory, GyroControllerFactory gyroControllerFactory, FuelControllerFactory fuelControllerFactory, ElectroControllerFactory electroControllerFactory, AirControllerFactory airControllerFactory){
    this.engineControllerFactory = engineControllerFactory;
    this.gyroControllerFactory = gyroControllerFactory;
    this.fuelControllerFactory = fuelControllerFactory;
    this.electroControllerFactory = electroControllerFactory;
    this.airControllerFactory = airControllerFactory;
    this.generatedSystemControllers = new HashList<SystemController>();
  }


  public SystemController ConstructSystemController(){
    EngineController engineController = engineControllerFactory.ConstructEngineController();
    GyroController gyroController = gyroControllerFactory.ConstructGyroController();
    FuelController fuelController = fuelControllerFactory.ConstructFuelController();
    ElectroController electroController = electroControllerFactory.ConstructElectroController();
    AirController airController = airControllerFactory.ConstructAirController();
    SystemController systemController = new SystemController(engineController, gyroController, fuelController, electroController, airController);
    generatedSystemControllers.Add(systemController);
    return systemController;
  }
  
  public SystemController DestructSystemController(){
    generatedSystemControllers.Remove(systemController);
    engineControllerFactory.DestructEngineController(systemController.GetEngineController());
    gyroControllerFactory.DestructGyroController(systemController.GetGyroController());
    fuelControllerFactory.DestructFuelController(systemController.GetFuelController());
    electroControllerFactory.DestructElectroController(systemController.GetElectroController());
    airControllerFactory.DestructAirController(systemController.GetAirController());
  }
  
  public int GetIdSystemController(int key){
    return savedMapping.Get(key);
  }
  
  public void SaveSystemController(HashMap<String,HashList<String>> saveList){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    engineControllerFactory.SaveEngineController(saveList);
    gyroControllerFactory.SaveGyroController(saveList);
    fuelControllerFactory.SaveFuelController(saveList);
    electroControllerFactory.SaveElectroController(saveList);
    airControllerFactory.SaveAirController(saveList);
    for(SystemController systemController: generatedSystemControllers){
      int indexEngineController = engineControllerFactory.GetIdEngineController(systemController.GetEngineController());
      int indexGyroController = gyroControllerFactory.GetIdGyroController(systemController.GetGyroController());
      int indexFuelController = fuelControllerFactory.GetIdFuelController(systemController.GetFuelController());
      int indexElectroController = electroControllerFactory.GetIdElectroController(systemController.GetElectroController());
      int indexAirController = airControllerFactory.GetIdAirController(systemController.GetAirController());
    }
  }
  
  public void FinishSavingSystemController(){
    savedMapping = false;
    engineControllerFactory.FinishSavingEngineController();
    gyroControllerFactory.FinishSavingGyroController();
    fuelControllerFactory.FinishSavingFuelController();
    electroControllerFactory.FinishSavingElectroController();
    airControllerFactory.FinishSavingAirController();
  }


}
