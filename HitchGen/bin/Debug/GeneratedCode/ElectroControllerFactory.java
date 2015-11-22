public class ElectroControllerFactory{


  ArrayList<ElectroController> generatedElectroControllers;
  HashMap<ElectroController,Integer> savedMapping;
  boolean savedChildren;


  public ElectroControllerFactory(){
    this.generatedElectroControllers = new ArrayList<ElectroController>();
  }


  public ElectroController ConstructElectroController(){
    ElectroController electroController = new ElectroController();
    generatedElectroControllers.Add(electroController);
    return electroController;
  }
  
  public ElectroController DestructElectroController(){
    generatedElectroControllers.Remove(electroController);
  }
  
  public int GetIdElectroController(ElectroController key){
    return savedMapping.Get(key);
  }
  
  public void SaveElectroController(HashMap<String,ArrayList<String>> saveMap){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping = true;
    }
    for(ElectroController electroController: generatedElectroControllers){
    }
    saveMap.Put("ElectroController", saveList);
  }
  
  public void FinishSavingElectroController(){
    savedMapping = false;
  }


}
