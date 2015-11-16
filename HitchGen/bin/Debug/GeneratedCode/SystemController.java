public class SystemController{


  EngineController engineController;
  GyroController gyroController;
  FuelController fuelController;
  ElectroController electroController;
  AirController airController;


  public SystemController(EngineController engineController, GyroController gyroController, FuelController fuelController, ElectroController electroController, AirController airController){
    this.engineController = engineController;
    this.gyroController = gyroController;
    this.fuelController = fuelController;
    this.electroController = electroController;
    this.airController = airController;
  }


  public EngineController GetEngineController(){
    return this.engineController;
  }
  
  public GyroController GetGyroController(){
    return this.gyroController;
  }
  
  public FuelController GetFuelController(){
    return this.fuelController;
  }
  
  public ElectroController GetElectroController(){
    return this.electroController;
  }
  
  public AirController GetAirController(){
    return this.airController;
  }


}
