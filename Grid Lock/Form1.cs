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
            int startModifier = 1;
            int endModifier = 0;
            bool change = false;
            int iWhite = -1;
            int jWhite = 0;
            int iSame = 1;
            int jSame = 0;
            int i4x4 = 0;
            int j4x4 = -1;
            int i4x4White = -1;
            int j4x4White = -1;
            Mover(this, e, comboBoxColour.Text, startModifier, endModifier, change, iWhite, jWhite, iSame, jSame, i4x4, j4x4, i4x4White, j4x4White);
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            int startModifier = 0;
            int endModifier = 1;
            bool change = true;
            int iWhite = 1;
            int jWhite = 0;
            int iSame = -1;
            int jSame = 0;
            int i4x4 = 0;
            int j4x4 = 1;
            int i4x4White = 1;
            int j4x4White = 1;
            Mover(this, e, comboBoxColour.Text, startModifier, endModifier, change, iWhite, jWhite, iSame, jSame, i4x4, j4x4, i4x4White, j4x4White);
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            int startModifier = 1;
            int endModifier = 0;
            bool change = true;
            int iWhite = 0;
            int jWhite = -1;
            int iSame = 0;
            int jSame = 1;
            int i4x4 = -1;
            int j4x4 = 0;
            int i4x4White = -1;
            int j4x4White = -1;
            Mover(this, e, comboBoxColour.Text, startModifier, endModifier, change, iWhite, jWhite, iSame, jSame, i4x4, j4x4, i4x4White, j4x4White);
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            int startModifier = 0;
            int endModifier = 1;
            bool change = false;
            int iWhite = 0;
            int jWhite = 1;
            int iSame = 0;
            int jSame = -1;
            int i4x4 = 1;
            int j4x4 = 0;
            int i4x4White = 1;
            int j4x4White = 1;
            Mover(this, e, comboBoxColour.Text, startModifier, endModifier, change, iWhite, jWhite, iSame, jSame, i4x4, j4x4, i4x4White, j4x4White);
        }
        private void Mover(object sender, EventArgs e, string color, int startModifier, int endModifier, bool change, int iWhite, int jWhite, int iSame, int jSame, int i4x4, int j4x4, int i4x4White, int j4x4White)
        {
            bool flag = false;
            for (int iFalse = 0 + startModifier; iFalse < 6 + endModifier; iFalse ++)
            {
                if (flag == true)
                {
                    break;
                }
                for (int jFalse = 5 + startModifier; jFalse > -1 + endModifier; jFalse += -1)
                {
                    int i = iFalse;
                    int j = jFalse;
                    if(change == true)
                    {
                        i = jFalse;
                        j = iFalse;
                    }
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text) && (gameBoard[i + iSame, j+ jSame].BackColor == gameBoard[i, j].BackColor))
                    {
                        if (gameBoard[i + i4x4, j + j4x4].BackColor == gameBoard[i, j].BackColor)
                        {
                            if (gameBoard[i + i4x4White, j + j4x4White].BackColor != Color.White && gameBoard[i + i4x4, j + j4x4].BackColor == gameBoard[i, j].BackColor)
                            {
                                flag = true;
                                break;
                            }
                            if (gameBoard[i + iWhite, j + jWhite].BackColor == Color.White)
                            {
                                ActuallyMove(this, e, comboBoxColour.Text, change, iWhite, jWhite);
                                CheckWin(change, i + iWhite, j + jWhite);
                            }
                        }
                        else if (gameBoard[i + iWhite, j + jWhite].BackColor == Color.White)
                        {
                            ActuallyMove(this, e, comboBoxColour.Text, change, iWhite, jWhite);
                        }
                        flag = true;
                        break;
                    }
                }
            }
        }
        private void ActuallyMove(object sender, EventArgs e, string color, bool change, int iWhite, int jWhite)
        {
            for (int iFalse = 0; iFalse < 7; iFalse++)
            {
                for (int jFalse = 6; jFalse > -1; jFalse+= -1)
                {
                    int i = iFalse;
                    int j = jFalse;
                    if (change == true)
                    {
                        i = jFalse;
                        j = iFalse;
                    }
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        gameBoard[i + iWhite, j + jWhite].BackColor = Color.FromName(color);
                        gameBoard[i, j].BackColor = Color.White;
                    }
                }
            }
        }
        private void CheckWin(bool change, int i, int j)
        {
            if(change == true)
            {
                i += -1;
                j += 1;
            }
            if (j == 6 && i == 2)
            {
                MessageBox.Show("Congrat's My Code Works!!!");
            }
        }
    }
}
