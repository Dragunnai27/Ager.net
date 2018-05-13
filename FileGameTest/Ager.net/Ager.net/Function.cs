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
    // class chức năng 
    class Function
    {
        // hàm kiểm tra va chạm
        static public bool Collision(Rect r1, Rect r2)
        {
            double r1_Left = r1.Left;
            double r1_Right = r1.Right;
            double r1_Top = r1.Top;
            double r1_Bottom = r1.Bottom;

            double r2_Left = r2.Left;
            double r2_Right = r2.Right;
            double r2_Top = r2.Top;
            double r2_Bottom = r2.Bottom;

            if (r1_Right > r2_Left &&
                r1_Left < r2_Right &&
                r1_Bottom > r2_Top &&
                r1_Top < r2_Bottom) return true;
            else return false;
        }
        // tạo Rect check va chạm
        static public Rect CreateRect(double x, double y, double m, double n)
        {
            Rect Rect = new Rect(x + (m / 6), y + (n / 6), Math.Sqrt(2) * m / 2, Math.Sqrt(2) * n / 2);
            return Rect;
        }
    }
}
