using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    //Used for timing multiple things
    class Timer
    {
        //Time in milliseconds
        private int timeMilliseconds = 0;
        //The interval at which to do things
        private int interval;

        public Timer(int interval)
        {
            this.interval = interval;
        }

        public int TimeMilliseconds
        {
            get { return timeMilliseconds; }
            set
            {
                if (value >= 0)
                    timeMilliseconds = value;
            }
        }

        public int Interval
        {
            get { return interval; }
        }

        //Resets the time to 0
        public void resetTimer()
        {
            timeMilliseconds = 0;
        }
    }
}
