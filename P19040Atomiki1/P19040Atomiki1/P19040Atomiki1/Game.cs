using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P19040Atomiki1
{  
    public partial class Game : Form
    {
        List<User> currPlayers = new List<User>();
        int score = 0; //player score
        int countdown = 20; //game timer
        User currPlayer;
        MainMenu mainMenu; 
        int difficulty;
        User bestPlayer;
        Random random; //dice probability 
        int randomImage;
        public Game(MainMenu mM,User user1,User best,int diff)
        {
            this.mainMenu = mM;
            this.currPlayer = user1;
            this.difficulty = diff;
            this.bestPlayer = best;
            InitializeComponent();
        }

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            //read list of users from file
            FileStream str = File.OpenRead("users.txt");
            BinaryFormatter bf = new BinaryFormatter();
            currPlayers = (List<User>)bf.Deserialize(str);
            str.Close();
            label8.Text = currPlayer.UsName;
            label11.Text = bestPlayer.UsName;
            random = new Random(); //initialize random object
            //set the label values for highscores based on chosen difficulty
            switch (difficulty) 
            {
                case 1:
                    label9.Text = "Easy";
                    label10.Text = currPlayer.hScore1.ToString();
                    label12.Text = bestPlayer.hScore1.ToString();
                    break;
                case 2:
                    label9.Text = "Medium";
                    label10.Text = currPlayer.hScore2.ToString();
                    label12.Text = bestPlayer.hScore2.ToString();
                    pictureBox1.Height = 50;
                    pictureBox1.Width = 50;
                    break;
                case 3:
                    label9.Text = "Hard";
                    label10.Text = currPlayer.hScore3.ToString();
                    label12.Text = bestPlayer.hScore3.ToString();
                    pictureBox1.Height = 30;
                    pictureBox1.Width = 30;
                    break;
            }

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            label13.Text = countdown.ToString();
            label13.Visible = true;
            label14.Visible = true;
            if (!timer1.Enabled) 
            {
                timer1.Enabled = true;
                playButton.Visible = false;
                pictureBox1.Visible = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            randomImage = random.Next(1, 7);
            pictureBox1.ImageLocation = "dices/" + randomImage.ToString() + ".png";
            int x1 = pictureBox1.Location.X;
            int y1 = pictureBox1.Location.Y;
            //dice moves only inside the panel
            x1 = random.Next(panel1.Width - pictureBox1.Width);
            y1 = random.Next(panel1.Height - pictureBox1.Height);
            pictureBox1.Location = new Point(x1, y1);
            //timer for the game(1 minute)           
            countdown--;
            label13.Text = countdown.ToString();
            if (countdown == 0) 
            {
                label15.Visible = true;
                pictureBox1.Enabled = false;
                pictureBox1.Visible = false;
                timer1.Enabled = false;
                if (score > Int32.Parse(label12.Text)) 
                {
                    //new best user 
                    label11.Text = currPlayer.UsName;
                    label12.Text = score.ToString();
                }
                int index = searchUser(currPlayer);
                if (index != -1) 
                {
                    //change current user score
                    if (difficulty == 1)
                    {
                        if (score > currPlayer.hScore1) 
                        {
                            currPlayer.hScore1 = score;
                            currPlayers.ElementAt(index).hScore1 = score;
                            label10.Text = currPlayer.hScore1.ToString();
                        }
                       
                    }
                    else if (difficulty == 2)
                    {
                        if (score > currPlayer.hScore2) 
                        {
                            currPlayer.hScore2 = score;
                            currPlayers.ElementAt(index).hScore2 = score;
                            label10.Text = currPlayer.hScore2.ToString();
                        }
                        
                    }
                    else
                    {
                        if (score > currPlayer.hScore3)
                        {
                            currPlayer.hScore3 = score;
                            currPlayers.ElementAt(index).hScore3 = score;
                            label10.Text = currPlayer.hScore3.ToString();
                        }
                    }
                }
                //save changes into file
                FileStream str = File.Create("users.txt");
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str, currPlayers);
                str.Close();                        
            }
        }


        int searchUser(User player) 
        {
            for (int i = 0; i < currPlayers.Count; i++) 
            {
                if (player.UsName.Equals(currPlayers.ElementAt(i).UsName))
                {
                    return i;
                }
            }
            return -1;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            //start game again
            timer1.Enabled = true;
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            label14.Text = "0";
            label15.Visible = false;
            countdown = 20;
            score = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            score += randomImage;
            label14.Text = score.ToString();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            ///////////////////////////////////
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            switch (difficulty)
            {
                case 1:
                    //no points are subtracted
                    break;
                case 2:
                    // if the score minus the points is below zero,set score to zero.
                    if (score - 20 < 0)
                    {
                        score = 0;
                    }
                    else
                    {
                        score -= 20;
                    }
                    break;
                case 3:
                    // reset score to zero.
                    score = 0;
                    break;
            }
            label14.Text = score.ToString(); 
        }
    }
}
