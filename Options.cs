using System.Collections.Generic;
using System.IO;
using System;

namespace Hangman
{
        public class Options
        {
            public Mode Mode { get; set; }
            public string UserName { get; set; }
            public List<Solution> Solutions = new List<Solution>();

            public Options(string userName)
            {
                Mode = Mode.Default;
                UserName = (string.IsNullOrEmpty(userName)) ? "ANONYMOUS" : userName;
                PopulateCategories();
            }

            /// <summary>
            /// Populates depending on the filename
            /// </summary>
            public void PopulateCategories()
            {
                string path = Directory.GetCurrentDirectory();
                var files = System.IO.Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (file.Contains(".txt"))
                    {
                        string category = file.Remove(file.Length - 4).Substring(file.LastIndexOf("/") + 1);
                        foreach (string line in File.ReadAllLines(file))
                        {
                            string[] solution = line.Replace(" ", "").Split("|");
                            Solutions.Add(new Solution(solution[1], solution[0], category));
                        }
                    }
                }

                return;
            }

            public int RandomSolutionIndex()
            {
                Random rnd = new Random();
                int sIndex = rnd.Next(Solutions.Count);
                return sIndex;
            }
        }
}