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
            MessageBox.Show("Welcome to Grid Lock, a game based off of the game \"Rush Hour\". \nThis was made as a part of my Year 11 Programming Project and has 174 lines of code. \n \nThe aim of the game is to get the 2x2 square to the finish (illustrated by a grey rectangle). Vertical pieces can only move vertically and horizontal pieces can only move horizontally.  \n \nTimer Starts on OK!!" );
            string[][] startingConfigArray = File.ReadLines(@"startingConfigArray.csv").Select(x => x.Split(',')).ToArray();//this ine is borowed from https://stackoverflow.com/questions/18806757/parsing-csv-file-into-2d-array/43528767 - I know enough to figure out how to use it but not enough to be able to recreate it.
            BoardLoad(this, e, startingConfigArray);
        }
        private void BoardLoad(object sender, EventArgs e, string[][] configArray)
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
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameBoard[i, j].BackColor = Color.FromName(configArray[i][j]);
                }
            }
        }
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "csv files (*.csv)|*.csv",
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[][] configArray = File.ReadLines(openFileDialog1.FileName).Select(x => x.Split(',')).ToArray();//this ine is borowed from https://stackoverflow.com/questions/18806757/parsing-csv-file-into-2d-array/43528767 - I know enough to figure out how to use it but not enough to be able to recreate it.
                BoardLoad(this, e, configArray);
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
            int i2x2 = variablesList[6];
            int j2x2 = variablesList[7];
            int i2x2White = variablesList[8];
            int j2x2White = variablesList[9];
            bool flag = false;
            for (int iFalse = 0 + startModifier; iFalse < 6 + endModifier; iFalse++)
            {
                if (flag == true)
                {
                    break;
                }
                for (int jFalse = 5 + startModifier; jFalse > -1 + endModifier; jFalse += -1)
                {
                    int i = iFalse;
                    int j = jFalse;
                    if (change == true)
                    {
                        i = jFalse;
                        j = iFalse;
                    }
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text) && (gameBoard[i + iSame, j + jSame].BackColor == gameBoard[i, j].BackColor))
                    {
                        if (i + i2x2 != -1 && i + i2x2 != 7 && j + j2x2 != -1 && j + j2x2 != 7)
                        {
                            if (gameBoard[i + i2x2, j + j2x2].BackColor == gameBoard[i, j].BackColor)
                            {
                                if (gameBoard[i + i2x2White, j + j2x2White].BackColor != Color.White && gameBoard[i + i2x2, j + j2x2].BackColor == gameBoard[i, j].BackColor)
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
                for (int jFalse = 6; jFalse > -1; jFalse += -1)
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
            if (change == true)
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
