
using System.Windows;
using AgerGame.ViewModel;
using AgerGame.Views;

namespace AgerGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameMenuViewModel gameMenuVM;
        GamePauseViewModel gamePauseVM;
        GamePlayWindow gamePlayWindow;
        public MainWindow()
        {
            InitializeComponent();
            gamePlayWindow = new GamePlayWindow(Ultil.CreatePlayer());
            gameMenuVM = new GameMenuViewModel(gamePlayWindow);
            gamePauseVM = new GamePauseViewModel(gamePlayWindow);
            grMain.Children.Add(gamePauseVM.gamePause);
            grMain.Children.Add(gameMenuVM.gameMenu);
        }
    }
}
