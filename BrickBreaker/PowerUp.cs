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
        public int x, y, size = 40, type;
        Random randGen = new Random(); // 1 = paddle increase, 2 = speed increase, 3 = instabreak, 4 = gun, 5 = launch
        public float ySpeed = 1;

        public PowerUp(int _x, int _y)
        {
            x = _x;
            y = _y;
            type = randGen.Next(1, 5);
        }
        public void Drop()
        {
            y += Convert.ToInt16(ySpeed);
            ySpeed += 0.1f;
        }

        public bool PaddleCollision(Paddle p)
        {
            Boolean didCollide = false;

            Rectangle powerUpRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(Convert.ToInt16(p.x), Convert.ToInt16(p.y), Convert.ToInt16(p.width), Convert.ToInt16(p.height));

            if (powerUpRec.IntersectsWith(paddleRec))
            {
                didCollide = true;
            }
            return didCollide;
        }
        public bool BottomCollision(GameScreen UC)
        {
            Boolean didCollide = false;

            if (y >= UC.Height)
            {
                didCollide = true;
            }

            return didCollide;
        }
    }
}
