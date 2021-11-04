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
        //public DispatcherTimer ClsTimer;




        public MyTimer(string hrs,string mnt,string scnd)
        {
            this.hours=int.Parse(hrs);
            this.minutes = int.Parse(mnt);
            this.seconds = int.Parse(scnd);
            this.hasStopped = false;
            /*
            ClsTimer=new DispatcherTimer();
            ClsTimer.Tick += new EventHandler(Timer_Tick);
            ClsTimer.Interval = new TimeSpan(0, 0, 1);
            ClsTimer.IsEnabled = false;
            ClsTimer.Start();*/
        }

        


        public MyTimer(int newh = 0, int newm = 0, int news = 0,bool hs=false)
        {
            hours = newh;
            minutes = newm;
            seconds = news;
            hasStopped = hs;
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
                        hours --;
                        minutes = 59;                        
                        seconds = 60;
                    }
                }
                else
                {
                    minutes --;                   
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
