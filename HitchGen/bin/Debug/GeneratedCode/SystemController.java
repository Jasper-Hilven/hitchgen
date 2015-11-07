public class SystemController {


  public EngineController engineController;
  public GyroController gyroController;
  public FuelController fuelController;
  public ElectroController electroController;
  public AirController airController;

  public SystemController(EngineController engineController, GyroController gyroController, FuelController fuelController, ElectroController electroController, AirController airController){

    this.engineController= engineController;
    this.gyroController= gyroController;
    this.fuelController= fuelController;
    this.electroController= electroController;
    this.airController= airController;

  }


}
