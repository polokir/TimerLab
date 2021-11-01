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

        public MyTimer()
        {
            this.hours = 0;
            this.minutes = 0;
            this.seconds = 0;
            this.IsRunning = false;
            
        }

        public MyTimer(int newh = 0, int newm = 0, int news = 0)
        {
            hours = newh;
            minutes = newm;
            seconds = news;
        }

        public void decval()
        {
            if (!IsRunning)
            {
                seconds--;
                if (seconds < (int)time.min)
                {
                    seconds = (int)time.max;
                    minutes--;
                    if (minutes < (int)time.min)
                    {
                        minutes = (int)time.max;
                        hours--;
                        if (hours < (int)time.min)
                        {
                            hours = (int)time.maxhr;
                            IsRunning = true;
                        }
                    }
                }
            }
        }

        public void setval(int newh = 0, int newm = 0, int news = 0) { hours = newh; minutes = newm; seconds = news; IsRunning = false; }

        public int retval_i(int retype)
        {
            if (retype == 0) return hours;
            else if (retype == 1) return minutes;
            else return seconds;
        }

        public string s_retval(int retype)
        {
            if (retype == 0) return hours.ToString();
            else if (retype == 1) return minutes.ToString();
            else return seconds.ToString();
        }

        public bool stopped()
        {
            return IsRunning;
        }
    }
}
