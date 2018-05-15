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
using System.Windows.Media;
using System.Windows.Controls;

namespace AgerGame.ViewModel
{
    public class GameMenuViewModel
    {
        private bool IsPlaying = false;
        public GameMenu gameMenu;
        public GamePlayWindow gamePlayWindow;
        MediaPlayer mp3 = new MediaPlayer();
        public GameMenuViewModel()
        {
            gameMenu = new GameMenu
            {
                Visibility = Visibility.Visible
            };
            mp3.Open(new Uri(@"unity.MP3", UriKind.RelativeOrAbsolute));
            gameMenu.btnQuit.Click += (sender, e) =>
            {
                App.Current.Shutdown();
            };
            gameMenu.btnPlay.Click += (sender, e) =>
            {
                gamePlayWindow = new GamePlayWindow(Ultil.CreatePlayer());
                gamePlayWindow.ShowDialog();
            };
            gameMenu.btnSetting.Click += (sender, e) =>
            {
                if (!IsPlaying)
                {
                    mp3.Play();
                    IsPlaying = true;
                    return;
                }
                else
                {
                    mp3.Pause();
                    IsPlaying = false;
                }
            };
        }
    }

}

