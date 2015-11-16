public class ElectroControllerFactory{


  HashList<ElectroController> generatedElectroControllers;
  HashMap<ElectroController,Integer> savedMapping;
  boolean savedChildren;


  public ElectroControllerFactory(){
    this.generatedElectroControllers = new HashList<ElectroController>();
  }


  public ElectroController ConstructElectroController(){
    ElectroController electroController = new ElectroController();
    generatedElectroControllers.Add(electroController);
    return electroController;
  }
  
  public ElectroController DestructElectroController(){
    generatedElectroControllers.Remove(electroController);
  }
  
  public Int GetIdElectroController(Int key){
    return savedMapping.Get(key);
  }
  
  public void SaveElectroController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(ElectroController electroController: generatedElectroControllers){
    ;
    }
  }
  
  public void FinishSavingElectroController(){
    savedMapping = false;
  }


}
