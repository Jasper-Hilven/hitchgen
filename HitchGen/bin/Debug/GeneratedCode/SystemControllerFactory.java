public class SystemControllerFactory{


  HashList<SystemController> generatedSystemController;
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
    return new SystemController(engineController, gyroController, fuelController, electroController, airController);
  }
  
  public SystemController DestructSystemController(){
    engineControllerFactory.DestructEngineController(systemController.GetEngineController());
    gyroControllerFactory.DestructGyroController(systemController.GetGyroController());
    fuelControllerFactory.DestructFuelController(systemController.GetFuelController());
    electroControllerFactory.DestructElectroController(systemController.GetElectroController());
    airControllerFactory.DestructAirController(systemController.GetAirController());
  }


}
