public class ElectroControllerFactory {


  public ArrayList<ElectroController> constructedChildren;

  public ElectroControllerFactory(){

    this.constructedChildren= new ArrayList<ElectroController>();

  }

  public ElectroController CreateElectroController(){
    ElectroController result= new ElectroController();
    constructedChildren.Add(result);
    return result;
  }

  public ElectroController DestroyElectroController(ElectroController toDestroy){
    constructedChildren.Remove(toDestroy);
  }

}
