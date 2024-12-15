using System.IO;
using universitycollege.finding.view;

namespace universitycollege.finding.model
{
    public static class ReaderPatternFile
    {
        private static string _filePath = InMemory.PathToPattern;

        /// <summary>
        /// Method for obtaining an array from a file
        /// </summary>
        public static sbyte[,] GetPatternArr(string fileName)
        {
            string[] lines = File.ReadAllLines(_filePath + fileName);

            int rows = lines.Length;
            int cols = lines[0].Split(' ').Length;

            sbyte[,] _patternArr = new sbyte[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] numbers = lines[i].Split(' ');
                for (int j = 0; j < cols; j++)
                {
                    _patternArr[i, j] = sbyte.Parse(numbers[j]);
                }
            }

            return _patternArr;
        }

        /// <summary>
        /// Method for creating a new pattern from an array
        /// </summary>
        /// <param name="patternArray">Array filled with numbers</param>
        public static void CreatePattern(sbyte[,] patternArray, string fileName)
        {
            StreamWriter writer = new StreamWriter(_filePath + fileName + ".txt");

            for (int i = 0; i < patternArray.GetLength(0); i++)
            {
                for (int j = 0; j < patternArray.GetLength(1); j++)
                {
                    writer.Write(patternArray[i, j]);
                    if (j < patternArray.GetLength(1) - 1)
                    {
                        writer.Write(" ");
                    }
                }
                writer.WriteLine();
            }

            writer.Close();
        }
    }
}
