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

public enum time: int { min=0,max=59,maxhr=23, };
public enum typet: int {hour=0,min,sec, };

namespace TimerLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int StackPanelSize = 2;
        bool IsRunning = false;

        private MyTimer mtimer;
        Dictionary<Button, MyTimer> timerDict = new Dictionary<Button, MyTimer>();
        Button lastButton = new Button();
        private bool showAlert = false;
        public DispatcherTimer Timer;
       // List<Button> buttons = new List<Button> ();
       // List<MyTimer> timers = new List<MyTimer>();

        public MainWindow()
        {
            InitializeComponent();

            mtimer = new MyTimer();
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.IsEnabled = false;
            Timer.Start();


            Hours_Text.MouseWheel += Mouse_Scroll;
            Minutes_Text.MouseWheel += Mouse_Scroll;
            Seconds_Text.MouseWheel += Mouse_Scroll;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timertxt.IsEnabled)
            {
                if (mtimer.stopped())
                {                   
                    
                }
                else mtimer.decval(); 
                if ((bool)CheckTimer.IsChecked)
                    timertxt.Text = mtimer.s_retval((int)typet.hour) + ':' + mtimer.s_retval((int)typet.min) + ':' + mtimer.s_retval((int)typet.sec);
                else
                    timertxt.Text = Hours_Text.Text.Trim() + ':' + Minutes_Text.Text.Trim() + ":0 (" + mtimer.s_retval((int)typet.hour) + ':' + mtimer.s_retval((int)typet.min) + ':' + mtimer.s_retval((int)typet.sec) + ')';
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
            newTimerButton.Click += SelectButton;
            TimerStackPanel.Children.Insert(StackPanelSize - 1, newTimerButton);
            StackPanelSize++;
            MyTimer NewTimer = new MyTimer();
            Timer_Tick(sender, e);
            timerDict.Add(newTimerButton, NewTimer);
            lastButton = newTimerButton;
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            timerDict.TryGetValue((Button)sender, out MyTimer nt);
            Hours_Text.Text = nt.hours.ToString();
            Minutes_Text.Text = nt.minutes.ToString();
            Seconds_Text.Text = nt.seconds.ToString();
            
            timertxt.Text= Hours_Text.Text.Trim()+":"+ Minutes_Text.Text.Trim()+":"+Seconds_Text.Text.Trim();
            if (IsRunning)
            {
                StartButton.Content = "STOP";
            }
            else
            {
                StartButton.Content = "START";
            }
            lastButton = (Button)sender;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timerDict.TryGetValue(lastButton, out MyTimer nt);
            IsRunning = !IsRunning;
            

            if (IsRunning && (bool)CheckTimer.IsChecked==true)
            {
                int hr = int.Parse(Hours_Text.Text);
                int mn = int.Parse(Minutes_Text.Text);
                int sc = int.Parse(Seconds_Text.Text);
                timertxt.IsEnabled = true;
                mtimer.setval(hr, mn, sc);
                Timer.IsEnabled = true;

                StartButton.Content = "STOP";                
            }
            else
            {
                StartButton.Content = "START";
                Timer.IsEnabled = false;
                Timer.Stop();
            }
        }

        private void Timer_Check(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckTimer.IsChecked)
            {
                Hours_Text.IsEnabled = true;
                Minutes_Text.IsEnabled = true;
                Seconds_Text.IsEnabled = true;
                timertxt.IsEnabled = true;
                if ((bool)CheckAlarm.IsChecked) CheckAlarm.IsChecked = false;
            }
            else
            {
                Hours_Text.IsEnabled = false;
                Minutes_Text.IsEnabled = false;
                Seconds_Text.IsEnabled = false;
                CheckAlarm.IsChecked = false;
            }
        }

        private void Alarm_Check(object sender,RoutedEventArgs e)
        {
            if ((bool)CheckAlarm.IsChecked)
            {
                Hours_Text.IsEnabled = true;
                Minutes_Text.IsEnabled = true;
                Seconds_Text.IsEnabled = true;
                CheckTimer.IsChecked = false;
                if ((bool)CheckTimer.IsChecked) CheckTimer.IsChecked = false;
            }
            else
            {
                Hours_Text.IsEnabled = false;
                Minutes_Text.IsEnabled = false;
                Seconds_Text.IsEnabled = false;
                CheckAlarm.IsChecked = false;
            }
        }
    }
}




