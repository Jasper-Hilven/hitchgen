namespace JavaImplementation
open AST
module JBasics = 


  let JavaPath  = JModule.Child(JModule.Root,"java")
  let UtilPath  = JModule.Child(JavaPath, "util")
  let AListPath = JModule.Child(UtilPath,"ArrayList")
  let HMapPath  = JModule.Child(UtilPath,"HashMap")