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
  
  public Int GetIdSystemController(Int key){
    return savedMapping.Get(key);
  }
  
  public void SaveSystemController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    engineControllerFactory.SaveEngineController();
    gyroControllerFactory.SaveGyroController();
    fuelControllerFactory.SaveFuelController();
    electroControllerFactory.SaveElectroController();
    airControllerFactory.SaveAirController();
    for(SystemController systemController: generatedSystemControllers){
    ;
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
