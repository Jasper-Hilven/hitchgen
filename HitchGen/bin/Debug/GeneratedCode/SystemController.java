public class SystemController {


  EngineController engineController;
  GyroController gyroController;
  FuelController fuelController;
  ElectroController electroController;
  AirController airController;

  public SystemController(EngineController engineController, GyroController gyroController, FuelController fuelController, ElectroController electroController, AirController airController){

        this.engineController= engineController;
        this.gyroController= gyroController;
        this.fuelController= fuelController;
        this.electroController= electroController;
        this.airController= airController;

  }


}
