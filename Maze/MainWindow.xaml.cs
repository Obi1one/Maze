using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;

namespace Maze {

    public partial class MainWindow : Window {
        public ActualResultContext rslt = new ActualResultContext();
        public MainWindow() {
            InitializeComponent();
        }

        private void Settings_MouseEnter(object sender, MouseEventArgs e) {
            ((Button)sender).FontSize += 10;
            ((Button)sender).Width += 10;
            ((Button)sender).Height += 10;
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e) {
            ((Button)sender).FontSize -= 10;
            ((Button)sender).Width -= 10;
            ((Button)sender).Height -= 10;
        }

        SettingsWindow SetSettings;
        bool wereSettingsOpened = false;
        public string sex;
        public string nick;
        public bool areSettingsSet = false;
        private void Settings_Click(object sender, RoutedEventArgs e) {
            if (wereSettingsOpened == false) {
                SetSettings = new SettingsWindow();
                SetSettings.Owner = this;
                SetSettings.Show();
                wereSettingsOpened = true;
            } else if (wereSettingsOpened == true) {
                SetSettings.Show();
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e) {
            if (wereSettingsOpened == false)
            {
                MessageBoxResult result = MessageBox.Show("Are you suer you want to start? Check if You picked character and nickname.", "Start?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    SetSettings = new SettingsWindow();
                    SetSettings.Owner = this;
                    SetSettings.Show();
                    wereSettingsOpened = true;
                }
                else {
                    
                }
            }
            if (sex == null)
            {
                sex = "Boy";
            }
            if (nick == null)
            {
                nick = "Player";
            }
            MyMazeWindow game = new MyMazeWindow(nick, sex);
            game.Owner = this;
            game.Show();            

        }

        private void ScoresBorad_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resWindow = new ResultWindow();
            resWindow.Result.ItemsSource = rslt.Results.ToList();
            resWindow.Show();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help helpWindow = new Help();
            helpWindow.Show();
        }
    }
}
