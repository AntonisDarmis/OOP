using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P19040Atomiki1
{
    [Serializable]
    public class User
    {
        private string username;
        private string password;
        private int highscore1;
        private int highscore2;
        private int highscore3;
        public User(String name, String pass, int highScore1, int highScore2, int highScore3)
        {
            UsName = name;
            Pswrd = pass;
            hScore1 = highScore1;
            hScore2 = highScore2;
            hScore3 = highScore3;
        }
        public string UsName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Pswrd
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public int hScore1
        {
            get
            {
                return highscore1;
            }
            set
            {
                highscore1 = value;
            }

        }

        public int hScore2
        {
            get
            {
                return highscore2;
            }
            set
            {
                highscore2 = value;
            }

        }

        public int hScore3
        {
            get
            {
                return highscore3;
            }
            set
            {
                highscore3 = value;
            }

        }
    }
}
