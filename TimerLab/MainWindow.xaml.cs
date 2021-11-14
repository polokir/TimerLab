using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;
using System.IO;
using System.Media;



namespace TimerLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int StackPanelSize = 2;
        bool IsRunning = false;


        Dictionary<Button, MyTimer> timerDict;
        Button lastButton = new Button();
        public DispatcherTimer Timer;

        Dictionary<Button, MyAlarm> alarmDict;
        Button lastButton1 = new Button();
        public DispatcherTimer Alarm;

        //private Ring ring;


        private SoundPlayer sound;

        public MainWindow()
        {
            InitializeComponent();


            Timer = new DispatcherTimer();
            timerDict = new Dictionary<Button, MyTimer>();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.IsEnabled = true;
            Timer.Start();

            Alarm = new DispatcherTimer();
            alarmDict = new Dictionary<Button, MyAlarm>();
            Alarm.Tick += new EventHandler(Alarm_Tick);
            Alarm.Interval = new TimeSpan(0, 0, 1);
            Alarm.IsEnabled = true;
            Alarm.Start();

            string filep = @"C:\Users\user\Downloads\snd2.wav";
            sound = new SoundPlayer(filep);


            Hours_Text.MouseWheel += Mouse_Scroll;
            Minutes_Text.MouseWheel += Mouse_Scroll;
            Seconds_Text.MouseWheel += Mouse_Scroll;
        }

        private void Alarm_Tick(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;

            Hrs_Text.Text = d.Hour.ToString();
            Mnt_Text.Text = d.Minute.ToString();
            Sec_Text.Text = d.Second.ToString();
            ThroughDictAlarm();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            ThroughDict();

                if (timerDict.TryGetValue(lastButton, out MyTimer value))
                {
                    Hours_Text.Text = value.hours.ToString();
                    Minutes_Text.Text = value.minutes.ToString();
                    Seconds_Text.Text = value.seconds.ToString();
                    if (value.hours == 0 && value.minutes == 0 && value.seconds == 0)
                    {
                     value.hasStopped = true;
                    }
                }
        }
        private void PlaySound()
        {
            for (int i = 0; i < 1; ++i)
            {
                sound.Play();
            }
        }


        private void ThroughDict()
        {
            foreach (var element in timerDict)
            {
                if (element.Value.hasStopped == false)
                {
                    element.Value.decval();
                }
                else
                {
                    PlaySound();
                }              
            }


        }

        private void ThroughDictAlarm()
        {
            foreach (var element in alarmDict)
            {
                if (element.Value.IsTime(int.Parse(Hrs_Text.Text), int.Parse(Mnt_Text.Text), int.Parse(Sec_Text.Text)))
                {
                    PlaySound();
                }
            }

        }

        private void Mouse_Scroll(object sender, MouseWheelEventArgs e)
        {
            if (lastButton != null)
            {
                timerDict.TryGetValue(lastButton, out MyTimer nt);

                int i = 0;
                if (e.Delta > 0)
                {
                    i = 1;
                }
                else if (e.Delta < 0)
                {
                    i = -1;
                }

                switch (((TextBox)sender).Name)
                {
                    case "Hours_Text":
                        if ((int.Parse(((TextBox)sender).Text) + i) > 23)
                        {
                            i = 0;
                            ((TextBox)sender).Text = "0";
                        }
                        else if ((int.Parse(((TextBox)sender).Text) + i) < 0)
                        {
                            i = 0;
                            ((TextBox)sender).Text = "23";
                        }
                        else { ((TextBox)sender).Text = (int.Parse(((TextBox)sender).Text) + i).ToString(); }
                        nt.hours = int.Parse(((TextBox)sender).Text);


                        break;
                    case "Minutes_Text":
                        if ((int.Parse(((TextBox)sender).Text) + i) > 59)
                        {
                            i = 0;
                            ((TextBox)sender).Text = "0";
                        }
                        else if ((int.Parse(((TextBox)sender).Text) + i) < 0)
                        {
                            i = 0;
                            ((TextBox)sender).Text = "59";
                        }
                        else { ((TextBox)sender).Text = (int.Parse(((TextBox)sender).Text) + i).ToString(); }
                        ((TextBox)sender).Text = (int.Parse(((TextBox)sender).Text) + i).ToString();
                        nt.minutes = int.Parse(((TextBox)sender).Text);
                        break;
                    case "Seconds_Text":
                        if ((int.Parse(((TextBox)sender).Text) + i) > 59)
                        {
                            i = 0;
                            ((TextBox)sender).Text = "0";
                        }
                        else if ((int.Parse(((TextBox)sender).Text) + i) < 0)
                        {
                            i = 0;
                            ((TextBox)sender).Text = "59";
                        }
                        else { ((TextBox)sender).Text = (int.Parse(((TextBox)sender).Text) + i).ToString(); }
                        ((TextBox)sender).Text = (int.Parse(((TextBox)sender).Text) + i).ToString();
                        nt.seconds = int.Parse(((TextBox)sender).Text);
                        break;
                    default:
                        break;
                }


            }
        }

        private void AddTimer_Click(object sender, RoutedEventArgs e)
        {

            var newTimerButton = new Button() { Content = "Timer" + " " + (StackPanelSize - 1).ToString(), Height = 150 };
            newTimerButton.Click += SelectButtonTimer;
            TimerStackPanel.Children.Insert(StackPanelSize - 1, newTimerButton);
            StackPanelSize++;

            MyTimer NewTimer = new MyTimer();
            NewTimer.setval(int.Parse(Hours_Text.Text), int.Parse(Minutes_Text.Text), int.Parse(Seconds_Text.Text));

            timerDict.Add(newTimerButton, NewTimer);
            lastButton = newTimerButton;
            Timer.IsEnabled = false;
            //IsRunning = true;


            Hours_Text.Text = "0";
            Minutes_Text.Text = "0";
            Seconds_Text.Text = "0";
        }

        private void AddAlarm_Click(object sender, RoutedEventArgs e)
        {
            var newAlarmButton = new Button() { Content = "Alarm" + " " + (StackPanelSize - 1).ToString(), Height = 150 };
                newAlarmButton.Click += SelectButtonAlarm;
                TimerStackPanel.Children.Insert(StackPanelSize - 1, newAlarmButton);
                StackPanelSize++;

                MyAlarm NewAlarm = new MyAlarm();
                NewAlarm.setval(int.Parse(Hours_Text.Text), int.Parse(Minutes_Text.Text), int.Parse(Seconds_Text.Text));

                alarmDict.Add(newAlarmButton, NewAlarm);
                lastButton1 = newAlarmButton;

                Hours_Text.Text = "0";
                Minutes_Text.Text = "0";
                Seconds_Text.Text = "0";
                Timer.IsEnabled = false;
                StartButton.Content = "SAVE";
        }

        private void SelectButtonTimer(object sender, RoutedEventArgs e)
        {
           
                if (timerDict.TryGetValue((Button)sender, out MyTimer nt))
                {
                    Hours_Text.Text = nt.hours.ToString();
                    Minutes_Text.Text = nt.minutes.ToString();
                    Seconds_Text.Text = nt.seconds.ToString();

                    if (IsRunning)
                    {
                        StartButton.Content = "STOP";
                        Timer.IsEnabled = true;
                    }
                    else
                    {
                        StartButton.Content = "START";
                        Timer.IsEnabled = false;
                    }
                    lastButton = (Button)sender;
                }
        }

        private void SelectButtonAlarm(object sender, RoutedEventArgs e)
        {

            if (alarmDict.TryGetValue((Button)sender, out MyAlarm nt))
            {
                Hours_Text.Text = nt.hours.ToString();
                Minutes_Text.Text = nt.minutes.ToString();
                Seconds_Text.Text = nt.seconds.ToString();

                StartButton.Content = "SAVE";

                lastButton1 = (Button)sender;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

            if (timerDict.TryGetValue(lastButton, out MyTimer nt))
            {
                nt.hours = int.Parse(Hours_Text.Text);
                nt.minutes = int.Parse(Minutes_Text.Text);
                nt.seconds = int.Parse(Seconds_Text.Text);
                IsRunning = !IsRunning;

                if (IsRunning )
                {
                    Timer.IsEnabled = true;
                    StartButton.Content = "STOP";
                }
                else
                {
                    StartButton.Content = "START";
                    Timer.IsEnabled = false;
                }
            }
            else if (alarmDict.TryGetValue(lastButton1,out MyAlarm at))
            {
                at.hours = int.Parse(Hours_Text.Text);
                at.minutes = int.Parse(Minutes_Text.Text);
                at.seconds = int.Parse(Seconds_Text.Text);
            }
            
        }
    }
}





