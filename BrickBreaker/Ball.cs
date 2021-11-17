using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BrickBreaker
{
    public class Ball
    {
        // Basic variables
        public float x, y, xSpeed, ySpeed, size;
        public Image image;
        public Color colour;

        // Collision related variables
        public string currentBlockCol = "none";
        Rectangle futureRectCol;

        public static Random rand = new Random();

        public Ball(float _x, float _y, float _xSpeed, float _ySpeed, int _ballSize, Image _image)
        {
            // Set variables
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;
            image = _image;
        }

        public void Move()
        {
            // Update position
            x = x + xSpeed;
            y = y + ySpeed;

            // Update position if there was a future collision last frame
            if (currentBlockCol != "none")
            {
                if (currentBlockCol == "fromLeft")
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = Convert.ToInt16(futureRectCol.X - size - 2);
                }
                if (currentBlockCol == "fromRight")
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = Convert.ToInt16(futureRectCol.X + futureRectCol.Width + 2);
                }
                if (currentBlockCol == "fromUp")
                {
                    ySpeed = -Math.Abs(ySpeed);
                    y = Convert.ToInt16(futureRectCol.Y - size - 2);
                }
                if (currentBlockCol == "fromDown")
                {
                    ySpeed = Math.Abs(ySpeed);
                    y = Convert.ToInt16(futureRectCol.Y + futureRectCol.Height + 2);
                }
                currentBlockCol = "none";
            }
        }

        public string Collision(Rectangle r, Rectangle b)
        {
            // Check what side the collision is on
            if(r.IntersectsWith(new Rectangle(b.X, b.Y + b.Height - 5, b.Width, 5)))
            {
                return "fromDown";
            }
            else if (r.IntersectsWith(new Rectangle(b.X, b.Y, b.Width, 5)))
            {
                return "fromUp";
            }
            else if (r.IntersectsWith(new Rectangle(b.X, b.Y, 5, b.Height)))
            {
                return "fromLeft";
            }
            else if (r.IntersectsWith(new Rectangle(b.X + b.Width - 5, b.Y, 5, b.Height)))
            {
                return "fromRight";
            }
            return "none";
        }

        public bool BlockCollision(Block b)
        {
            // Get rectanglular collision objects
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            Rectangle futureBallRec = new Rectangle(Convert.ToInt16(x + xSpeed), Convert.ToInt16(y + ySpeed), Convert.ToInt16(size), Convert.ToInt16(size));

            // Check if this block will have a collision with the player next frame
            if(Collision(futureBallRec, blockRec) != "none")
            {
                currentBlockCol = Collision(futureBallRec, blockRec);
                futureRectCol = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            }

            return futureBallRec.IntersectsWith(blockRec);
        }

        public void PaddleCollision(Paddle p)
        {
            // Check for paddle collisions with boxes
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
                        xSpeed += (x - p.x - p.width / 2) / 15;
                    }
                    else
                    {
                        y = Convert.ToInt16(p.y + p.height);
                        ySpeed = Math.Abs(ySpeed);
                        xSpeed += (x - p.x - p.width / 2) / 15;
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
            // Check if the ball is below the screen
            Boolean didCollide = false;

            if (y >= UC.Height)
            {
                didCollide = true;
            }

            return didCollide;
        }

        public void PlaySound()
        {

        }

    }
}
