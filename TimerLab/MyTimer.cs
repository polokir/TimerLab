using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerLab
{
    
class MyTimer
    {
        public int hours;
        public int minutes;
        public int seconds;
        public bool IsRunning;
        //  private DispatcherTimer timer = new DispatcherTimer();

        public MyTimer()
        {
            this.hours = 0;
            this.minutes = 0;
            this.seconds = 0;
            this.IsRunning = false;
            // timer.Tick += new EventHandler(Timer_Tick);
            // timer.Interval = new TimeSpan(0, 0, 1);
        }

    }
}
