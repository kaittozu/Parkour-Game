using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameBuYuli
{
    public partial class Form1 : Form
    {

        //Global Variable
        int gravity;
        int gravityValue = 8;
        int obstacleSpeed = 10;
        int score = 0;
        int highScore = 0;
        bool gameOver = false;
        Random random = new Random();


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            lblScore.Text = "Score : " + score;
            lblHighScore.Text = "High Score : " + highScore;
            Player.Top += gravity;

            //Ketika Player mendarat di platform
            if (Player.Top > 343)
            {
                gravity = 0;
                Player.Top = 343;
                Player.Image = Properties.Resources.run_down0;
            }
            else if (Player.Top < 38)
            {
                gravity = 0;
                Player.Top = 38;
                Player.Image = Properties.Resources.run_up0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = random.Next(1200, 3000);
                        score += 1;
                    }

                    if (x.Bounds.IntersectsWith(Player.Bounds))
                    {
                        gameTimer.Stop();
                        lblScore.Text += " Game Over!! Tekan Enter untuk mulai lagi. ";
                        gameOver = true;

                        // Memasukan High Score

                        if (score > highScore)
                        {
                            highScore = score;
                        }
                    }
                }
            }

            // menambah kecepatan game
            if (score > 2)
            {
                obstacleSpeed = 20;
                gravityValue = 12;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (Player.Top == 343)
                {
                    Player.Top -= 10;
                    gravity = -gravityValue;
                }
                else if (Player.Top == 38)
                {
                    Player.Top += 10;
                    gravity = gravityValue;
                }
            }

            if (e.KeyCode == Keys.Enter && gameOver == true) 
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            lblScore.Parent = pictureBox1;
            lblHighScore.Parent = pictureBox2;
            lblHighScore.Top = 0;
            Player.Location = new Point(180, 149);
            Player.Image = Properties.Resources.run_down0;
            score = 0;
            gravityValue = 8;
            gravity = gravityValue;
            obstacleSpeed = 10;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left = random.Next(1200, 3000);
                }
            }

            gameTimer.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
