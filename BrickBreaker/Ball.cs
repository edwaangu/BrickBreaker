using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrickBreaker
{
    public class Ball
    {
        public float x, y, xSpeed, ySpeed, size;
        public Image image;
        public Color colour;

        public static Random rand = new Random();

        public Ball(float _x, float _y, float _xSpeed, float _ySpeed, int _ballSize, Image _image)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;
            image = _image;
        }

        public void Move()
        {
            x = x + xSpeed;
            y = y + ySpeed;
        }

        public bool BlockCollision(Block b)
        {
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));

            if (ballRec.IntersectsWith(blockRec))
            {
                if (x > b.x - size - xSpeed && x < b.x + (b.width / 4) - xSpeed && y > b.y - size + 5 && y < b.y + b.height - 5)
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = Convert.ToInt16(b.x - size - 2);
                }
                else if (x < b.x + b.width - xSpeed && x > b.x - size - xSpeed + b.width - (b.width / 4) && y > b.y - size + 5 && y < b.y + b.height - 5)
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = Convert.ToInt16(b.x + b.width + 2);
                }
                else if (x > b.x - size + 5 && x < b.x + b.width - 5)
                {
                    if (y > b.y - size && y < b.y - size + (b.height / 2))
                    {
                        y = Convert.ToInt16(b.y - size);
                        ySpeed = -Math.Abs(ySpeed);
                    }
                    else
                    {
                        y = Convert.ToInt16(b.y + b.height);
                        ySpeed = Math.Abs(ySpeed);
                    }
                }
            }

            return blockRec.IntersectsWith(ballRec);
        }

        public void PaddleCollision(Paddle p)
        {
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            Rectangle paddleRec = new Rectangle(Convert.ToInt16(p.x), Convert.ToInt16(p.y), Convert.ToInt16(p.width), Convert.ToInt16(p.height));

            if (ballRec.IntersectsWith(paddleRec))
            {
                if(x > p.x - size - xSpeed && x < p.x + (p.width / 4) - xSpeed && y > p.y - size + 5 && y < p.y + p.height - 5)
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = Convert.ToInt16(p.x - size - 2);
                }
                else if (x < p.x + p.width - xSpeed && x > p.x - size - xSpeed + p.width - (p.width / 4) && y > p.y - size + 5 && y < p.y + p.height - 5)
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = Convert.ToInt16(p.x + p.width + 2);
                }
                else if (x > p.x - size + 5 && x < p.x + p.width - 5)
                {
                    if (y > p.y - size && y < p.y - size + (p.height / 2))
                    {
                        y = Convert.ToInt16(p.y - size);
                        ySpeed = -Math.Abs(ySpeed);
                        xSpeed += p.xs / 5;
                    }
                    else
                    {
                        y = Convert.ToInt16(p.y + p.height);
                        ySpeed = Math.Abs(ySpeed);
                        xSpeed += p.xs / 5;
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
