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
        private dbManage db;

        public MainWindow()
        {
            InitializeComponent();

            //setup background image
            ImageSource isource = new BitmapImage((new Uri(@"images/titleScreen.png", UriKind.Relative)));
            this.titleScreen.Background = new ImageBrush(isource);
            //this.helpScreen.Background = new ImageBrush(isource);
            //this.creditsScreen.Background = new ImageBrush(isource);
            //this.resultsScreen.Background = new ImageBrush(isource);

            //setup gameScreen
            this.db = new dbManage();
            this.game = new Game(this.gameScreen, this.db);
            this.game.GameEnded += this.showResultsScreen;

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            this.hideTitleScreen();
            this.gameScreen.Visibility = System.Windows.Visibility.Visible;
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

        private void showResultsScreen(object sender, Boolean b)
        {
            this.hideGameScreen();
            this.resultsScreen.Visibility = System.Windows.Visibility.Visible;
            Canvas temp = (Canvas)this.resultsScreen.Children[0];
            Label temp2 = (Label)temp.Children[1];
            if(b)
                temp2.Content = "Congratulations You Win!";
            else
                temp2.Content = "You Suck! Go Die of Shame!";
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
            this.game.evaluateQuestion();
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
            this.game = new Game(this.gameScreen, this.db);
            this.game.GameEnded += this.showResultsScreen;
        }

        private void gameScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.game.mouseClick(e.GetPosition(this.mapCanvas));
        }

    }
}
