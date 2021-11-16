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
        public int x, y, size = 20, type;
        Random randGen = new Random(); // 1 = paddle increase, 2 = speed increase, 3 = instabreak, 4 = gun, 5 = launch
        public float ySpeed = 5;

        public PowerUp(int _x, int _y)
        {
            x = _x;
            y = _y;
            type = randGen.Next(1, 6);
        }
        public void Spawn()
        {
            y += Convert.ToInt16(ySpeed);
            ySpeed += 0.1f;
        }
    }
}
