using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    //Score is comprised of a name and the number of points scored
    class Score : IComparable<Score>
    {
        private string name;
        private int points;

        public Score(int points)
        {
            this.points = points;
        }

        public Score(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public string Name
        {
            get { return name; }
        }

        public int Points
        {
            get { return points; }
        }

        //Override the default ToString method for use in displaying highscores
        public override string ToString()
        {
            return name + ": " + points.ToString();
        }

        //Used in Array.Sort, scores are compared by the points
        public int CompareTo(Score other)
        {
            if (other == null)
                return 1;
            return other.points.CompareTo(this.points);
        }
    }
}
