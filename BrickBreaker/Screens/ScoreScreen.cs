using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BrickBreaker
{
    public partial class ScoreScreen : UserControl
    {

        public ScoreScreen()
        {
            InitializeComponent();
        }

        private void ScoreScreen_Load(object sender, EventArgs e)
        {
            Form1.highscores.Clear();

            XmlReader reader = XmlReader.Create("Resources/Scores.xml");

            for (int i = 0; i < 3; i++)
            {
                reader.ReadToFollowing("Score");
                string theName = reader.GetAttribute("name");
                int theScore = Convert.ToInt16(reader.GetAttribute("score"));
                Form1.highscores.Add(new Highscore(theScore, theName));
            }
            reader.Close();

            Form1.highscores = Form1.highscores.OrderByDescending(x => x.score).ToList();

            if(Form1.theScore < Form1.highscores[2].score || Form1.theScore == -1)
            {
                enterNameBox.Visible = false;
                enterNameBox.Enabled = false;
                enterButton.Visible = false;
                enterButton.Enabled = false;
                label1.Visible = false;
            }
            else
            {
                enterNameBox.Visible = true;
                enterNameBox.Enabled = true;
                enterButton.Visible = true;
                enterButton.Enabled = true;
                label1.Visible = true;
            }

            if(Form1.theScore == -1)
            {
                scoreLabel.Visible = false;
            }
            else
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = Form1.theScore.ToString();
            }

            firstPlaceName.Text = Form1.highscores[0].name;
            firstPlaceScore.Text = Form1.highscores[0].score.ToString();
            secondPlaceName.Text = Form1.highscores[1].name;
            secondPlaceScore.Text = Form1.highscores[1].score.ToString();
            thirdPlaceName.Text = Form1.highscores[2].name;
            thirdPlaceScore.Text = Form1.highscores[2].score.ToString();
        }

        private void leaderboardButton_Click(object sender, EventArgs e)
        {
            MenuScreen mcs = new MenuScreen();
            Form form = this.FindForm();

            form.Controls.Add(mcs);
            form.Controls.Remove(this);

            mcs.Location = new Point((form.Width - mcs.Width) / 2, (form.Height - mcs.Height) / 2);
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            if(Form1.theScore > Form1.highscores[0].score)
            {
                Form1.highscores[2].name = Form1.highscores[1].name;
                Form1.highscores[2].score = Form1.highscores[1].score;
                Form1.highscores[1].name = Form1.highscores[0].name;
                Form1.highscores[1].score = Form1.highscores[0].score;
                Form1.highscores[0].name = enterNameBox.Text;
                Form1.highscores[0].score = Form1.theScore;
            }
            else if(Form1.theScore > Form1.highscores[1].score)
            {
                Form1.highscores[2].name = Form1.highscores[1].name;
                Form1.highscores[2].score = Form1.highscores[1].score;
                Form1.highscores[1].name = enterNameBox.Text;
                Form1.highscores[1].score = Form1.theScore;
            }
            else if(Form1.theScore > Form1.highscores[2].score)
            {
                Form1.highscores[2].name = enterNameBox.Text;
                Form1.highscores[2].score = Form1.theScore;
            }
            enterNameBox.Visible = false;
            enterNameBox.Enabled = false;
            enterButton.Visible = false;
            enterButton.Enabled = false;
            Form1.theScore = -1;

            XmlWriter writer = XmlWriter.Create("Resources/Scores.xml");
            //Write the root element 

            writer.WriteStartElement("root");
            foreach (Highscore h in Form1.highscores)
            {
                //Start an element 
                writer.WriteStartElement("Score");
                writer.WriteAttributeString("name", h.name);
                writer.WriteAttributeString("score", h.score.ToString());

                // end the element 
                writer.WriteEndElement();
            }

            // end the root element 
            writer.WriteEndElement();

            //Write the XML to file and close the writer 
            writer.Close();

            firstPlaceName.Text = Form1.highscores[0].name;
            firstPlaceScore.Text = Form1.highscores[0].score.ToString();
            secondPlaceName.Text = Form1.highscores[1].name;
            secondPlaceScore.Text = Form1.highscores[1].score.ToString();
            thirdPlaceName.Text = Form1.highscores[2].name;
            thirdPlaceScore.Text = Form1.highscores[2].score.ToString();
        }

        private void updateTick_Tick(object sender, EventArgs e)
        {

            if (this.ActiveControl == backButton)
            {
                backButton.Location = new Point(700, 0);
            }
            else
            {
                backButton.Location = new Point(700, 29);
            }
            if (this.ActiveControl == enterButton)
            {
                enterButton.Location = new Point(513, 100);
            }
            else
            {
                enterButton.Location = new Point(513, 118);
            }
        }
    }
}
