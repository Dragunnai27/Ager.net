using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgerGame.Views;
using static System.Net.Mime.MediaTypeNames;

namespace AgerGame.ViewModel
{
    public class GameMenuViewModel
    {
        public GameMenu gm;
        public GamePlayViewModel gamePlayVM;
        public GamePauseViewModel gamePauseVM;
        public GameMenuViewModel(GamePlayViewModel gpVM)
        {
            gm = new GameMenu
            {
                Visibility = Visibility.Visible
            };
            gamePlayVM = gpVM;
            gm.btnQuit.Click += (sender, e) =>
            {
                App.Current.Shutdown();
            };
            gm.btnPlay.Click += (sender, e) =>
            {
                gm.Visibility = Visibility.Hidden;
                gpVM.Game.Visibility = Visibility.Visible;
                gpVM.Game.gameTime.Start();
            };
        }
    }

}
