using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Millionaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartGame();
        }
    }
    class ReadFromFile
    {
        public List<Level> ReadFile()
        {
            List<Level> GameSet = new List<Level>();

            StreamReader qreader = new StreamReader(File.OpenRead(@"C:\Users\danil\source\repos\ITEA.Kudlyak\ITEA.GAME\bin\Debug\netcoreapp3.1\1.txt"));
            {
                while (!qreader.EndOfStream)
                {
                    string line = qreader.ReadLine();

                    string[] values = line.Split(',');

                    string levelName = values[0];

                    string levelQuestion = values[1];

                    Dictionary<string, bool> answers = new Dictionary<string, bool>()
                                           {
                                             { values[2].ToString(), Convert.ToBoolean(values[3]) },
                                             { values[4].ToString(), Convert.ToBoolean(values[5]) },
                                             { values[6].ToString(), Convert.ToBoolean(values[7]) },
                                             { values[8].ToString(), Convert.ToBoolean(values[9])  }
                                           };

                    Level Level = new Level(levelName, levelQuestion, answers);

                    GameSet.Add(Level);

                }
            }
            return GameSet;
        }
    }
    class Level : ILevel
    {
        public string LevelName { get; set; }
        public string LevelQuestion { get; set; }
        public Dictionary<string, bool> LevelAnswers { get; set; }

        public Level(string name, string question, Dictionary<string, bool> answers)
        {
            LevelQuestion = question;
            LevelAnswers = answers;
            LevelName = name;
        }

    }
    interface ILevel
    {
        string LevelName { get; set; }
        string LevelQuestion { get; set; }
        Dictionary<string, bool> LevelAnswers { get; set; }

    }
    class Game
    {
        public void StartGame()
        {
            ReadFromFile firstgame = new ReadFromFile();
            var LevelsList = firstgame.ReadFile();

            List<int> rewards = new List<int>() { 500, 1000, 2000, 4000, 8000, 16000, 32000, 65000, 125000, 250000, 500000, 1000000 };

            int rewardsIndex = 0;

            foreach (Level level in LevelsList)
            {
                Console.WriteLine(level.LevelName);

                Console.WriteLine(level.LevelQuestion);

                foreach (var answer in level.LevelAnswers)
                {
                    Console.WriteLine($"{answer.Key}");
                }

                Console.WriteLine("Chose the answer from 1 to 4");

                var chosenAnswer = Console.ReadLine().ToString();

                bool result = level.LevelAnswers.FirstOrDefault(x => x.Key.Contains(chosenAnswer)).Value;

                if (result)
                {
                    Console.WriteLine($"Well done! This is right answer! You won {rewards[rewardsIndex]}");
                    //Console.WriteLine("You can take money or continue playing");
                }

                else { Console.WriteLine("Yuo loose!!! HA-HA-HA"); break; }

                rewardsIndex++;

            }
        }

    }
}
