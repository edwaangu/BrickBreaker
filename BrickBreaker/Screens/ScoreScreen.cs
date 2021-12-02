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
            firstPlaceName.Text = Form1.names[0];
            firstPlaceScore.Text = Form1.scores[0];
            secondPlaceName.Text = Form1.names[1];
            secondPlaceScore.Text = Form1.scores[1];
            thirdPlaceName.Text = Form1.names[2];
            thirdPlaceScore.Text = Form1.scores[2];

        }
    }
}
