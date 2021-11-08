using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Media;



namespace TimerLab
{
    class Ring
    {
        private SoundPlayer snd;

        public Ring(string file) // constructor
        {
            //file= @"C:\Users\user\Downloads\snd.mp3"; ;
            snd = new SoundPlayer(file); // testing for file done in main prg.
        }

        public void play() { snd.Play(); } // play the sound
        public void stop() { snd.Stop(); } // stop the sound
    }

    class MyTimer
    {
        public int hours;
        public int minutes;
        public int seconds;
        public bool hasStopped;
       

        
        public MyTimer(string hrs,string mnt,string scnd)
        {
            this.hours=int.Parse(hrs);
            this.minutes = int.Parse(mnt);
            this.seconds = int.Parse(scnd);
            this.hasStopped = false;
            
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

        

        public bool stopped()
        {
            return hasStopped;
        }
    }
}
