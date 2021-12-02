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
    public partial class Form1 : Form
    {
        public static string[] scores = new string[3];
        public static string[] names = new string[3];
        
        XmlReader reader = XmlReader.Create("Resources/XML.xml");


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start the program centred on the Menu Screen
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);

            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);

            reader.ReadToFollowing("Score1");
            names[0] = reader.GetAttribute("name");
            scores [0] = reader.GetAttribute("score");

            reader.ReadToFollowing("Score2");
            names[1] = reader.GetAttribute("name");
            scores[1] = reader.GetAttribute("score");
            
            reader.ReadToFollowing("Score3");
            names[2] = reader.GetAttribute("name");
            scores[2] = reader.GetAttribute("score");
        }
    }
}
