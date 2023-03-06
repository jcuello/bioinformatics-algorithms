using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Bioinformatics.Tests.Attributes
{
    public class FileDataDiscoverer : DataDiscoverer
    {
        public override bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            return false;
        }
    }

    [DataDiscoverer("Bioinformatics.Tests.Attributes.FileDataDiscoverer", "Bioinformatics.Tests")]
    public class NewLineTextFileDataAttribute : DataAttribute
    {
        string _inputFilePath;
        int _inputLineCount;
        string _outputFilePath;
        int _outputLineCount;
        public NewLineTextFileDataAttribute(string inputFilePath, int inputLineCount,
            string outputFilePath, int outputLineCount)
        {
            _inputFilePath = inputFilePath;
            _inputLineCount = inputLineCount;
            _outputFilePath = outputFilePath;
            _outputLineCount = outputLineCount;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            List<object[]> results = new();
            var files = Enumerable.Zip(GetFiles(_inputFilePath), GetFiles(_outputFilePath));
            foreach ((string inputFile, string outputFile) in files)
            {
                if (File.Exists(inputFile) && File.Exists(outputFile))
                {
                    var inputs = ReadFile(inputFile, _inputLineCount);
                    var outputs = ReadFile(outputFile, _outputLineCount);
                    inputs.AddRange(outputs);
                    results.Add(inputs.ToArray());
                }
                else
                {
                    throw new ArgumentException($"Unable to find files {Path.GetFullPath(_inputFilePath)} and %s{Path.GetFullPath(_outputFilePath)}");
                }
            }
            return results;
        }

        private List<object> ReadFile(string filepath, int lineCount)
        {
            List<object> results = new();
            using (var file = File.OpenText(filepath))
            {
                for (int i = 0; i < lineCount; i++)
                {
                    results.Add(file.ReadLine() ?? "");
                }
            }
            return results;
        }

        private string[] GetFiles(string path)
        {
            var fullPath = Path.GetFullPath(path);
            var filePathDirectory = Path.GetDirectoryName(fullPath) ?? "";

            if (fullPath.Contains('*') || fullPath.Contains('?'))
            {
                return Directory.GetFiles(filePathDirectory, Path.GetFileName(fullPath), SearchOption.TopDirectoryOnly);
            }
            else
            {
                return Directory.GetFiles(filePathDirectory);
            }
        }
    }
}
