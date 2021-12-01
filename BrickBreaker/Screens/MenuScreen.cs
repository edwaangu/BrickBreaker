﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace BrickBreaker
{
    public partial class MenuScreen : UserControl
    {

        Image playButtonImage = Properties.Resources.PLAY;
        Image scoresButtonImage = Properties.Resources.HIGHSCORES;
        Image exitButtonImage = Properties.Resources.EXIT;

        public MenuScreen()
        {
            InitializeComponent();
        }
        private void MenuScreen_Load(object sender, EventArgs e)
        {
            //SoundPlayer daPlayer = new SoundPlayer("Resources/levitating.mp3");
            //daPlayer.Play();
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void updateButtonTick_Tick(object sender, EventArgs e)
        {
            if (this.ActiveControl == playButton)
            {
                playButton.Location = new Point(154, 231);
            }
            else
            {
                playButton.Location = new Point(154, 261);
            }
            if (this.ActiveControl == leaderboardButton)
            {
                leaderboardButton.Location = new Point(355, 231);
            }
            else
            {
                leaderboardButton.Location = new Point(355, 261);
            }
            if (this.ActiveControl == exitButton)
            {
                exitButton.Location = new Point(560, 231);
            }
            else
            {
                exitButton.Location = new Point(560, 261);
            }
            this.Refresh();
        }

        private void leaderboardButton_Click(object sender, EventArgs e)
        {
            ScoreScreen scs = new ScoreScreen();
            Form form = this.FindForm();

            form.Controls.Add(scs);
            form.Controls.Remove(this);

            scs.Location = new Point((form.Width - scs.Width) / 2, (form.Height - scs.Height) / 2);
        }
    }
}
