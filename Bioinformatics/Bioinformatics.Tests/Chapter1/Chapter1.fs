namespace Bioinformatics.Tests.Attributes

module Chapter1 =
  open Bioinformatics
  open Xunit    
  
  [<Theory>]  
  [<NewLineTextFileData("Chapter1/PatternCount/inputs/*.txt", 2, "Chapter1/PatternCount/outputs/*.txt", 1)>]
  let ``My test`` (text:string, pattern:string, expectedOutput:int) =
      let result = Chapter1.patternCount text pattern
      Assert.Equal(result, expectedOutput)