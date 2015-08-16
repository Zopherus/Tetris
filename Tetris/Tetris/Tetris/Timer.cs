using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    //Used for timing multiple things
    class Timer
    {
        //Time in milliseconds
        private int timeMilliseconds = 0;
        //The interval at which to do things
        private int interval;
        //true if the timer is running
        private bool started;

        public Timer(int interval)
        {
            this.interval = interval;
        }

        public Timer(int timeMilliseconds, int interval)
        {
            this.timeMilliseconds = timeMilliseconds;
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
            set
            {
                if (value >= 0)
                    interval = value;
            }
        }

        public void start() 
        {
            started = true;
        }

        //Add the time in milliseconds to the timer if the timer is started
        public void tick(GameTime gameTime) 
        {
            if (started)
                timeMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
        }

        //Resets the time to 0
        public void resetTimer()
        {
            timeMilliseconds = 0;
        }

        //creates a certain number of timers into a list with a certain interval 
        public static IEnumerable<Timer> Create(int count, int interval)
        {
            List<Timer> myList = new List<Timer>();
            for (var i = 0; i < count; i++)
            {
                myList.Add(new Timer(interval));
            }
            return myList;
        }
    }
}
