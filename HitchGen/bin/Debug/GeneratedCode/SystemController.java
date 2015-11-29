public class SystemController{


  EngineController engineController;
  GyroController gyroController;
  FuelController fuelController;
  ElectroController electroController;
  AirController airController;


  public SystemController(EngineController engineController, GyroController gyroController, FuelController fuelController, ElectroController electroController, AirController airController){
    this.EngineController engineController=engineController;
    this.GyroController gyroController=gyroController;
    this.FuelController fuelController=fuelController;
    this.ElectroController electroController=electroController;
    this.AirController airController=airController;
  }


  public EngineController GetEngineController(){
    return engineController;
  }
  
  public GyroController GetGyroController(){
    return gyroController;
  }
  
  public FuelController GetFuelController(){
    return fuelController;
  }
  
  public ElectroController GetElectroController(){
    return electroController;
  }
  
  public AirController GetAirController(){
    return airController;
  }


}
