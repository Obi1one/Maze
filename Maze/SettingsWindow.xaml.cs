using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maze {
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
            List<string> sex = new List<string>() { "Boy", "Girl" };
            Sex.ItemsSource = sex;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e) {
            ReturnBtn.FontSize += 5;
            ReturnBtn.Width += 5;
            ReturnBtn.Height += 5;
        }

        private void ReturnBtn_MouseLeave(object sender, MouseEventArgs e) {
            ReturnBtn.FontSize -= 5;
            ReturnBtn.Width -= 5;
            ReturnBtn.Height -= 5;
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e) {
            if (Sex.Text == "") {
                ((MainWindow)Owner).sex = "Boy";
            } else {
                ((MainWindow)Owner).sex = Sex.Text;
            }
            ((MainWindow)Owner).nick = NickName.Text;
            ((MainWindow)Owner).areSettingsSet = true;
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map();
            map.Show();
        }
    }
}
