/*  Created by: Team 2 (Ted, Matt, Bilal, Dylan, and Colbey)
 *  Project: Brick Breaker
 *  Date Started: 11/3/2021 - __/__/2021
 */ 
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

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown;

        // Game values
        int lives;
        int level;
        int powerupCounter;
        float startDirection = 180;
        bool directionLeftKey = false;
        bool directionRightKey = false;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;
        List<PowerUp> powerups = new List<PowerUp>();

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);

        // Random
        Random randGen = new Random();

        // Should ball move
        bool ballMoving = false;
        Image brickImage = Properties.Resources.Brick;

        #endregion

        int playerScore;



        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            //SoundPlayer daPlayer = new SoundPlayer(Properties.Resources.dababy2);
            //daPlayer.Play();
        }

        void SetupLevel(int _level)
        {
            blocks.Clear();

            XmlReader reader = XmlReader.Create("Resources/XML.xml");

            int thelevel = -1;
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element && reader.Name == "Level")
                {
                    thelevel++;
                }
                if (thelevel == _level)
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        int __x = Convert.ToInt16(reader.ReadString());
                        reader.ReadToFollowing("y");
                        int __y = Convert.ToInt16(reader.ReadString());
                        reader.ReadToFollowing("type");
                        int __type = Convert.ToInt16(reader.ReadString());

                        blocks.Add(new Block(__x, __y, __type, Color.White));
                    }
                }
            }

            reader.Close();
        }

        void NewLevel()
        {
            level++;
            SetupLevel(level);
            if(blocks.Count == 0)
            {
                gameTimer.Enabled = false;
                OnEnd();
                return;
            }
            // Moves the ball back to origin
            ball.x = ((Convert.ToInt32(paddle.x) - (ball.size / 2)) + (Convert.ToInt32(paddle.width) / 2));
            ball.y = (this.Height - Convert.ToInt32(paddle.height)) - 80;
            float dir = Convert.ToSingle(randGen.Next(0, 360));
            ball.xSpeed = Convert.ToSingle(Math.Sin(dir / (180 / 3.14)) * 4); // 6
            ball.ySpeed = Convert.ToSingle(Math.Cos(dir / (180 / 3.14)) * 4); // 6
            ballMoving = false;
        }

        public void OnStart()
        {
            // set starting score to 0
            playerScore = 0;
              
            
            //set life counter
            lives = 3;
            powerupCounter = 0;

            // MAKE SURE THE BALL FREEZES IN PLACE AND DIES
            ballMoving = false;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 3; // 5
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            float ballX = this.Width / 2 - 10;
            float ballY = this.Height - Convert.ToInt32(paddle.height) - 80;

            // Creates a new ball
            startDirection = 0; 
            int ballSize = 20;
            ball = new Ball(ballX, ballY, 0, 0, ballSize, Properties.Resources.whiteBrick2);

            // Setup level
            level = 3;
            SetupLevel(level);

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    if (ballMoving == false)
                    {
                        ball.xSpeed = Convert.ToSingle(Math.Sin(startDirection / (180 / 3.14)) * 6);
                        ball.ySpeed = Convert.ToSingle(Math.Cos(startDirection / (180 / 3.14)) * 6);
                    }
                    ballMoving = true;
                    break;
                case Keys.A:
                    directionLeftKey = true;
                    break;
                case Keys.D:
                    directionRightKey = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.A:
                    directionLeftKey = false;
                    break;
                case Keys.D:
                    directionRightKey = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }

            paddle.updatePosition(this.Width);

            // Move ball
            if (ballMoving)
            {
                ball.Move();
            }
            else
            {
                ball.x = Convert.ToInt16(paddle.x + paddle.width / 2 - ball.size / 2);

                // Change the direction at start as necessary
                if (directionLeftKey)
                {
                    startDirection += 2;
                }
                if (directionRightKey)
                {
                    startDirection -= 2;
                }
                if(startDirection < 90)
                {
                    startDirection = 90;
                }
                else if(startDirection > 270)
                {
                    startDirection = 270;
                }
                ball.currentBlockCol = "none";
            }

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;
               
                playerScore--;
                scoreLabel.Text = $"Your Score:{playerScore}";

                // Moves the ball back to origin
                ball.x = ((Convert.ToInt32(paddle.x) - (ball.size / 2)) + (Convert.ToInt32(paddle.width) / 2));
                ball.y = (this.Height - Convert.ToInt32(paddle.height)) - 80;
                ball.xSpeed = Convert.ToSingle(Math.Sin(startDirection / (180 / 3.14)) * 6);
                ball.ySpeed = Convert.ToSingle(Math.Cos(startDirection / (180 / 3.14)) * 6);
                ballMoving = false;

                if (lives == 0)
                {
                    gameTimer.Enabled = false;
                    OnEnd();
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    playerScore++;
                    scoreLabel.Text = $"Your Score:{playerScore}";
                    b.hp--;
                    if (b.hp <= 0)
                    {
                        blocks.Remove(b);

                        PowerUpMethod();

                        if (blocks.Count == 0)
                        {
                            NewLevel();
                        }
                    }

                    break;
                }
            }


            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();
            
            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

       

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                e.Graphics.DrawImage(brickImage, b.x, b.y, b.width, b.height);
                e.Graphics.DrawString(b.hp.ToString(), DefaultFont, new SolidBrush(Color.White), b.x + b.width / 2, b.y + b.height / 2);
            }

            // Draws ball
            e.Graphics.FillRectangle(new SolidBrush(Color.White), ball.x, ball.y, ball.size, ball.size);

            if (!ballMoving)
            {
                for (int i = 0; i < 5; i++) {
                    float xS = Convert.ToSingle(Math.Sin(startDirection / (180 / 3.14)) * 4);
                    float yS = Convert.ToSingle(Math.Cos(startDirection / (180 / 3.14)) * 4);
                    e.Graphics.FillEllipse(new SolidBrush(Color.Gray), ball.x + ((10 - i * 2) / 2) + xS * ((i + 1) * 5), ball.y + ((10 - i * 2) / 2) + yS * ((i + 1) * 5), 10 - i * 2, 10 - i * 2);
                }
            }
        }

        public void PowerUpMethod()
        {
            powerupCounter++;

            if (powerupCounter == 5)
            {               
                Random rand = new Random();
                int powerUp = rand.Next(1, 6);

                if (powerUp == 1)
                {
                    InstaBreak();
                    powerupCounter = 0;
                }
                if (powerUp == 2)
                {
                    SpeedIncrease();
                    powerupCounter = 0;
                }
                if (powerUp == 3)
                {
                    IncreasePaddleSize();
                    powerupCounter = 0;
                }
                if (powerUp == 4)
                {
                    Gun();
                    powerupCounter = 0;
                }
                else
                {
                    DaBabyLaunch();
                    powerupCounter = 0;
                }
            }
        }

        public void InstaBreak()
        {

        }
        public void SpeedIncrease()
        {
            ball.xSpeed *= 1.5f;
            ball.ySpeed *= 1.5f;
        }
        public void IncreasePaddleSize()
        {

        }
        public void Gun()
        {

        }
        public void DaBabyLaunch()
        {
          
        }
    }
}

