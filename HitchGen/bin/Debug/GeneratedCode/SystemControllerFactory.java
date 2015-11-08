public class SystemControllerFactory {


  public EngineControllerFactory engineControllerFactory;
  public GyroControllerFactory gyroControllerFactory;
  public FuelControllerFactory fuelControllerFactory;
  public ElectroControllerFactory electroControllerFactory;
  public AirControllerFactory airControllerFactory;
  public ArrayList<SystemController> constructedChildren;
  public HashMap<SystemController, Integer> ChildToNumber;

  public SystemControllerFactory(engineControllerFactory, gyroControllerFactory, fuelControllerFactory, electroControllerFactory, airControllerFactory){

    this.engineControllerFactory= engineControllerFactory;
    this.gyroControllerFactory= gyroControllerFactory;
    this.fuelControllerFactory= fuelControllerFactory;
    this.electroControllerFactory= electroControllerFactory;
    this.airControllerFactory= airControllerFactory;
    this.constructedChildren= new ArrayList<SystemController>();

  }

  public int MakeChildToNumber(int currentCount){
    currentCount= engineControllerFactory.MakeChildToNumber(currentCount);
    currentCount= gyroControllerFactory.MakeChildToNumber(currentCount);
    currentCount= fuelControllerFactory.MakeChildToNumber(currentCount);
    currentCount= electroControllerFactory.MakeChildToNumber(currentCount);
    currentCount= airControllerFactory.MakeChildToNumber(currentCount);
    ChildToNumber= new HashMap<SystemController, Integer>();
    for(SystemController savingElement : constructedChildren){
      ChildToNumber.Add(savingElement, currentCount);
      currentCount= currentCount + 1;
    }
    return currentCount;
  }

  public SystemController CreateSystemController(){
    EngineController engineController= engineControllerFactory.CreateEngineController();
    GyroController gyroController= gyroControllerFactory.CreateGyroController();
    FuelController fuelController= fuelControllerFactory.CreateFuelController();
    ElectroController electroController= electroControllerFactory.CreateElectroController();
    AirController airController= airControllerFactory.CreateAirController();
    SystemController result= new SystemController(engineController, gyroController, fuelController, electroController, airController);
    constructedChildren.Add(result);
    return result;
  }

  public SystemController DestroySystemController(SystemController toDestroy){
    engineControllerFactory.DestroyEngineController(toDestroy.engineController);
    gyroControllerFactory.DestroyGyroController(toDestroy.gyroController);
    fuelControllerFactory.DestroyFuelController(toDestroy.fuelController);
    electroControllerFactory.DestroyElectroController(toDestroy.electroController);
    airControllerFactory.DestroyAirController(toDestroy.airController);
    constructedChildren.Remove(toDestroy);
  }

}
