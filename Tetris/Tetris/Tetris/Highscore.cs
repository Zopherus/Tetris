using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace Tetris
{
    class Highscore
    {
        public static string currentName = "";
        private static Score[] scores = new Score[10];

        public static Score[] Scores
        {
            get { return scores; }
        }

        public static void ReadFromFile()
        {
            Array.Clear(scores, 0, scores.Length);
            string line = "";
            using (StreamReader sr = new StreamReader("Content/Highscores.txt"))
            {
                while (true)
                {
                    for (int counter = 0; counter < scores.Length; counter ++)
                    {
                        line = sr.ReadLine();
                        if(line == null)
                        {
                            break;
                        }
                        String[] array = line.Split(new Char[] { ',' });
                        scores[counter] = new Score(array[0], int.Parse(array[1]));
                    }
                    break;
                }
                sr.Close();
            }
        }

        private static void WriteToFile()
        {
            using (StreamWriter sw = new StreamWriter("Content/Highscores.txt"))
            {
                foreach (Score score in scores)
                {
                    if (score != null)
                        sw.WriteLine(score.Name.Trim()+ "," + score.Points);
                }
                sw.Close();
            }
        }

        public static void sortScores()
        {
            Array.Sort(scores);
        }

        public static void addScore(Score score)
        {
            for (int counter = 0; counter < 10; counter++)
            {
                if (scores[counter] == null || scores[counter].Points < score.Points)
                {
                    insert(score, counter);
                    WriteToFile();
                    sortScores();
                    return;
                }
            }
        }

        private static void insert (Score score, int position)
        {
            for (int counter = 8; counter >= position; counter--)
            {
                scores[counter + 1] = scores[counter];
            }
            scores[position] = score;
        }
    }
}