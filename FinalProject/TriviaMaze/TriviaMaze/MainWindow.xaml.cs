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
using System.Windows.Shapes;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            this.gameScreen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            this.titleScreen.Visibility = System.Windows.Visibility.Hidden;
            this.gameScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            this.gameScreen.Visibility = System.Windows.Visibility.Hidden;
            this.titleScreen.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
