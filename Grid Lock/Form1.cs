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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveLeft(this, e, comboBoxColour.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxColour_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            int index = 1; //This tracks the pictureBox we are up to 
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameBoard[i, j] = (PictureBox)Controls.Find("pictureBox" + (index).ToString(), true)[0];
                    index++;
                }
            }
            string[][] startingConfigArray = File.ReadLines(@"startingconfig.csv").Select(x => x.Split(',')).ToArray();//this ine is borowed from https://stackoverflow.com/questions/18806757/parsing-csv-file-into-2d-array/43528767
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameBoard[i, j].BackColor = Color.FromName(startingConfigArray[i][j]);
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void MoveUp(object sender, EventArgs e, string color)
        {
            bool moveLock = false; //disables movement of the tile
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        //If on the first row locks movement (i.e. prevents moving upwards out of the gameboard)
                        if (i == 0)
                        {
                            moveLock = true;
                        }
                        //if not on the first row swaps the colours around(i.e.moves square up by one)
                        if (i > 0)
                        {
                            gameBoard[i - 1, j].BackColor = Color.FromName(color);
                            gameBoard[i, j].BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveUp(this, e, comboBoxColour.Text);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveDown(this, e, comboBoxColour.Text);
        }
        private void MoveLeft(object sender, EventArgs e, string color)
        {
            bool moveLock = false; //disables movement of the tile
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        //If on the first row locks movement (i.e. prevents moving upwards out of the gameboard)
                        if (j == 0)
                        {
                            moveLock = true;
                        }
                        //if not on the first row swaps the colours around(i.e.moves square up by one)
                        if (j > 0)
                        {
                            gameBoard[i, j - 1].BackColor = Color.FromName(color);
                            gameBoard[i, j].BackColor = Color.White;
                        }
                    }
                }
            }
        }
        private void MoveDown(object sender, EventArgs e, string color)
        {
            bool moveLock = false; //disables movement of the tile
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 6; i >= 0; i += -1)
            {
                for (int j = 6; j >= 0; j+=-1)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        //If on the first row locks movement (i.e. prevents moving upwards out of the gameboard)
                        if (i == 0)
                        {
                            moveLock = true;
                        }
                        //if not on the first row swaps the colours around(i.e.moves square up by one)
                        if (i < 6)
                        {
                            gameBoard[i + 1, j].BackColor = Color.FromName(color);
                            gameBoard[i, j].BackColor = Color.White;
                        }
                    }
                }
            }
        }
        private void MoveRight(object sender, EventArgs e, string color)
        {
            bool moveLock = false; //disables movement of the tile
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 6; i >= 0; i += -1)
            {
                for (int j = 6; j >= 0; j += -1)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        //If on the first row locks movement (i.e. prevents moving upwards out of the gameboard)
                        if (j == 0)
                        {
                            moveLock = true;
                        }
                        //if not on the first row swaps the colours around(i.e.moves square up by one)
                        if (j < 6)
                        {
                            gameBoard[i, j + 1].BackColor = Color.FromName(color);
                            gameBoard[i, j].BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveRight(this, e, comboBoxColour.Text);
        }

        private void btnMiddle_Click(object sender, EventArgs e)
        {

        }
    }
}
