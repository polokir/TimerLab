using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

namespace TimerLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int StackPanelSize = 2;
        //bool StoreBeforeLaunch = false;

        MyTimer mtimer = new MyTimer();
        Dictionary<Button, MyTimer> timerDict = new Dictionary<Button, MyTimer>();
        Button lastButton = new Button();
        public DispatcherTimer Timer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);


            Hours_Text.MouseWheel += Mouse_Scroll;
            Minutes_Text.MouseWheel += Mouse_Scroll;
            Seconds_Text.MouseWheel += Mouse_Scroll;
        }



        private void Timer_Tick(object sender, EventArgs e)
        {

            if (mtimer.seconds == 0)
            {
                if (mtimer.minutes == 0)
                {
                    if (mtimer.hours == 0)
                    {
                        Timer.IsEnabled = false;
                        mtimer.IsRunning = false;
                    }
                    else
                    {
                        mtimer.hours -= 1;

                        Hours_Text.Text = (mtimer.hours).ToString("00");

                        mtimer.minutes = 59;
                        Minutes_Text.Text = (mtimer.minutes).ToString("00");

                        mtimer.seconds = 60;
                        mtimer.IsRunning = true;
                    }
                }
                else
                {
                    mtimer.minutes -= 1;
                    Minutes_Text.Text = mtimer.minutes.ToString("00");
                    mtimer.seconds = 60;
                    mtimer.IsRunning = true;
                }
            }



            if (Timer.IsEnabled == true)
            {
                mtimer.seconds -= 1;
                Seconds_Text.Text = mtimer.seconds.ToString("00");
                mtimer.IsRunning = true;
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

        private void EnableButtons()
        {
            Hours_Text.IsReadOnly = false;
            Minutes_Text.IsReadOnly = false;
            Seconds_Text.IsReadOnly = false;
        }

        private void DisableButtons()
        {
            Hours_Text.IsReadOnly = true;
            Minutes_Text.IsReadOnly = true;
            Seconds_Text.IsReadOnly = true;
        }
    }



}
