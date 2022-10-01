using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P19040Atomiki2
{
    public partial class Form1 : Form
    {
        int userOption = 0; //indicates what the user wants to draw
        Graphics g;
        Pen p;             
        int coord1, coord2;
        bool canDraw;
        int option;
        //coordinates for the already made drawings
        int[,] bookCoords = new int[5, 4] { { 200, 150, 300, 150 },{ 200, 100, 300, 100 },{ 200, 150, 200, 100 },{ 300, 150, 300, 100 },{ 250, 150, 250, 100 } };
        int[,] boatCoords = new int[8, 4] { { 100, 200, 200, 200 }, { 100, 200, 130, 230 }, { 200, 200, 170, 230 }, { 130, 230, 170, 230 }, { 150, 200, 150, 150 }, { 150, 150, 170, 150 }, { 150, 160, 170, 160 }, { 170, 150, 170, 160 } };
        int[,] houseCoords = new int[9, 4] { { 600, 300, 700, 300 }, { 600, 150, 700, 150 }, { 600, 300, 600, 150 }, { 700, 300, 700, 150 }, { 600, 150, 650, 75 }, { 700, 150, 650, 75 }, { 630, 250, 670, 250 }, { 630, 300, 630, 250 }, { 670, 300, 670, 250 } };
        int[,] OOPCoords = new int[12, 4] { { 400, 250, 450, 250 }, { 400, 300, 450, 300 }, { 400, 300, 400, 250 }, { 450, 300, 450, 250 }, { 470, 250, 520, 250 }, { 470, 300, 520, 300 }, { 470, 300, 470, 250 }, { 520, 250, 520, 300 }, { 540, 300, 540, 250 }, { 540, 250, 570, 250 }, { 540, 270, 570, 270 }, { 570, 270, 570, 250 } };
        int bookindex = 0;
        int boatindex = 0;
        int houseindex = 0;
        int OOPindex = 0;
        public Form1()

        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            p = new Pen(Color.Black);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canDraw = true;
            coord1 = e.X;
            coord2 = e.Y;
        }


        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canDraw = false; //stops the drawing
            //determines which shape to draw based on the button clicked
            switch (userOption) 
            {
                case 1:
                    //line drawing
                    g.DrawLine(p, coord1, coord2,e.X,e.Y);
                    saveToDb("Line");
                    break;
                case 2:
                    //rectangle drawing
                    g.DrawRectangle(p, coord1, coord2, e.X - coord1, e.Y - coord2);
                    saveToDb("Rectangle");
                    break;
                case 3:
                    //ellipsis drawing
                    g.DrawEllipse(p, coord1, coord2, e.X - coord1, e.Y - coord2);
                    saveToDb("Ellipse");
                    break;
                case 4:
                    g.DrawEllipse(p, coord1, coord2, (e.X + e.Y)-(coord2+coord1) , (e.X + e.Y)-(coord2 + coord1));
                    saveToDb("Circle");
                    break;          
                   
                

            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //freestyle drawing
            if (canDraw && userOption == 0) 
            {
                g.DrawLine(p, coord1, coord2, e.X, e.Y);
                coord1 = e.X;
                coord2= e.Y;
            }
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            userOption = 1;
             
           
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            userOption = 2;
            
        }

        private void ellipsisButton_Click(object sender, EventArgs e)
        {
            userOption = 3;
           
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            userOption = 4;
            
        }

        private void freestyleButton_Click(object sender, EventArgs e)
        {
            userOption = 0;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p.Color = colorDialog1.Color;
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Paint app made by Antonis Darmis");
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            p.Width = 5;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            p.Width = 7;
        }

       

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            p.Width = 8;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            p.Width = 10;
        }

        

        

        private void button1_Click(object sender, EventArgs e)
        {
            option = 1;
            timer1.Enabled = true; 
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            boatTimer.Enabled = true;
        }

        private void houseButton_Click(object sender, EventArgs e)
        {        
            houseTimer.Enabled = true;
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            
            OOPtimer.Enabled = true;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer for book drawing
            
            if (timer1.Enabled) 
            {
                if (bookindex <= 4)
                {
                    g.DrawLine(p, bookCoords[bookindex, 0], bookCoords[bookindex, 1], bookCoords[bookindex, 2], bookCoords[bookindex, 3]);
                    bookindex++;
                }
                else 
                {
                    timer1.Enabled = false;
                    bookindex = 0;
                } 
                
            }
           
        }

        private void boatTimer_Tick(object sender, EventArgs e)
        {
            if (boatTimer.Enabled) 
            {
                if (boatindex <= 7)
                {
                    g.DrawLine(p, boatCoords[boatindex, 0], boatCoords[boatindex, 1], boatCoords[boatindex, 2], boatCoords[boatindex, 3]);
                    boatindex++;
                }
                else 
                {
                    boatTimer.Enabled = false;
                    boatindex = 0;
                }
            }
        }

        private void houseTimer_Tick(object sender, EventArgs e)
        {
            if (houseTimer.Enabled)
            {
                if (houseindex <= 8)
                {
                    g.DrawLine(p, houseCoords[houseindex, 0], houseCoords[houseindex, 1], houseCoords[houseindex, 2], houseCoords[houseindex, 3]);
                    houseindex++;
                }
                else
                {
                    houseTimer.Enabled = false;
                    houseindex = 0;
                }
            }
        }

        private void OOPtimer_Tick(object sender, EventArgs e)
        {
            if (OOPtimer.Enabled)
            {
                if (OOPindex <= 11)
                {
                    g.DrawLine(p, OOPCoords[OOPindex, 0], OOPCoords[OOPindex, 1], OOPCoords[OOPindex, 2], OOPCoords[OOPindex, 3]);
                    OOPindex++;
                }
                else
                {
                    OOPtimer.Enabled = false;
                    OOPindex = 0;
                }
            }
        }

        private void saveToDb(String shape) 
        {
            String connectionString;
            SQLiteConnection conn;
            connectionString = "Data Source = Shapes.db; Version = 3";
            conn = new SQLiteConnection(connectionString);
            conn.Open();
            string queryString = "INSERT INTO Shapes(Shape,Timestamp) VALUES(@shape,@time)";           
            SQLiteCommand sqlCommand = new SQLiteCommand(queryString, conn);
            sqlCommand.Parameters.AddWithValue("@shape", shape);
            sqlCommand.Parameters.AddWithValue("@time", DateTime.Now.ToString());
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            /////////
        }
    }
}
