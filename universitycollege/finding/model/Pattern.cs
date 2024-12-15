using System.Collections.Generic;
using static universitycollege.finding.model.Map;

namespace universitycollege.finding.model
{
    public class Pattern
    {
        private sbyte[,] _patternArr;
        private Coords _coordsCenter;
        private string _name;

        /// <summary>
        /// Конструктор, если необходимо прочитать шаблон из папки
        /// </summary>
        /// <param name="fileName">Название шаблона</param>
        public Pattern(string fileName) 
        {
            _patternArr = ReaderPatternFile.GetPatternArr(fileName);
            _name = fileName;
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
            _name = fileName;
            ReaderPatternFile.CreatePattern(patternArr, fileName);
            GetCenterCoords();
        }
        
        /// <summary>
        /// Метод, для реверсии всех цифр в массиве
        /// </summary>
        public void SetNegativeArr()
        {
            for (int i = 0; i < _patternArr.GetLength(0); i++)
            {
                for (int j = 0; j < _patternArr.GetLength(1); j++)
                {
                    if (_patternArr[i,j] > 3)
                    {
                        _patternArr[i, j] = -3;
                    }
                    else
                    {
                        _patternArr[i, j] = (sbyte)-_patternArr[i, j];
                    }
                }
            }
        }

        private void GetCenterCoords()
        {
            _coordsCenter.x = _patternArr.GetLength(0) / 2;
            _coordsCenter.y = _patternArr.GetLength(1) / 2;
        }

        /// <summary>
        /// Метод для получения координат, на которых будет рисунок паттерна
        /// </summary>
        /// <param name="map">Ссылка на объект класса Map</param>
        /// <param name="centerCoords">Координаты точки, где будет центр паттерна</param>
        /// <returns></returns>
        public Dictionary<Coords, sbyte> GetPatternCoords(Map map, Coords centerCoords)
        {
            Dictionary<Coords, sbyte> tempDict = new Dictionary<Coords, sbyte>();

            for (int collums = 0; collums < _patternArr.GetLength(0); collums++)
            {
                for (int row = 0; row < _patternArr.GetLength(1); row++)
                {
                    Coords mapCoords = new Coords(centerCoords.x + collums - (_patternArr.GetLength(0) / 2),
                                           centerCoords.y + row - (_patternArr.GetLength(1) / 2));

                    if (map.IsInAMap(mapCoords) && 
                        (map.IsPointAreBelow(mapCoords, _patternArr[collums, row]) || map.IsPointAreHigher(mapCoords, _patternArr[collums, row])))
                        tempDict[mapCoords] = _patternArr[collums, row];
                }
            }

            return tempDict;
        }

        public override string ToString()
        {
            string pattern = _name + "\n";
            
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
