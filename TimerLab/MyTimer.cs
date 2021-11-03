using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TimerLab
{
    
class MyTimer
    {
        public int hours;
        public int minutes;
        public int seconds;
        public bool hasStopped;
        
        

        public MyTimer()
        {
            this.hours=0;
            this.minutes = 0;
            this.seconds = 0;
            this.hasStopped = false;          
        }

        

        public MyTimer(int newh = 0, int newm = 0, int news = 0)
        {
            hours = newh;
            minutes = newm;
            seconds = news;
        }
        
        public void decval()
        {       
            if (seconds == 0)
            {
                if (minutes == 0)
                {
                    if (hours == 0)
                    {
                        hasStopped= true;                                          
                    }
                    else
                    {
                        hours -= 1;
                        minutes = 59;                        
                        seconds = 60;
                    }
                }
                else
                {
                    minutes -= 1;                   
                    seconds = 60;
                }
            }



            if (hasStopped == false)
            {
                seconds -= 1;              
            }
        }
        
        public void setval(int newh = 0, int newm = 0, int news = 0) { hours = newh; minutes = newm; seconds = news; hasStopped = false; }

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
            return hasStopped;
        }
    }
}
