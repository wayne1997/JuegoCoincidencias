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

namespace JuegoCoincidencias
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int timeOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick1; 
            SetUpGame();
        }

        private void Timer_Tick1(object sender, EventArgs e)
        {
            timeOfSecondsElapsed++;
            timeTextBlock.Text = (timeOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8) {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + "- ¿Nuevo juego?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>() {
                "🐙", "🐙",
                "😀", "😀",
                "🐘", "🐘",
                "🐳", "🐳",
                "😍", "😍",
                "🦕", "🦕",
                "🦘", "🦘",
                "😎", "😎",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) {
                if(textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            timeOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBoxClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false) {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBoxClicked = textBlock;
                findingMatch = true;
            } else if (textBlock.Text == lastTextBoxClicked.Text) {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBoxClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (matchesFound == 8) {
                
                SetUpGame();
            }
        }
    }
}
