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
    public partial class MainMenu : Form
    {
        List<User> playerList = new List<User>();
        List<User> sorted = new List<User>();
        StringBuilder sb = new StringBuilder();
        User player1;
        Form1 form1;
        User best;
        int diff;
        public MainMenu(Form1 f1,User signedUser)
        {
            player1 = signedUser;
            form1 = f1;
            InitializeComponent();
        }

        public MainMenu()
        {
            InitializeComponent();
        }


        private void signOut_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Close();
        }


        private void MainMenu_Load(object sender, EventArgs e)
        {
            FileStream str = File.OpenRead("users.txt");
            BinaryFormatter bf = new BinaryFormatter();
            //read the users into the list
            playerList = (List<User>)bf.Deserialize(str);
            str.Close();
            label11.Text = player1.UsName;
            //default chosen difficulty is easy
            label6.Text = "Easy";
            diff = 1; 
            label7.Text = player1.hScore1.ToString(); //score for easy difficulty          
            sorted = sortedList();
            best = sorted.ElementAt(0);         
            setOnDiff();

        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //easy
            sorted = sortedList();
            best = sorted.ElementAt(0);
            label6.Text = "Easy";
            diff = 1;
            label7.Text = player1.hScore1.ToString(); //score for easy difficulty
            label8.Text = sb.Append(sorted.ElementAt(0).UsName).Append("-").Append(sorted.ElementAt(0).hScore1.ToString()).ToString();
            sb.Clear();
            label9.Text = sb.Append(sorted.ElementAt(1).UsName).Append("-").Append(sorted.ElementAt(1).hScore1.ToString()).ToString();
            sb.Clear();
            label10.Text = sb.Append(sorted.ElementAt(2).UsName).Append("-").Append(sorted.ElementAt(2).hScore1.ToString()).ToString();
            sb.Clear();
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //medium
            sorted = sortedList();
            best = sorted.ElementAt(0);
            label6.Text = "Medium";
            diff = 2;
            label7.Text = player1.hScore2.ToString(); //score for medium difficulty
            label8.Text = sb.Append(sorted.ElementAt(0).UsName).Append("-").Append(sorted.ElementAt(0).hScore2.ToString()).ToString();
            sb.Clear();
            label9.Text = sb.Append(sorted.ElementAt(1).UsName).Append("-").Append(sorted.ElementAt(1).hScore2.ToString()).ToString();
            sb.Clear();
            label10.Text = sb.Append(sorted.ElementAt(2).UsName).Append("-").Append(sorted.ElementAt(2).hScore2.ToString()).ToString();
            sb.Clear();
        }



        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //medium
            sorted = sortedList();
            best = sorted.ElementAt(0);
            label6.Text = "Hard";
            diff = 3;
            label7.Text = player1.hScore3.ToString(); //score for hard difficulty
            label8.Text = sb.Append(sorted.ElementAt(0).UsName).Append("-").Append(sorted.ElementAt(0).hScore3.ToString()).ToString();
            sb.Clear();
            label9.Text = sb.Append(sorted.ElementAt(1).UsName).Append("-").Append(sorted.ElementAt(1).hScore3.ToString()).ToString();
            sb.Clear();
            label10.Text = sb.Append(sorted.ElementAt(2).UsName).Append("-").Append(sorted.ElementAt(2).hScore3.ToString()).ToString();
            sb.Clear();
        }



        private void MainMenu_VisibleChanged(object sender, EventArgs e)
        {
            //read the file again on visibility change for possible edits on the file(ex. user highscores)
            FileStream str = File.OpenRead("users.txt");
            BinaryFormatter bf = new BinaryFormatter();
            playerList = (List<User>)bf.Deserialize(str);
            label6.Text = "Easy";
            label7.Text = player1.hScore1.ToString(); //score for easy difficulty
            sorted = sortedList();
            best = sorted.ElementAt(0);
            str.Close();
            setOnDiff();        
        }


        private List<User> sortedList() 
        {
            List<User> bestScores = new List<User>();
            if (radioButton1.Checked)
            {
                //sort based on easy highscores
                bestScores = playerList.OrderByDescending(h => h.hScore1).ToList();
            }
            else if (radioButton2.Checked)
            {
                //sort based on medium highscores
                bestScores = playerList.OrderByDescending(h => h.hScore2).ToList();
            }
            else 
            {
                //sort based on hard highscores
                bestScores = playerList.OrderByDescending(h => h.hScore3).ToList();
            }
            return bestScores;
        }


        private void howToPlayButton_Click(object sender, EventArgs e)
        {
            HowToPlay howToPlay = new HowToPlay();
            howToPlay.Show();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Game game = new Game(this, player1, best, diff);
            this.Hide();
            game.Show();
        }


        private void setOnDiff() 
        {
            switch (diff)
            {
                case 1:
                    label6.Text = "Easy";
                    label7.Text = player1.hScore1.ToString();
                    label8.Text = sb.Append(sorted.ElementAt(0).UsName).Append("-").Append(sorted.ElementAt(0).hScore1.ToString()).ToString();
                    sb.Clear();
                    label9.Text = sb.Append(sorted.ElementAt(1).UsName).Append("-").Append(sorted.ElementAt(1).hScore1.ToString()).ToString();
                    sb.Clear();
                    label10.Text = sb.Append(sorted.ElementAt(2).UsName).Append("-").Append(sorted.ElementAt(2).hScore1.ToString()).ToString();
                    sb.Clear();
                    break;
                case 2:
                    label6.Text = "Medium";
                    label7.Text = player1.hScore2.ToString();
                    label8.Text = sb.Append(sorted.ElementAt(0).UsName).Append("-").Append(sorted.ElementAt(0).hScore2.ToString()).ToString();
                    sb.Clear();
                    label9.Text = sb.Append(sorted.ElementAt(1).UsName).Append("-").Append(sorted.ElementAt(1).hScore2.ToString()).ToString();
                    sb.Clear();
                    label10.Text = sb.Append(sorted.ElementAt(2).UsName).Append("-").Append(sorted.ElementAt(2).hScore2.ToString()).ToString();
                    sb.Clear();
                    break;
                case 3:
                    label6.Text = "Hard";
                    label7.Text = player1.hScore3.ToString();
                    label8.Text = sb.Append(sorted.ElementAt(0).UsName).Append("-").Append(sorted.ElementAt(0).hScore3.ToString()).ToString();
                    sb.Clear();
                    label9.Text = sb.Append(sorted.ElementAt(1).UsName).Append("-").Append(sorted.ElementAt(1).hScore3.ToString()).ToString();
                    sb.Clear();
                    label10.Text = sb.Append(sorted.ElementAt(2).UsName).Append("-").Append(sorted.ElementAt(2).hScore3.ToString()).ToString();
                    sb.Clear();
                    break;
            }
        }
    }

   


}
