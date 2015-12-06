﻿namespace JavaImplementation
open NUnit.Framework
open FsUnit
open ImportProvider
open AST
module TData = 
  let testModule() = JModule.Child(JModule.Child(JModule.Root,"Children"),"Test")
  let simpleName() = "Simple"
  let testType() = JType.Dedicated(simpleName(), testModule())
  let testDedicatedVariable() = new JVariable(simpleName(),testType())
  let testGetClassImports(jClass: JClass) = GetClassImports jClass
module JavaImplementationTest =
  open TData
  
  [<TestFixture>]
  type ASTTest() =
      
    [<Test>]
    member x.TestIfSameTypesAreEqual() = 
      let firstType = testType()
      let firstTypeClone = JType.Dedicated(TData.simpleName(), TData.testModule())
      let secondType = JType.Dedicated("Second", TData.testModule())
      firstType |> should not' (equal secondType)
      firstType |> should equal firstTypeClone
  [<TestFixture>]
  type ImportsTest() =
    [<Test>]
    member x.TestIfClassItselfNotImported() =
      let simpleClass = new JClass(TData.testType(), [], [], [TData.testDedicatedVariable()])
      testGetClassImports(simpleClass).Count |> should equal 0