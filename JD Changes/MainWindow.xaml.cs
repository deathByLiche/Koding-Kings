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
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            dbManage d = new dbManage();
            //setup background image
            ImageSource isource = new BitmapImage((new Uri(@"images/titleScreen.png", UriKind.Relative)));
            this.titleScreen.Background = new ImageBrush(isource);
            //this.titleScreen.Background = new ImageBrush() { ImageSource = new BitmapImage((new Uri(@"images/titleScreen.png", UriKind.Relative))) };

            this.game = new Game(this.gameScreen);
            this.game.GameEnded += new EventHandler(this.showResultsScreen);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            this.hideTitleScreen();
            this.gameScreen.Visibility = System.Windows.Visibility.Visible;

            this.game.promptForUsername();
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            this.hideTitleScreen();
            this.helpScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void creditsButton_Click(object sender, RoutedEventArgs e)
        {
            this.hideTitleScreen();
            this.creditsScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void showTitleScreen()
        {
            this.titleScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void showResultsScreen(object sender, EventArgs e)
        {
            this.hideGameScreen();
            this.resultsScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void hideTitleScreen()
        {
            this.titleScreen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void hideGameScreen()
        {
            this.gameScreen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void hideHelpScreen()
        {
            this.helpScreen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void hideCreditsScreen()
        {
            this.creditsScreen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void hideResultsScreen()
        {
            this.resultsScreen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void doneButtonGameScreen_Click(object sender, RoutedEventArgs e)
        {
            this.hideGameScreen();
            this.titleScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void doneButtonHelpScreen_Click(object sender, RoutedEventArgs e)
        {
            this.hideHelpScreen();
            this.showTitleScreen();
        }

        private void doneButtonCreditsScreen_Click(object sender, RoutedEventArgs e)
        {
            this.hideCreditsScreen();
            this.showTitleScreen();
        }

        private void doneButtonResultsScreen_Click(object sender, RoutedEventArgs e)
        {
            this.hideResultsScreen();
            this.showTitleScreen();
        }

        //for testing only

        private void gameScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.game.mouseClick(e.GetPosition(this.mapCanvas));
            //this.game.checkIfGameOver();
        }

    }
}
