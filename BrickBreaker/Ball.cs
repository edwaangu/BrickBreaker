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
        public float px, py;
        public Image image;
        public Color colour;

        public int collisionTotals;

        // Collision related variables
        public string currentBlockCol = "none";
        bool shouldChangeSpeedOnPaddle = false;

        public static Random rand = new Random();

        public Ball(float _x, float _y, float _xSpeed, float _ySpeed, int _ballSize, Image _image)
        {
            // Set variables
            x = _x;
            y = _y;
            px = x;
            py = y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;
            image = _image;
            collisionTotals = 0;
        }

        public void Move(float divisor)
        {
            // Update position
            x += xSpeed / divisor;
            y += ySpeed / divisor;
        }

        public bool BlockCollision(Block b)
        {
            // Get rectanglular collision objects
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));

            // COLLISIONS
            if (ballRec.IntersectsWith(blockRec))
            {
                if (x + size > b.x && x < b.x + b.width && y + size > b.y && y + size * (3 / 4) < b.y + b.height / 4)
                {
                    ySpeed = -Math.Abs(ySpeed);
                    y = b.y - size;
                }

                // BOTTOM
                if (x + size > b.x && x < b.x + b.width && y + size / 4 > b.y + b.height * (3 / 4) && y < b.y + b.height)
                {
                    ySpeed = Math.Abs(ySpeed);
                    y = b.y + b.height;
                }


                // RIGHT SIDE
                if (y + size > b.y && y < b.y + b.height && x + size / 4 > b.x + b.width * (3 / 4) && x < b.x + b.width)
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = b.x + b.width;
                }

                // LEFT SIDE
                if (y + size > b.y && y < b.y + b.height && x + size > b.x && x + size * (3 / 4) < b.x + b.width / 4)
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = b.x - size;
                }
                return true; 
            }

            return false;
        }

        public void PaddleCollision(Paddle p)
        {
            // Check for paddle collisions with boxes
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            Rectangle paddleRec = new Rectangle(Convert.ToInt16(p.x), Convert.ToInt16(p.y), Convert.ToInt16(p.width), Convert.ToInt16(p.height));


            if (ballRec.IntersectsWith(paddleRec))
            {
                // TOP
                if (x + size > p.x && x < p.x + p.width && y + size > p.y && y + size * (3 / 4) < p.y + p.height / 4)
                {
                    ySpeed = -Math.Abs(ySpeed);
                    y = p.y - size;
                    xSpeed += ((x + size / 2) - (p.x + p.width / 2)) / 30;
                }

                // BOTTOM
                if (x + size > p.x && x < p.x + p.width && y + size / 4 > p.y + p.height * (3 / 4) && y < p.y + p.height)
                {
                    ySpeed = Math.Abs(ySpeed);
                    y = p.y + p.height;
                }


                // RIGHT SIDE
                if (y + size > p.y && y < p.y + p.height && x + size / 4 > p.x + p.width * (3 / 4) && x < p.x + p.width)
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = p.x + p.width;
                }

                // LEFT SIDE
                if (y + size > p.y && y < p.y + p.height && x + size > p.x && x + size * (3 / 4) < p.x + p.width / 4)
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = p.x - size;
                }

                var breakSound = new System.Windows.Media.MediaPlayer();
                breakSound.Open(new Uri(Application.StartupPath + "/Resources/collision.wav"));
                breakSound.Play();
            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                xSpeed *= -1;
                x = 1; 
                shouldChangeSpeedOnPaddle = true;
            }

            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed *= -1;
                x = UC.Width - size - 1;
                shouldChangeSpeedOnPaddle = true;
            }

            // Collision with top wall
            if (y <= 2)
            {
                ySpeed *= -1;
                y = 3;
                shouldChangeSpeedOnPaddle = true;
            }
        }

        public bool BottomCollision(UserControl UC)
        {
            // Check if the ball is below the screen
            Boolean didCollide = false;

            if (y >= UC.Height)
            {
                didCollide = true;
                shouldChangeSpeedOnPaddle = false;
            }

            return didCollide;
        }

        public void PlaySound()
        {

        }

    }
}
