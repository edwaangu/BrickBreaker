
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Xml;
using System.IO;

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
            RectangleF blockRec = new RectangleF(b.x, b.y, b.width, b.height);
            RectangleF leftBlockRec = new RectangleF(b.x - 1, b.y - 1, b.width / 3 + 2, b.height + 2);
            RectangleF rightBlockRec = new RectangleF(b.x + (b.width * (0.66f)) - 1, b.y - 1, b.width / 3f + 2, b.height + 2);
            RectangleF upBlockRec = new RectangleF(b.x - 1, b.y - 1, b.width + 2, b.height / 3f + 2);
            RectangleF downBlockRec = new RectangleF(b.x - 1, b.y + (b.height * (0.66f)) - 1, b.width + 2, b.height / 3f + 2);
            RectangleF ballRec = new RectangleF(x, y, size, size);
            RectangleF leftBallRec = new RectangleF( (x) - 1,  (y) - 1,  (size /  3f) + 2,  (size) + 2);
            RectangleF rightBallRec = new RectangleF( (x + (size * 0.66f)) - 1,  (y) - 1,  (size / 3f) + 2,  (size) + 2);
            RectangleF upBallRec = new RectangleF( (x) - 1,  (y) - 1,  (size) + 2,  (size / 3f) + 2);
            RectangleF downBallRec = new RectangleF( (x) - 1,  (y + size * (0.66f)) - 1,  (size) + 2,  (size / 3f) + 2);

            // COLLISIONS
            if (ballRec.IntersectsWith(blockRec))
            {
                // [RIGHT][DOWN] collides with UP LEFT
                if(rightBallRec.IntersectsWith(upBlockRec) && downBallRec.IntersectsWith(upBlockRec) && rightBallRec.IntersectsWith(leftBlockRec) && downBallRec.IntersectsWith(leftBlockRec))
                {
                    ySpeed = -Math.Abs(ySpeed);
                    y = b.y - size;
                    xSpeed = -Math.Abs(xSpeed);
                    x = b.x - size;
                }

                // LEFT RIGHT [DOWN] collides with UP
                if(leftBallRec.IntersectsWith(upBlockRec) && rightBallRec.IntersectsWith(upBlockRec) && downBallRec.IntersectsWith(upBlockRec))
                {
                    ySpeed = -Math.Abs(ySpeed);
                    y = b.y - size;
                }

                // [LEFT][DOWN] collides with UP RIGHT
                if (leftBallRec.IntersectsWith(upBlockRec) && downBallRec.IntersectsWith(upBlockRec) && leftBallRec.IntersectsWith(rightBlockRec) && downBallRec.IntersectsWith(rightBlockRec))
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = b.x + b.width + 2;
                    ySpeed = -Math.Abs(ySpeed);
                    y = b.y - size;
                }

                // UP DOWN [LEFT] collides with RIGHT
                if (leftBallRec.IntersectsWith(rightBlockRec) && upBallRec.IntersectsWith(rightBlockRec) && downBallRec.IntersectsWith(rightBlockRec))
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = b.x + b.width + 2;

                }

                // [LEFT][UP] collides with DOWN RIGHT
                if (leftBallRec.IntersectsWith(downBlockRec) && upBallRec.IntersectsWith(downBlockRec) && leftBallRec.IntersectsWith(rightBlockRec) && upBallRec.IntersectsWith(rightBlockRec))
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = b.x + b.width + 2;
                    ySpeed = Math.Abs(ySpeed);
                    y = b.y + b.height;

                }

                // LEFT RIGHT [UP] collides with DOWN
                if (leftBallRec.IntersectsWith(downBlockRec) && upBallRec.IntersectsWith(downBlockRec) && rightBallRec.IntersectsWith(downBlockRec))
                {

                    ySpeed = Math.Abs(ySpeed);
                    y = b.y + b.height;
                }

                // [RIGHT][UP] collides with DOWN LEFT
                if (rightBallRec.IntersectsWith(downBlockRec) && upBallRec.IntersectsWith(downBlockRec) && rightBallRec.IntersectsWith(leftBlockRec) && upBallRec.IntersectsWith(leftBlockRec))
                {

                    ySpeed = Math.Abs(ySpeed);
                    y = b.y + b.height;
                    xSpeed = -Math.Abs(xSpeed);
                    x = b.x - size;
                }

                // UP DOWN [RIGHT] collides with LEFT
                if (downBallRec.IntersectsWith(leftBlockRec) && upBallRec.IntersectsWith(leftBlockRec) && rightBallRec.IntersectsWith(leftBlockRec))
                {

                    xSpeed = -Math.Abs(xSpeed);
                    x = b.x - size;
                }




                /*
                // TOP
                if (x + size > b.x && x < b.x + b.width && y + size > b.y && y + size * (3 / 4) < b.y + b.height / 4 && ySpeed > 0)
                {
                    ySpeed = -Math.Abs(ySpeed);
                    y = b.y - size;
                }

                // BOTTOM
                if (x + size > b.x && x < b.x + b.width && y + size / 4 > b.y + b.height * (3 / 4) && y < b.y + b.height && ySpeed < 0)
                {
                    ySpeed = Math.Abs(ySpeed);
                    y = b.y + b.height;
                }

                // RIGHT SIDE
                if (y + size > b.y && y < b.y + b.height && x + size / 4 > b.x + b.width * (3 / 4) && x < b.x + b.width && xSpeed < 0)
                {
                    xSpeed = Math.Abs(xSpeed);
                    x = b.x + b.width + 2;
                }

                // LEFT SIDE
                if (y + size > b.y && y < b.y + b.height && x + size > b.x && x + size * (3 / 4) < b.x + b.width / 4 && xSpeed > 0)
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = b.x - size - 2;
                }*/

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
                    x = p.x + p.width + 2;
                }

                // LEFT SIDE
                if (y + size > p.y && y < p.y + p.height && x + size > p.x && x + size * (3 / 4) < p.x + p.width / 4)
                {
                    xSpeed = -Math.Abs(xSpeed);
                    x = p.x - size - 2;
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
