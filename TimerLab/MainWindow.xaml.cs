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


        private MyTimer mtimer;
        Dictionary<Button, MyTimer> timerDict = new Dictionary<Button, MyTimer>();
        Button lastButton = new Button();
        private bool showAlert = false;


        public MainWindow()
        {
            InitializeComponent();

            mtimer = new MyTimer();
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();


            Hours_Text.MouseWheel += Mouse_Scroll;
            Minutes_Text.MouseWheel += Mouse_Scroll;
            Seconds_Text.MouseWheel += Mouse_Scroll;
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            mtimer.setval(int.Parse(Hours_Text.Text), int.Parse(Minutes_Text.Text), int.Parse(Seconds_Text.Text));
            if (timertxt.IsEnabled)
            {
                if (mtimer.stopped())
                {
                    showAlert = !showAlert;
                    if (showAlert)
                    {
                        this.Hours_Text.Visibility = Visibility.Visible;
                        this.Minutes_Text.Visibility = Visibility.Visible;
                        this.Seconds_Text.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.Hours_Text.Visibility = Visibility.Hidden;
                        this.Minutes_Text.Visibility = Visibility.Hidden;
                        this.Seconds_Text.Visibility = Visibility.Hidden;
                    }
                }
                else mtimer.decval();
                if (!showAlert)
                    timertxt.Text = mtimer.s_retval((int)typet.hour) + ':' + mtimer.s_retval((int)typet.min) + ':' + mtimer.s_retval((int)typet.sec);
                else
                    timertxt.Text = timertxt.Text.Trim() + ':' + timertxt.Text.Trim() + ":0 (" + mtimer.s_retval((int)typet.hour) + ':' + mtimer.s_retval((int)typet.min) + ':' + mtimer.s_retval((int)typet.sec) + ')';
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
            timerDict.Add(newTimerButton, NewTimer);
            lastButton = newTimerButton;
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            timerDict.TryGetValue((Button)sender, out MyTimer nt);
            Hours_Text.Text = nt.hours.ToString();
            Minutes_Text.Text = nt.minutes.ToString();
            Seconds_Text.Text = nt.seconds.ToString();
            if (nt.IsRunning)
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
            nt.IsRunning = !nt.IsRunning;
            Timer_Tick(sender, e);

            if (nt.IsRunning)
            {
                Timer_Tick(sender, e);
                StartButton.Content = "STOP";
            }
            else
            {
                StartButton.Content = "START";
            }
        }
       /*
       private void Check_Timer(object sender, RoutedEventArgs e)
        {
            if ((bool)this.TimerCheck.IsChecked)
            {
                Hours_Text.IsEnabled = true;
                Minutes_Text.IsEnabled = true;
                Seconds_Text.IsEnabled = true;
                AlarmCheck.IsChecked = false;
            }
            else
            {
                Hours_Text.IsEnabled = false;
                Minutes_Text.IsEnabled = false;
                Seconds_Text.IsEnabled = false;
                AlarmCheck.IsChecked = true;

            }
       */

        }
    }




