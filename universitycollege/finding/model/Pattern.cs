using System.Collections.Generic;
using System.IO;
using universitycollege.finding.view;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class Pattern // TODO: Разделить загрузку из файла и работу с паттернами
    {
        private sbyte[,] _patternArr;
        private string _filePath = InMemory.PathToPattern;
        private Coords _coordsCenter;

        /// <summary>
        /// Конструктор, если необходимо прочитать шаблон из папки
        /// </summary>
        /// <param name="fileName">Название шаблона</param>
        public Pattern(string fileName) 
        {
            _filePath += fileName;
            GetPatternArr();
            GetCenterCoords();
        }

        /// <summary>
        /// Конструктор, если необходимо создать новый шаблон в папке
        /// </summary>
        /// <param name="patternArr">Двумерный массив из цифр</param>
        /// <param name="fileName">Название шаблона</param>
        public Pattern(sbyte[,] patternArr, string fileName)
        {
            _patternArr = patternArr;
            _filePath += fileName + ".txt";
            CreatePattern(patternArr);
            GetCenterCoords();
        }

        /// <summary>
        /// Метод получения массива из файла
        /// </summary>
        private void GetPatternArr() // TODO: В отдельный класс
        {
            string[] lines = File.ReadAllLines(_filePath);

            int rows = lines.Length;
            int cols = lines[0].Split(' ').Length;

            _patternArr = new sbyte[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] numbers = lines[i].Split(' ');
                for (int j = 0; j < cols; j++)
                {
                    _patternArr[i, j] = sbyte.Parse(numbers[j]);
                }
            }
        }

        /// <summary>
        /// Метод для создания нового шаблона из массива
        /// </summary>
        /// <param name="patternArray">Массив, заполенный цифрами</param>
        private void CreatePattern(sbyte[,] patternArray) // TODO: В отдельный класс
        {
            StreamWriter writer = new StreamWriter(_filePath);

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

        private void GetCenterCoords()
        {
            _coordsCenter.x = _patternArr.GetLength(0) / 2;
            _coordsCenter.y = _patternArr.GetLength(1) / 2;
        }

        public Dictionary<Coords, sbyte> GetPatternCoords(Map map, Coords centerCoords)
        {
            Dictionary<Coords, sbyte> tempDict = new Dictionary<Coords, sbyte>();

            for (int i = 0; i < _patternArr.GetLength(0); i++)
            {
                for (int j = 0; j < _patternArr.GetLength(1); j++)
                {
                    Coords mapCoords = new Coords(centerCoords.x + i - (_patternArr.GetLength(0) / 2),
                                           centerCoords.y + j - (_patternArr.GetLength(1) / 2));

                    if (map.IsInAMap(mapCoords) && 
                        (map.IsPointAreBelow(mapCoords, _patternArr[i, j]) || map.IsPointAreHigher(mapCoords, _patternArr[i, j])))
                        tempDict[mapCoords] = (sbyte)_patternArr[i, j];
                }
            }

            return tempDict;
        }

        public override string ToString()
        {
            string pattern = $"{System.IO.Path.GetFileName(_filePath)}\n";
            
            for (int i = 0; i < _patternArr.GetLength(0); i++)
            {
                for (int j = 0; j < _patternArr.GetLength(1); j++)
                {
                    pattern += _patternArr[i, j] + " ";
                }
                pattern += "\n";
            }

            return pattern;
        }
    }
}
