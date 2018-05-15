
using System.Windows;
using AgerGame.ViewModel;

namespace AgerGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameMenuViewModel gmVM;
        GamePlayViewModel gpVM;
        public MainWindow()
        {
            InitializeComponent();
            gpVM = new GamePlayViewModel();
            gmVM = new GameMenuViewModel(gpVM);
            grMain.Children.Add(gmVM.gm);
            grMain.Children.Add(gpVM.Game);
            GameWindow.KeyDown += (sender, e) =>
            {
                if (e.Key == System.Windows.Input.Key.C)
                {
                    MessageBox.Show("C");
                }
            };
        }
    }
}
