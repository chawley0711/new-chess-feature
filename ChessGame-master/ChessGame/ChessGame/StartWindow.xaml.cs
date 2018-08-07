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
using System.Windows.Shapes;

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnChess_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow("Chess");
            m.Show();
            this.Hide();
        }

        private void btn960_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow("960");
            m.Show();
            this.Hide();
        }
    }
}
