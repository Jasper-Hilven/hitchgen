namespace JavaImplementation
module JavaImplementationTest =
  open NUnit.Framework
  open FsUnit
  open AST
  [<TestFixture>]
  type ASTTest() =
    [<Test>]
    member x.TestIfSameTypesAreEqual() = 
      let firstType = JType.Dedicated("One",JModule.Child(JModule.Root,"Children"))
      let firstTypeClone = JType.Dedicated("One",JModule.Child(JModule.Root,"Children"))
      let secondType = JType.Dedicated("Second",JModule.Child(JModule.Root, "Children"))
      firstType |> should not' (equal secondType)
      firstType |> should equal firstTypeClone