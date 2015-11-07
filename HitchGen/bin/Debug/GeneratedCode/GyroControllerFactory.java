public class GyroControllerFactory {


  public ArrayList<GyroController> constructedChildren;

  public GyroControllerFactory(){

    this.constructedChildren= new ArrayList<GyroController>();

  }

  public GyroController CreateGyroController(){
    GyroController result= new GyroController();
    constructedChildren.Add(result);
    return result;
  }

  public GyroController DestroyGyroController(GyroController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
