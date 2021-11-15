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
            //file= @"C:\Users\user\Downloads\videoplayback.mp3"; ;
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
        private SoundPlayer snd;
        //public DispatcherTimer ClsTimer;



        public MyTimer(string hrs, string mnt, string scnd)
        {
            this.hours = int.Parse(hrs);
            this.minutes = int.Parse(mnt);
            this.seconds = int.Parse(scnd);
            this.hasStopped = true;

            /*ClsTimer = new DispatcherTimer();
            ClsTimer.Tick += new EventHandler(Timer_Tick);
            ClsTimer.Interval = new TimeSpan(0, 0, 1);
            ClsTimer.IsEnabled = true;
            ClsTimer.Start();*/

        }



        public MyTimer(int newh = 0, int newm = 0, int news = 0, bool hs = false)
        {
            hours = newh;
            minutes = newm;
            seconds = news;
            hasStopped = hs;
            snd = new SoundPlayer(@"C:\Users\user\Downloads\snd2.wav");
        }

        public void decval()
        {
            if (seconds == 0)
            {
                if (minutes == 0)
                {
                    if (hours == 0)
                    {
                        hasStopped = true;
                    }
                    else
                    {
                        hours--;
                        minutes = 59;
                        seconds = 60;
                    }
                }
                else
                {
                    minutes--;
                    seconds = 60;
                }
            }



            if (hasStopped == false)
            {
                seconds -= 1;
            }
        }

        public void setval(int newh = 0, int newm = 0, int news = 0) { hours = newh; minutes = newm; seconds = news; hasStopped = false; }

        public void PlaySnd()
        {
            for(int i = 0; i < 1; ++i)
            {
                snd.Play();
            }

        }


        public bool stopped()
        {
            return hasStopped;
        }

        public bool IsNull()
        {
            if (hours == 0)
            {
                if (minutes == 0)
                {
                    if (seconds == 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
    }
}
