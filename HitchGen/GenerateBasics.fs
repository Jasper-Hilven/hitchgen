module GenerateBasics

open Controllers
open JavaImplementation.JAPI
open LanguageInterface.API
open LanguageInterface.ImplementationInterface

type UT = ILJava
//let apiProvider = new APIProvider<ILJava>(new JProvider() );

///CONTROLLER

type LController<'L>(controller: Controller,lApiProvider: APIProvider<'L> ) =
  member private this.ApiProvider = lApiProvider
  member this.Module = lApiProvider.ModuleRoot.Child("com").Child("jasperhilven").Child("controller")
  member this.LType = lApiProvider.LType controller.Name this.Module
  member this.Variable = this.LType.Variable
  member this.Children = controller.Children |> List.map (fun c -> new LController<'L>(c,lApiProvider))
  member this.ChildVariables = this.Children |> List.map (fun c -> c.Variable)
  
///ILVARIABLE

type LVariable<'L> with
  member this.Getter = this.Provider.ClMethodDecl ("Get" + this.LType.Name) this.LType [] (this.Provider.StReturn this.Eval.IlRHV)
  member this.AsField = this.Provider.This.AccessField(this)

///ILTYPE

type LType<'L> with
  member this.GetConstructorFieldInitializations(fieldsToInitialize: LVariable<'L> list, constructorType) =
    //let content = fieldsToInitialize |> List.map (fun (o : LVariable<'L>)-> o.AsField.SetTo(o.Eval))
    0
  //GetConstructorDeclaration(constructorType,fieldsToInitialize,content)
  (*

let GetControllerOfClass(controller) = 
  let constructorMethod = GetConstructorFieldInitializations(childVariables,controllerType)
  let getters = childVariables |> List.map GetGetter
  GetClass(controllerType,[constructorMethod],getters,childVariables)

let controllerFiles = controllers |> List.map GetControllerOfClass *)