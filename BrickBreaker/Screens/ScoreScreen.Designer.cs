
namespace BrickBreaker
{
    partial class ScoreScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.secondPlaceName = new System.Windows.Forms.Label();
            this.secondPlaceScore = new System.Windows.Forms.Label();
            this.firstPlaceScore = new System.Windows.Forms.Label();
            this.firstPlaceName = new System.Windows.Forms.Label();
            this.thirdPlaceScore = new System.Windows.Forms.Label();
            this.thirdPlaceName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // secondPlaceName
            // 
            this.secondPlaceName.BackColor = System.Drawing.Color.Transparent;
            this.secondPlaceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondPlaceName.ForeColor = System.Drawing.SystemColors.Window;
            this.secondPlaceName.Location = new System.Drawing.Point(181, 252);
            this.secondPlaceName.Name = "secondPlaceName";
            this.secondPlaceName.Size = new System.Drawing.Size(100, 23);
            this.secondPlaceName.TabIndex = 0;
            this.secondPlaceName.Text = "Name";
            this.secondPlaceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondPlaceScore
            // 
            this.secondPlaceScore.BackColor = System.Drawing.Color.Transparent;
            this.secondPlaceScore.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondPlaceScore.ForeColor = System.Drawing.SystemColors.Window;
            this.secondPlaceScore.Location = new System.Drawing.Point(181, 304);
            this.secondPlaceScore.Name = "secondPlaceScore";
            this.secondPlaceScore.Size = new System.Drawing.Size(100, 23);
            this.secondPlaceScore.TabIndex = 1;
            this.secondPlaceScore.Text = "Score";
            this.secondPlaceScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstPlaceScore
            // 
            this.firstPlaceScore.BackColor = System.Drawing.Color.Transparent;
            this.firstPlaceScore.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstPlaceScore.ForeColor = System.Drawing.SystemColors.Window;
            this.firstPlaceScore.Location = new System.Drawing.Point(374, 252);
            this.firstPlaceScore.Name = "firstPlaceScore";
            this.firstPlaceScore.Size = new System.Drawing.Size(100, 23);
            this.firstPlaceScore.TabIndex = 3;
            this.firstPlaceScore.Text = "Score";
            this.firstPlaceScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstPlaceName
            // 
            this.firstPlaceName.BackColor = System.Drawing.Color.Transparent;
            this.firstPlaceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstPlaceName.ForeColor = System.Drawing.SystemColors.Window;
            this.firstPlaceName.Location = new System.Drawing.Point(374, 202);
            this.firstPlaceName.Name = "firstPlaceName";
            this.firstPlaceName.Size = new System.Drawing.Size(100, 23);
            this.firstPlaceName.TabIndex = 2;
            this.firstPlaceName.Text = "Name";
            this.firstPlaceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thirdPlaceScore
            // 
            this.thirdPlaceScore.BackColor = System.Drawing.Color.Transparent;
            this.thirdPlaceScore.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thirdPlaceScore.ForeColor = System.Drawing.SystemColors.Window;
            this.thirdPlaceScore.Location = new System.Drawing.Point(555, 342);
            this.thirdPlaceScore.Name = "thirdPlaceScore";
            this.thirdPlaceScore.Size = new System.Drawing.Size(100, 23);
            this.thirdPlaceScore.TabIndex = 5;
            this.thirdPlaceScore.Text = "Score";
            this.thirdPlaceScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thirdPlaceName
            // 
            this.thirdPlaceName.BackColor = System.Drawing.Color.Transparent;
            this.thirdPlaceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thirdPlaceName.ForeColor = System.Drawing.SystemColors.Window;
            this.thirdPlaceName.Location = new System.Drawing.Point(555, 290);
            this.thirdPlaceName.Name = "thirdPlaceName";
            this.thirdPlaceName.Size = new System.Drawing.Size(100, 23);
            this.thirdPlaceName.TabIndex = 4;
            this.thirdPlaceName.Text = "Name";
            this.thirdPlaceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScoreScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BrickBreaker.Properties.Resources.LeaderBoardScreen;
            this.Controls.Add(this.thirdPlaceScore);
            this.Controls.Add(this.thirdPlaceName);
            this.Controls.Add(this.firstPlaceScore);
            this.Controls.Add(this.firstPlaceName);
            this.Controls.Add(this.secondPlaceScore);
            this.Controls.Add(this.secondPlaceName);
            this.Name = "ScoreScreen";
            this.Size = new System.Drawing.Size(854, 542);
            this.Load += new System.EventHandler(this.ScoreScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label secondPlaceName;
        private System.Windows.Forms.Label secondPlaceScore;
        private System.Windows.Forms.Label firstPlaceScore;
        private System.Windows.Forms.Label firstPlaceName;
        private System.Windows.Forms.Label thirdPlaceScore;
        private System.Windows.Forms.Label thirdPlaceName;
    }
}
