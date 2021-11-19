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
        Rectangle futureRectCol;
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

        public void Move()
        {
            // Update position
            x = x + xSpeed;
            y = y + ySpeed;
        }

        public bool BlockCollision(Block b)
        {
            // Get rectanglular collision objects
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            Rectangle futureBallRec = new Rectangle(Convert.ToInt16(x + xSpeed), Convert.ToInt16(y + ySpeed), Convert.ToInt16(size), Convert.ToInt16(size));

            if (ballRec.IntersectsWith(blockRec))
            {
                double dir = Math.Atan2(ySpeed, xSpeed);

                for (int i = 0; i < 4; i++)
                {
                    PointF basePoint  = new PointF(x, y);
                    PointF subtractPoint = new PointF(0, 0);
                    if (i == 1)
                    {
                        basePoint = new PointF(x + size, y);
                        subtractPoint = new PointF(size, 0);
                    }
                    if (i == 2)
                    {
                        basePoint = new PointF(x + size, y + size);
                        subtractPoint = new PointF(size, size);
                    }
                    if (i == 3)
                    {
                        basePoint = new PointF(x, y + size);
                        subtractPoint = new PointF(0, size);
                    }
                    PointF currentPoint = new PointF(basePoint.X + Convert.ToSingle(Math.Cos(dir) * size * 3 / 4), basePoint.Y + Convert.ToSingle(Math.Sin(dir) * size * 3 / 4));
                    PointF point5Ago = new PointF(basePoint.X - (xSpeed * 5), basePoint.Y - (ySpeed * 5));



                    // Hits left
                    if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X, blockRec.Y, blockRec.X, blockRec.Y + blockRec.Height))
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        x = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X, blockRec.Y, blockRec.X, blockRec.Y + blockRec.Height).X - subtractPoint.X - 2;
                        collisionTotals++;
                        break;
                    }

                    // Hits right
                    if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X + blockRec.Width, blockRec.Y, blockRec.X + blockRec.Width, blockRec.Y + blockRec.Height))
                    {
                        xSpeed = Math.Abs(xSpeed);
                        x = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X + blockRec.Width, blockRec.Y, blockRec.X + blockRec.Width, blockRec.Y + blockRec.Height).X + subtractPoint.X + 2;
                        collisionTotals++;
                        break;
                    }

                    // Hits top
                    if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X, blockRec.Y, blockRec.X + blockRec.Width, blockRec.Y))
                    {
                        ySpeed = -Math.Abs(ySpeed);
                        y = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X, blockRec.Y, blockRec.X + blockRec.Width, blockRec.Y).Y - subtractPoint.Y - 2;
                        collisionTotals++;
                        break;
                    }
                    // Hits bottom
                    if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X, blockRec.Y + blockRec.Height, blockRec.X + blockRec.Width, blockRec.Y + blockRec.Height))
                    {
                        ySpeed = Math.Abs(ySpeed);
                        y = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, blockRec.X, blockRec.Y + blockRec.Height, blockRec.X + blockRec.Width, blockRec.Y + blockRec.Height).Y + subtractPoint.Y + 2;
                        collisionTotals++;
                        break;
                    }
                }
                shouldChangeSpeedOnPaddle = true;
                return true;
            }
            return false;

            /*
            // Check if this block will have a collision with the player next frame
            if(Collision(futureBallRec, blockRec) != "none")
            {
                currentBlockCol = Collision(futureBallRec, blockRec);
                futureRectCol = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            }

            return futureBallRec.IntersectsWith(blockRec);*/
        }

        public bool checkIfLineCollision(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            // Zero length lines
            if((x1 == x2 && y1 == y2) || (x3 == x4 && y3 == y4))
            {
                return false;
            }

            // No parallel lines
            float D = (x1 - x2)*(y3 - y4) - (y1 - y2)*(x3 - x4);
            if(D == 0)
            {
                return false;
            }


            float ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            float ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

            if(ua < 0 || ua > 1 || ub < 0 || ua > 1)
            {
                return false;
            }

            return true;
        }

        public PointF findPointFromLineCollision(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            float ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            float ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

            float x = x1 + ua * (x2 - x1);
            float y = y1 + ua * (y2 - y1);

            return new PointF(x, y);
        }

        public void PaddleCollision(Paddle p)
        {
            // Check for paddle collisions with boxes
            Rectangle ballRec = new Rectangle(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(size), Convert.ToInt16(size));
            px = x - xSpeed;
            py = y - ySpeed;
            Rectangle paddleRec = new Rectangle(Convert.ToInt16(p.x), Convert.ToInt16(p.y), Convert.ToInt16(p.width), Convert.ToInt16(p.height));

            

            if (ballRec.IntersectsWith(paddleRec))
            {
                double dir = Math.Atan2(ySpeed, xSpeed);

                for (int i = 0; i < 4; i++)
                {
                    PointF basePoint = new PointF(x, y);
                    PointF subtractPoint = new PointF(0, 0);
                    if (i == 1)
                    {
                        basePoint = new PointF(x + size, y);
                        subtractPoint = new PointF(size, 0);
                    }
                    if (i == 2)
                    {
                        basePoint = new PointF(x + size, y + size);
                        subtractPoint = new PointF(size, size);
                    }
                    if (i == 3)
                    {
                        basePoint = new PointF(x, y + size);
                        subtractPoint = new PointF(0, size);
                    }
                    PointF currentPoint = new PointF(basePoint.X + Convert.ToSingle(Math.Cos(dir) * size * 3 / 4), basePoint.Y + Convert.ToSingle(Math.Sin(dir) * size * 3 / 4));
                    PointF point5Ago = new PointF(basePoint.X - (xSpeed * 5), basePoint.Y - (ySpeed * 5));

                    // Hits top
                    if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X, paddleRec.Y, paddleRec.X + paddleRec.Width, paddleRec.Y))
                    {
                        ySpeed = -Math.Abs(ySpeed);
                        y = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X, paddleRec.Y, paddleRec.X + paddleRec.Width, paddleRec.Y).Y - subtractPoint.Y - 5;
                        collisionTotals++;
                        if (shouldChangeSpeedOnPaddle)
                        {
                            xSpeed += (x - p.x - p.width / 2) / 30;
                            shouldChangeSpeedOnPaddle = false;
                        }
                        break;
                    }
                    // Hits bottom
                    else if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X, paddleRec.Y + paddleRec.Height, paddleRec.X + paddleRec.Width, paddleRec.Y + paddleRec.Height))
                    {
                        ySpeed = Math.Abs(ySpeed);
                        y = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X, paddleRec.Y + paddleRec.Height, paddleRec.X + paddleRec.Width, paddleRec.Y + paddleRec.Height).Y + subtractPoint.Y + 5;
                        collisionTotals++;
                        //xSpeed += (x - p.x - p.width / 2) / 15;
                        break;
                    }

                    // Hits left
                    if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X, paddleRec.Y, paddleRec.X, paddleRec.Y + paddleRec.Height))
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        x = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X, paddleRec.Y, paddleRec.X, paddleRec.Y + paddleRec.Height).X - subtractPoint.X - 5;
                        collisionTotals++;
                        break;
                    }

                    // Hits right
                    else if (checkIfLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X + paddleRec.Width, paddleRec.Y, paddleRec.X + paddleRec.Width, paddleRec.Y + paddleRec.Height))
                    {
                        xSpeed = Math.Abs(xSpeed);
                        x = findPointFromLineCollision(currentPoint.X, currentPoint.Y, point5Ago.X, point5Ago.Y, paddleRec.X + paddleRec.Width, paddleRec.Y, paddleRec.X + paddleRec.Width, paddleRec.Y + paddleRec.Height).X - subtractPoint.X + 5;
                        collisionTotals++;
                        break;
                    }
                }

            }

            /*
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
            */
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
