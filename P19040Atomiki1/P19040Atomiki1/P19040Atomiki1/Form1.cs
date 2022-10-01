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
    public partial class Form1 : Form
    {
        List<User> players = new List<User>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream str = File.OpenRead("users.txt");
            BinaryFormatter bf = new BinaryFormatter();
            if (str.Length > 0)
            {
                //there are registered users
                players = (List<User>)bf.Deserialize(str);                            
            }
            str.Close();
        }


        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            FileStream str = File.OpenRead("users.txt");
            BinaryFormatter bf = new BinaryFormatter();
            if (str.Length > 0)
            {
                //there are registered users
                players = (List<User>)bf.Deserialize(str);             
            }
            str.Close();
        }


        private void createButton_Click(object sender, EventArgs e)
        {
            AccountCreation accountCreation = new AccountCreation(this);
            accountCreation.Show();
            this.Hide();
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            bool found = false;
            foreach (User user in players) 
            {
                if (textBox1.Text.Equals(user.UsName) && textBox2.Text.Equals(user.Pswrd)) 
                {
                    //user found
                    found = true;
                    MessageBox.Show("Successfull login.");
                    MainMenu mainMenu = new MainMenu(this,user);
                    mainMenu.Show();
                    this.Hide();
                    textBox1.Text = null;
                    textBox2.Text = null;
                    break;
                }
            }
            if (!found) 
            {
                MessageBox.Show("User with given credentials was not found.");
                textBox1.Text = null;
                textBox2.Text = null;
            }

        }
    }
}
