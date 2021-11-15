using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickBreaker
{
    class PowerUp
    {
        public int x, y, xSpeed, ySpeed, size;

        public PowerUp(int _x, int _y, int _xSpeed, int _ySpeed, int _Size)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _Size;
        }
        public void Spawn()
        {

        }
    }
}
