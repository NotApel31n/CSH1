using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace CSharpProj1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TicTacToe tictac;
        private bool playing_x = false;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            BlockMenu();
        }

        private void AllowMenu()
        {
            ResetBoard();
            tictac = new TicTacToe();
            playing_x = !playing_x;

            if (!playing_x)
            {
                BotClick(playing_x ? 1 : 2);
            }
        }

        private void ResetBoard()
        {
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    Button button = FindName($"pole{i}_{j}") as Button;
                    if (button != null)
                    {
                        button.IsEnabled = true;
                        button.Content = "";
                    }
                }
            }

            RezTextBox.Text = "";
        }

        private void BotClick(int val)
        {
            while (true)
            {
                int num1 = random.Next(1, 4);
                int num2 = random.Next(1, 4);
                Button myBut = FindName($"pole{num1}_{num2}") as Button;

                if (myBut != null && myBut.IsEnabled)
                {
                    myBut.Content = val == 1 ? "o" : "x";
                    myBut.IsEnabled = false;
                    tictac.Click(num1 - 1, num2 - 1, val); // Передаем значение игрока
                    return;
                }
            }
        }


        private bool CheckWinner()
        {
            int result = tictac.Scan();

            switch (result)
            {
                case 3:
                    RezTextBox.Text = "Ничья!";
                    BlockMenu();
                    break;
                case 2:
                    RezTextBox.Text = "Победа X!";
                    BlockMenu();
                    break;
                case 1:
                    RezTextBox.Text = "Победа O!";
                    BlockMenu();
                    break;
                default:
                    return false;
            }

            return true;
        }

        private void BlockMenu()
        {
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    Button button = FindName($"pole{i}_{j}") as Button;
                    if (button != null)
                    {
                        button.IsEnabled = false;
                    }
                }
            }

            startButton.IsEnabled = true;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            AllowMenu();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button obj = sender as Button;
            string[] prepCoords = obj.Name.Replace("pole", "").Split('_');
            int[] coords = new int[2];
            coords[0] = Convert.ToInt32(prepCoords[0]) - 1;
            coords[1] = Convert.ToInt32(prepCoords[1]) - 1;

            int player = playing_x ? 2 : 1; // Определите текущего игрока

            bool clicked = tictac.Click(coords[0], coords[1], player);
            Button myBut = FindName($"pole{prepCoords[0]}_{prepCoords[1]}") as Button;

            if (myBut != null)
            {
                myBut.Content = playing_x ? "x" : "o";
                myBut.IsEnabled = false;

                if (!CheckWinner())
                {
                    BotClick(playing_x ? 1 : 2);
                    CheckWinner();
                }
            }
        }

    }
}
