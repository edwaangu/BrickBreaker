using System.Drawing;

namespace BrickBreaker
{
    public class Paddle
    {
        public float x, y, width, height, speed;
        public float xs = 0;
        public Color colour;

        public Paddle(float _x, float _y, float _width, float _height, float _speed, Color _colour)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            colour = _colour;
        }

        public void Move(string direction)
        {
            if (direction == "left")
            {
                xs -= speed;
            }
            if (direction == "right")
            {
                xs += speed;
            }
        }

        public void updatePosition(int widthScreen)
        {
            xs *= 0.8f;
            x += xs;

            if(x < 0)
            {
                x = 0;
            }
            if(x + width > widthScreen)
            {
                x = widthScreen - width;
            }
        }
    }
}
