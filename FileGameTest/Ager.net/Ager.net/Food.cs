using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ager.net
{
    // class đồ ăn 
    class Food
    {
        // chiều dài và cao
        double foodWidthAndHeight;
        // tọa độ 
        double fX;
        double fY;
        // thông số rect của food
        double fRectX;
        double fRectY;

        public double FoodWidthAndHeight { get => foodWidthAndHeight; set => foodWidthAndHeight = value; }
        public double FX { get => fX; set => fX = value; }
        public double FY { get => fY; set => fY = value; }
        public double FRectX { get => fRectX; set => fRectX = value; }
        public double FRectY { get => fRectY; set => fRectY = value; }
        
        public Food()
        {
            foodWidthAndHeight = 10;
        }
    }
}
