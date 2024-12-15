using System.Collections.Generic;
using System.IO;
using universitycollege.finding.model;
using universitycollege.finding.view;

namespace universitycollege.finding.controller
{
    /// <summary>
    /// Контроллер для выбранных шаблонов рисунков
    /// </summary>
    public class PatternController
    {
        private List<Pattern> _allPatternsList;
        private List<Pattern> _selectedPatterns;
        private string _directoryPath = InMemory.PathToPattern;

        public List<Pattern> AllPatternsList => _allPatternsList;
        public List<Pattern> SelectedPatterns => _selectedPatterns;

        public PatternController() 
        { 
            GetAllPatterns();
        }

        private void GetAllPatterns()
        {
            List<Pattern> patterns = new List<Pattern>();

            string[] files = Directory.GetFiles(_directoryPath);

            foreach (string file in files)
            {
                string fileName = System.IO.Path.GetFileName(file);
                patterns.Add(new Pattern(fileName));
            }

            _allPatternsList = patterns;
        }

        public void UsePattern(Pattern pattern)
        {
            _selectedPatterns.Add(pattern);
        }

        public void UseAllPatterns()
        {
            _selectedPatterns = _allPatternsList;
        }
    }
}
