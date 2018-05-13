using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ager.net
{
    // class nhân vật player
    class Player
    {
        // chiều dài và cao
        double playerWidthAndHeight;
        // tọa độ 
        double pX;
        double pY;
        // tốc độ
        double pSpeed;

        public double PlayerWidthAndHeight { get => playerWidthAndHeight; set => playerWidthAndHeight = value; }
        public double PX { get => pX; set => pX = value; }
        public double PY { get => pY; set => pY = value; }
        public double PSpeed { get => pSpeed; set => pSpeed = value; }

        public Player()
        {
            playerWidthAndHeight = 15;
            pSpeed = 2.2;
        }
    }
}
