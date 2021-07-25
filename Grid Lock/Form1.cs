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
            bool change = false;
            List<int> variablesList = new List<int>() { 1, 0, -1, 0, 1, 0, 0, -1, -1, -1 };
            Verify(this, e, comboBoxColour.Text, change, variablesList);
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            bool change = true;
            List<int> variablesList = new List<int>() { 0, 1, 1, 0, -1, 0, 0, 1, 1, 1 };
            Verify(this, e, comboBoxColour.Text, change, variablesList);
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            bool change = true;
            List<int> variablesList = new List<int>() { 1, 0, 0, -1, 0, 1, -1, 0, -1, -1 };
            Verify(this, e, comboBoxColour.Text, change, variablesList);
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            bool change = false;
            List<int> variablesList = new List<int>() { 0, 1, 0, 1, 0, -1, 1, 0, 1, 1 };
            Verify(this, e, comboBoxColour.Text, change, variablesList);
        }
        private void Verify(object sender, EventArgs e, string color, bool change, List<int> variablesList)
        {
            int startModifier = variablesList[0];
            int endModifier = variablesList[1];
            int iWhite = variablesList[2];
            int jWhite = variablesList[3];
            int iSame = variablesList[4];
            int jSame = variablesList[5];
            int i4x4 = variablesList[6];
            int j4x4 = variablesList[7];
            int i4x4White = variablesList[8];
            int j4x4White = variablesList[9];
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
                        if (i + i4x4 != -1 && i + i4x4 != 7 && j + j4x4 != -1 && j + j4x4 != 7)
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
                                    Mover(this, e, comboBoxColour.Text, change, iWhite, jWhite);
                                    CheckWin(change, i + iWhite, j + jWhite);
                                }
                            }
                        }
                        if (gameBoard[i + iWhite, j + jWhite].BackColor == Color.White)
                        {
                            Mover(this, e, comboBoxColour.Text, change, iWhite, jWhite);
                        }
                        flag = true;
                        break;
                    }
                }
            }
        }
        private void Mover(object sender, EventArgs e, string color, bool change, int iWhite, int jWhite)
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
