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


namespace Ager.net
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //nút thoát
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //nút play
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            GamePlay gamePlay = new GamePlay();
            gamePlay.ShowDialog();
        }
        //nút setting
        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
