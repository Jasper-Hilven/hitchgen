public class SystemControllerFactory {


  EngineControllerFactory engineControllerFactory;
  GyroControllerFactory gyroControllerFactory;
  FuelControllerFactory fuelControllerFactory;
  ElectroControllerFactory electroControllerFactory;
  AirControllerFactory airControllerFactory;

  public SystemControllerFactory(engineControllerFactory, gyroControllerFactory, fuelControllerFactory, electroControllerFactory, airControllerFactory){

        this.engineControllerFactory= engineControllerFactory;
        this.gyroControllerFactory= gyroControllerFactory;
        this.fuelControllerFactory= fuelControllerFactory;
        this.electroControllerFactory= electroControllerFactory;
        this.airControllerFactory= airControllerFactory;

  }

  public SystemController CreateSystemController(){
    EngineController engineController= engineControllerFactory.CreateEngineController();
    GyroController gyroController= gyroControllerFactory.CreateGyroController();
    FuelController fuelController= fuelControllerFactory.CreateFuelController();
    ElectroController electroController= electroControllerFactory.CreateElectroController();
    AirController airController= airControllerFactory.CreateAirController();
    return new SystemController(engineController, gyroController, fuelController, electroController, airController);
  }

}
