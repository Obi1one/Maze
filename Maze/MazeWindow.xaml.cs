using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Maze {
    /// <summary>
    /// Interaction logic for MyMazeWindow.xaml
    /// </summary>
    public partial class MyMazeWindow : Window {
        int flag = 0;
        int id = 0;
        bool cheese = false;
        DateTime startTime;
        DispatcherTimer timer;
        string nick;
        Point lastpos;

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        public MyMazeWindow(string player , string sex) {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval += new TimeSpan(0, 0, 1);
            timer.Start();
            ImageBrush brush = new ImageBrush();
            if (sex == "Boy")
            {
                brush.ImageSource = new BitmapImage(new Uri("cheese.png", UriKind.Relative));
                System.Windows.Resources.StreamResourceInfo info = Application.GetResourceStream(new Uri("blackrat_norm.cur", UriKind.Relative));
                Labirynth.Cursor = new System.Windows.Input.Cursor(info.Stream);
            }
            else {
                brush.ImageSource = new BitmapImage(new Uri("flowerjpg.jpg", UriKind.Relative));
                System.Windows.Resources.StreamResourceInfo info = Application.GetResourceStream(new Uri("girlIsPlayer.cur", UriKind.Relative));
                Labirynth.Cursor = new System.Windows.Input.Cursor(info.Stream);
            }
            Target.Background = brush;
            Exit.Visibility = Visibility.Hidden;
            Exit.IsEnabled = false;
            Target.Visibility = Visibility.Visible;
            Target.IsEnabled = true;
            nick = player;
            startTime = DateTime.Now;
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e) {
            if (cheese == true) {
                Target.IsEnabled = true;
                Target.Visibility = Visibility.Visible;
                Exit.Visibility = Visibility.Hidden;
                Exit.IsEnabled = false;
            }
            Point position = startLabel.PointToScreen(new Point(0d, 0d));
            SetCursorPos((int)position.X - 60, (int)position.Y + 30);
            
        }

        private void wdw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult result = MessageBox.Show("Are you shure you want to exit maze?", "Quit?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    MessageBoxResult exitRes = MessageBox.Show("Enjoy your game!");
                    if (exitRes == MessageBoxResult.OK)
                    {
                        Point position = startLabel.PointToScreen(new Point(0d, 0d));
                        SetCursorPos((int)position.X - 60, (int)position.Y + 30);
                    }
                }
            }
            else if (e.Key == Key.Enter)
            {
                startTime = DateTime.Now;
                Point position = startLabel.PointToScreen(new Point(0d, 0d));
                SetCursorPos((int)position.X - 60, (int)position.Y + 30);
            }
            else if (e.Key == Key.H) {
                Mazeexit exit = new Mazeexit();
                exit.Show();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                Trap1.IsEnabled = true;
                Trap2.IsEnabled = true;
                Trap1.Visibility = Visibility.Visible;
                Trap2.Visibility = Visibility.Visible;
                flag = 1;
            }
            else {
                Trap1.IsEnabled = false;
                Trap2.IsEnabled = false;
                Trap1.Visibility = Visibility.Hidden;
                Trap2.Visibility = Visibility.Hidden;
                flag = 0;
            }
        }

        private void Target_MouseEnter(object sender, MouseEventArgs e)
        {
            Exit.IsEnabled = true;
            Exit.Visibility = Visibility.Visible;
            Target.Visibility = Visibility.Hidden;
            Target.IsEnabled = false;
            cheese = true;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            timer.Stop();
            this.Close();
            ResultWindow resWindow = new ResultWindow();
            using (ActualResultContext rslt = new ActualResultContext()) {
                id = rslt.Results.Count();
                Result rs = new Result { ID = ++id, NickName = nick, TimeResult = DateTime.Now - startTime };
                rslt.Results.Add(rs);
                rslt.SaveChangesAsync();
                resWindow.Result.ItemsSource = rslt.Results.ToList();
            }
            MessageBoxResult exit = MessageBox.Show("Congratulations!!" + "\n" + "Your Time: " + (DateTime.Now - startTime).ToString());
            if (exit == MessageBoxResult.OK) {
                resWindow.Show();
            }
            
        }

        private void wdw_MouseMove(object sender, MouseEventArgs e)
        {
            lastpos = e.GetPosition(wdw);
        }

        private void wdw_Loaded(object sender, RoutedEventArgs e)
        {
            Point position = startLabel.PointToScreen(new Point(0d, 0d));
            SetCursorPos((int)position.X - 60, (int)position.Y + 30);
        }
    }
}
