using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgerGame.Views;
using static System.Net.Mime.MediaTypeNames;
using AgerGame.ViewModel;

namespace AgerGame.ViewModel
{
    public class GameMenuViewModel
    {
        public GameMenu gameMenu;
        public GamePlayWindow gamePlayWindow;
        public GameMenuViewModel(GamePlayWindow gpwd)
        {
            gameMenu = new GameMenu
            {
                Visibility = Visibility.Visible
            };            
            gameMenu.btnQuit.Click += (sender, e) =>
            {
                App.Current.Shutdown();
            };
            gameMenu.btnPlay.Click += (sender, e) =>
            {
                //gamePlayWindow = gpwd;
                gamePlayWindow = new GamePlayWindow(Ultil.CreatePlayer());
                gamePlayWindow.ShowDialog();
            };
        }
    }

}

