namespace LanguageInterface

module TokenProvider = 
  type ILanguage = 
    abstract member LanguageName: string
  and ILType<'L> = interface end

  and ILModule<'L> = interface end

  and ILValue<'L> = 
    abstract member Eval: ILRHV<'L>

  and ILRHV<'L> = 
    abstract member Statement: ILStatement<'L>

  and ILVariable<'L> = 
    abstract member Eval: ILRHV<'L>
  and ILStatement<'L> = interface end
  and ILMethod<'L> = interface end
  and ILConstructor<'L> = interface end
  and ILClass<'L> = interface end
  and ITokenProvider<'L> = 
    ///TYPES
    abstract member TypeList: ILType<'L> -> ILType<'L>
    abstract member TypeMap: ILType<'L> -> ILType<'L> -> ILType<'L>
    abstract member TypeFree: string -> ILModule<'L> -> ILType<'L>
    abstract member TypeVoid: ILType<'L>
    abstract member TypeString: ILType<'L>
    abstract member TypeBool: ILType<'L>
    abstract member TypeInt: ILType<'L>
    abstract member Type2String: ILType<'L> -> string

    ///Values
    abstract member ValueTrue: ILValue<'L>
    abstract member ValueFalse: ILValue<'L>
    abstract member ValueString: string -> ILValue<'L>
    abstract member ValueInt: int -> ILValue<'L>
    ///VARIABLES
    abstract member Variable:string -> ILType<'L> -> ILVariable<'L>
    ///GETSTATEMENT
    abstract member StIfBlock:ILRHV<'L> ->  ILStatement<'L> -> ILStatement<'L> 
    abstract member StIfElseBlock: ILRHV<'L> ->  ILStatement<'L> -> ILStatement<'L> -> ILStatement<'L> 
    abstract member StAssignment: ILVariable<'L> -> ILRHV<'L> -> ILStatement<'L>
    abstract member StMultSt: ILStatement<'L> list -> ILStatement<'L>
    abstract member StDeclAssignVariable: ILVariable<'L> -> ILRHV<'L> -> ILStatement<'L>
    abstract member StReturn: ILRHV<'L> -> ILStatement<'L>
    abstract member StReturnVoid: ILStatement<'L>
    abstract member StForeach: ILVariable<'L> -> ILRHV<'L> -> ILStatement<'L> -> ILStatement<'L>
    //OO
    abstract member OoAccessField: ILRHV<'L>->ILVariable<'L>-> ILRHV<'L>
    abstract member OoConstructorCall: ILType<'L> -> (ILRHV<'L> list) -> ILRHV<'L>
    abstract member OoThis: ILVariable<'L>
    abstract member OoMethodCall: ILRHV<'L> -> ILVariable<'L> -> ILRHV<'L> list -> ILRHV<'L>
  
    //Class
    abstract member ClMethodDecl: ILVariable<'L> -> (ILVariable<'L> list) -> ILStatement<'L> -> ILMethod<'L>
    abstract member ClConstructorDecl: ILType<'L> -> (ILVariable<'L> list) -> ILStatement<'L> -> ILConstructor<'L>
    abstract member ClClassDecl: ILType<'L> -> ILConstructor<'L> list -> ILMethod<'L> list -> ILVariable<'L> list -> ILClass<'L>
  
  
  type IClassResult = 
    abstract member ClassPath: string
    abstract member ClassContent : string list
    abstract member ClassName: string
  
  type IClassPrinter<'L> = 
    abstract member PrintAllClasses: ILClass<'L> list -> string -> IClassResult list
