public class ElectroControllerFactory{


  HashList<ElectroController> generatedElectroController;
  HashMap<ElectroController,Integer> savedMapping;
  boolean savedChildren;


  public ElectroControllerFactory(){
    this.generatedElectroController= new HashList<ElectroController>();
  }


  public ElectroController ConstructElectroController(){
    ElectroController electroController= new ElectroController();
    generatedElectroController.Add(electroController);
    return electroController;
  }
  
  public ElectroController DestructElectroController(){
    generatedElectroController.Remove(electroController);
  }
  
  public Int GetIdElectroController(Int key){
    return savedMapping.Get(key);
  }
  
  public void StartSavingElectroController(){
    if(savedMapping){
      return ;
    }
    else{
      savedMapping= True;
    }
  }


}
