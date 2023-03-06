using Bioinformatics.Tests.Attributes;

namespace Bioinformatics.Tests.Chapter1
{
    public class Chapter1
    {
        [Theory]
        [NewLineTextFileData("Chapter1/PatternCount/inputs/*.txt", 2, "Chapter1/PatternCount/outputs/*.txt", 1)]
        public void PatternCountTests(string text, string pattern, int expectedOutput)
        {
            var result = Bioinformatics.Chapter1.patternCount(text, pattern);
            Assert.Equal(result, expectedOutput);
        }
    }
}