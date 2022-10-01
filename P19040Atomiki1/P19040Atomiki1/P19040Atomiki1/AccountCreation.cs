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
    public partial class AccountCreation : Form
    {
        List<User> users;
        Form1 form1;
        public AccountCreation(Form1 f1)
        {
            form1 = f1;
            InitializeComponent();
        }

        public AccountCreation()
        {
            InitializeComponent();
        }

        private void AccountCreation_Load(object sender, EventArgs e)
        {
            form1.Hide();
           try
            {
                FileStream str = File.OpenRead("users.txt");
                BinaryFormatter bf = new BinaryFormatter();
                if (str.Length > 0) //there are registered users,deserialize into list
                {
                    users = (List<User>)bf.Deserialize(str);
                }
                else
                {
                    //no registered users,initialize list
                    users = new List<User>();
                }
                str.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("An unexpected error occured.");
                this.Close();
            }           
        }

        private bool validateName() 
        {
            if (textBox1.Text.Length >= 5 && textBox1.Text.Length <= 15) //username is inside given range
            {
                if (users.Count > 0) //there are registered users
                {
                    foreach (User user in users)
                    {
                        if (user.UsName.Equals(textBox1.Text))
                        {
                            MessageBox.Show("Username is taken,choose another one.");
                            return false;
                        }
                    }
                    //no matching username was found
                    MessageBox.Show("Valid username.");
                    return true;
                }
                else
                {
                    //no registered users
                    return true;
                }
            }
            else 
            {
                //username outside given range
                MessageBox.Show("Username outside given range.");
                return false;
            }
        }


        private bool matchingPasswords() 
        {
            if (textBox2.Text.Equals(textBox3.Text))
            {
                //passwords match
                return true;
            }
            else 
            {
                return false;
            }
        }

        void clearTextBoxes()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = null;
                }
            }
        }

        private void creationButton_Click(object sender, EventArgs e)
        {
            if (validateName() && matchingPasswords())
            {
                //save user credentials into file
                User user = new User(textBox1.Text, textBox2.Text, 0, 0, 0);
                users.Add(user);
                FileStream str = File.Create("users.txt");
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str, users);
                str.Close();
                MessageBox.Show("Created account successfully.");
                this.Close();
                form1.Show();
            }
            else 
            {
                MessageBox.Show("Failed to create account.");
                clearTextBoxes();
            }
        }
    }
}
