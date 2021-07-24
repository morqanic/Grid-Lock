using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Grid_Lock
{
    public partial class Form1 : Form
    {
        PictureBox[,] gameBoard = new PictureBox[7, 7];
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            int index = 1; 
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameBoard[i, j] = (PictureBox)Controls.Find("pictureBox" + (index).ToString(), true)[0];
                    index++;
                }
            }
            string[][] startingConfigArray = File.ReadLines(@"startingconfig.csv").Select(x => x.Split(',')).ToArray();//this ine is borowed from https://stackoverflow.com/questions/18806757/parsing-csv-file-into-2d-array/43528767 - I know enough to figure out how to use it but not enough to be able to recreate it.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameBoard[i, j].BackColor = Color.FromName(startingConfigArray[i][j]);
                }
            }
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            int a = 1;
            int b = 0;
            int c = -1;
            int x = a;
            int y = b;
            int z = 0;
            Mover(this, e, comboBoxColour.Text, a, b, c, x, y, z);
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            int a = 1;
            int b = 0;
            int c = -1;
            int x = a;
            int y = b;
            int z = 0;
            Mover(this, e, comboBoxColour.Text, a, b, c, x, y, z);
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            int a = 0;
            int b = 1;
            int c = 1;
            int x = a;
            int y = b;
            int z = 0;
            Mover(this, e, comboBoxColour.Text, a, b, c, x, y, z);
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            int a = 0;
            int b = 1;
            int c = 1;
            int x = a;
            int y = b;
            int z = 0;
            Mover(this, e, comboBoxColour.Text, a, b, c, x, y, z);
        }
        private void Mover(object sender, EventArgs e, string color, int a, int b, int c, int x, int y, int z)
        {
            bool flag = false;
            for (int i = 0 + a; i < 6 + b; i++)
            {
                if (flag == true)
                {
                    break;
                }
                for (int j = 0 + b; j < 6 + a; j++)
                {
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text) && (gameBoard[i + a, j + b].BackColor == gameBoard[i, j].BackColor))
                    {
                        if (gameBoard[i, j + 1].BackColor == gameBoard[i, j].BackColor)
                        {
                            if (gameBoard[i + c, j - c].BackColor != Color.White && gameBoard[i + b, j + a].BackColor == gameBoard[i, j].BackColor)
                            {
                                flag = true;
                                break;
                            }
                            if (gameBoard[i - a, j - b].BackColor == Color.White)
                            {
                                ActuallyMove(this, e, comboBoxColour.Text, a, b);
                                CheckWin(i - a, j - b);
                            }
                        }
                        else if (gameBoard[i - a, j - b].BackColor == Color.White)
                        {
                            ActuallyMove(this, e, comboBoxColour.Text, a, b);
                        }
                        flag = true;
                        break;
                    }
                }
            }
        }
        private void ActuallyMove(object sender, EventArgs e, string color, int a, int b)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        gameBoard[i - a, j - b].BackColor = Color.FromName(color);
                        gameBoard[i, j].BackColor = Color.White;
                    }
                }
            }
        }
        private void CheckWin(int i, int j)
        {
            if (j == 5 && i == 2)
            {
                MessageBox.Show("Congrat's My Code Works!!!");
            }
        }
    }
}
