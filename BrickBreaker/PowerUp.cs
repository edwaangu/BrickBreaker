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
        public int x, y, Speed, size;

        public PowerUp(int _x, int _y, int _Speed, int _Size)
        {
            x = _x;
            y = _y;
            Speed = _Speed;
            size = _Size;
        }
        public void Drop()
        {
            y += Speed;
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
