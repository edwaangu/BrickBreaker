using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrickBreaker
{
    public class Ball
    {
        public int x, y, xSpeed, ySpeed, size;
        public Color colour;

        public static Random rand = new Random();

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed, int _ballSize)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;

        }

        public void Move()
        {
            x = x + xSpeed;
            y = y + ySpeed;
        }

        public bool BlockCollision(Block b)
        {
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }

            return blockRec.IntersectsWith(ballRec);
        }

        public void PaddleCollision(Paddle p)
        {
            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(Convert.ToInt16(p.x), Convert.ToInt16(p.y), Convert.ToInt16(p.width), Convert.ToInt16(p.height));

            if (ballRec.IntersectsWith(paddleRec))
            {
                if(x > p.x - size - xSpeed && x < p.x - xSpeed + (p.width / 2 ) && y > p.y - size + 2 && y < p.y - 2)
                {
                    x = Convert.ToInt16(p.x - size - 2);
                }
                else if (x < p.x + p.width - xSpeed && x > p.x - size - xSpeed + p.width - (p.width / 4) && y > p.y - size + 2 && y < p.y - 2)
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = Convert.ToInt16(p.x + p.width + 2);
                }
                else if (x > p.x - size && x < p.x + p.width)
                {
                    if (y > p.y - size && y < p.y - size + (p.height / 2))
                    {
                        y = Convert.ToInt16(p.y - size);
                        ySpeed = -Math.Abs(ySpeed);
                    }
                    else
                    {
                        y = Convert.ToInt16(p.y - size + p.height);
                        ySpeed = Math.Abs(ySpeed);
                    }
                }
            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                xSpeed *= -1;
                x = 1;
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed *= -1;
                x = UC.Width - size - 1;
            }
            // Collision with top wall
            if (y <= 2)
            {
                ySpeed *= -1;
                y = 3;
            }
        }

        public bool BottomCollision(UserControl UC)
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
