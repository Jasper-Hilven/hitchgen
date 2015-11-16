public class SystemControllerFactory{


  HashList<SystemController> generatedSystemController;
  HashMap<SystemController,Integer> savedMapping;
  boolean savedChildren;
  EngineControllerFactory engineControllerFactory;
  GyroControllerFactory gyroControllerFactory;
  FuelControllerFactory fuelControllerFactory;
  ElectroControllerFactory electroControllerFactory;
  AirControllerFactory airControllerFactory;


  public SystemControllerFactory(EngineControllerFactory engineControllerFactory, GyroControllerFactory gyroControllerFactory, FuelControllerFactory fuelControllerFactory, ElectroControllerFactory electroControllerFactory, AirControllerFactory airControllerFactory){
    this.engineControllerFactory= engineControllerFactory;
    this.gyroControllerFactory= gyroControllerFactory;
    this.fuelControllerFactory= fuelControllerFactory;
    this.electroControllerFactory= electroControllerFactory;
    this.airControllerFactory= airControllerFactory;
    this.generatedSystemController= new HashList<SystemController>();
  }


  public SystemController ConstructSystemController(){
    EngineController engineController= engineControllerFactory.ConstructEngineController();
    GyroController gyroController= gyroControllerFactory.ConstructGyroController();
    FuelController fuelController= fuelControllerFactory.ConstructFuelController();
    ElectroController electroController= electroControllerFactory.ConstructElectroController();
    AirController airController= airControllerFactory.ConstructAirController();
    SystemController systemController= new SystemController(engineController, gyroController, fuelController, electroController, airController);
    generatedSystemController.Add(systemController);
    return systemController;
  }
  
  public SystemController DestructSystemController(){
    generatedSystemController.Remove(systemController);
    engineControllerFactory.DestructEngineController(systemController.GetEngineController());
    gyroControllerFactory.DestructGyroController(systemController.GetGyroController());
    fuelControllerFactory.DestructFuelController(systemController.GetFuelController());
    electroControllerFactory.DestructElectroController(systemController.GetElectroController());
    airControllerFactory.DestructAirController(systemController.GetAirController());
  }
  
  public Int GetIdSystemController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingSystemController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
    engineControllerFactory.StartSavingSystemController();
    gyroControllerFactory.StartSavingSystemController();
    fuelControllerFactory.StartSavingSystemController();
    electroControllerFactory.StartSavingSystemController();
    airControllerFactory.StartSavingSystemController();
  }


}
