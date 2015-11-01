public class SystemControllerFactory {


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

  }

}
