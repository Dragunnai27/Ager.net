using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgerGame.Views;

namespace AgerGame.ViewModel
{
    public class GamePauseViewModel
    {
        public GamePause gp;
        public GamePauseViewModel()
        {
            gp = new GamePause
            {
                Visibility = Visibility.Hidden
            };
        }
    }
}
