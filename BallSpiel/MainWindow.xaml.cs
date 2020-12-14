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
using System.Windows.Threading;

namespace BallSpiel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Richtung
        {
            Rechts,
            Links,
            Rauf,
            Runter
        }


        private readonly DispatcherTimer _animationsTimer = new DispatcherTimer();              // timer anlegen
        private Richtung BallXRichtung = Richtung.Rechts;
        private Richtung BallYRichtung = Richtung.Runter;
        private int Zaehler = 0;


        public MainWindow()
        {
            InitializeComponent();

            _animationsTimer.Interval = TimeSpan.FromMilliseconds(50);                         // timer configuration
            _animationsTimer.Tick += PositioniereBall;                                          // Aktion wenn Timer abgelaufen ist

        }

        private void PositioniereBall(object sender, EventArgs e)
        {
            /********************************************
             * X-Richtung
             ********************************************/
            var x = Canvas.GetLeft(Ball);

            if (x >= Spielplatz.ActualWidth - Ball.ActualWidth)
            {
                BallXRichtung = Richtung.Links;
            }

            if (x <= 0)
            {
                BallXRichtung = Richtung.Rechts;
            }

            if (BallXRichtung == Richtung.Rechts)
            {
                Canvas.SetLeft(Ball, x + 10);
            }
            else
            {
                Canvas.SetLeft(Ball, x - 10);
            }

            /********************************************
             * Y-Richtung
             ********************************************/
            var y = Canvas.GetTop(Ball);

            if (y >= Spielplatz.ActualHeight - Ball.ActualHeight)
            {
                BallYRichtung = Richtung.Rauf;
            }

            if (y <= 0)
            {
                BallYRichtung = Richtung.Runter;
            }

            if (BallYRichtung == Richtung.Runter)
            {
                Canvas.SetTop(Ball, y + 10);
            }
            else
            {
                Canvas.SetTop(Ball, y - 10);
            }



        }




        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_animationsTimer.IsEnabled)
            {
                _animationsTimer.Stop();
            }
            else
            {
                _animationsTimer.Start();
                Zaehler = 0;
                SpielStandLabel.Content = $"{Zaehler} Clicks";
            }
            
        }

        private void Ball_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_animationsTimer.IsEnabled)
            {
                Zaehler += 1;
                SpielStandLabel.Content = $"{Zaehler} Clicks";
            }
        }

        private void Ball_KeyUp(object sender, KeyEventArgs e)          // Beachte: Ball_KeyUp ist im "MainWindow"
        {
            if (e.Key == Key.R)
            {
                Ball.Fill = Brushes.Red;
            }
            else if (e.Key == Key.B)
            {
                Ball.Fill = Brushes.Blue;
            }
        }

        private void Spielplatz_Loaded(object sender, RoutedEventArgs e)
        {                                                                                                     // Nach "Loaded"
            Canvas.SetLeft(Ball, (Spielplatz.ActualWidth  / 2) - (Ball.ActualWidth  / 2) );                   // Setze Ball in Mitte
            Canvas.SetTop(Ball,  (Spielplatz.ActualHeight / 2) - (Ball.ActualHeight / 2));

            

        }
    }
}
