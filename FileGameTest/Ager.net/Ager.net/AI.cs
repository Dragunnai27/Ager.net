using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ager.net
{
    class AI
    {
        // chiều dài và cao
        double aiWidthAndHeight;
        // tọa độ 
        double aX;
        double aY;
        // tốc độ
        double aSpeed;
        // còn sông
        bool alive;

        public double AiWidthAndHeight { get => aiWidthAndHeight; set => aiWidthAndHeight = value; }
        public double AX { get => aX; set => aX = value; }
        public double AY { get => aY; set => aY = value; }
        public double ASpeed { get => aSpeed; set => aSpeed = value; }
        public bool Alive { get => alive; set => alive = value; }

        public AI()
        {
            aiWidthAndHeight = 20;
            //aSpeed = 1.95;
            aSpeed = 2;
            Alive = true;
        }
    }
}
