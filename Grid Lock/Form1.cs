﻿using System;
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

        private void btnUp_Click(object sender, EventArgs e)
        {
            ValidUp(this, e, comboBoxColour.Text);
            
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ValidDown(this, e, comboBoxColour.Text);
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            ValidLeft(this, e, comboBoxColour.Text);
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            ValidRight(this, e, comboBoxColour.Text);
        }

        private void btnMiddle_Click(object sender, EventArgs e)
        {

        }
        private void MoveUp(object sender, EventArgs e, string color)
        {
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        gameBoard[i - 1, j].BackColor = Color.FromName(color);
                        gameBoard[i, j].BackColor = Color.White;
                    }
                }
            }
        }
        private void MoveDown(object sender, EventArgs e, string color)
        {
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 6; i >= 0; i += -1)
            {
                for (int j = 6; j >= 0; j += -1)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        gameBoard[i + 1, j].BackColor = Color.FromName(color);
                        gameBoard[i, j].BackColor = Color.White;
                    }
                }
            }
        }
        private void MoveLeft(object sender, EventArgs e, string color)
        {
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        gameBoard[i, j - 1].BackColor = Color.FromName(color);
                        gameBoard[i, j].BackColor = Color.White;
                    }
                }
            }
        }
        
        private void MoveRight(object sender, EventArgs e, string color)
        {
            // Loop through gameboard checking each pictureBox one at a time.
            for (int i = 6; i >= 0; i += -1)
            {
                for (int j = 6; j >= 0; j += -1)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        gameBoard[i, j + 1].BackColor = Color.FromName(color);
                        gameBoard[i, j].BackColor = Color.White;
                    }
                }
            }
        }
        private void ValidUp(object sender, EventArgs e, string color)
        {
            bool flag = false;
            for (int i = 1; i < 6; i++)
            {
                if (flag == true)
                {
                    break;
                }
                for (int j = 0; j < 7; j++)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        if (gameBoard[i + 1, j].BackColor == gameBoard[i, j].BackColor )
                        {
                            if (gameBoard[i - 1, j].BackColor == Color.White)
                            {
                                MoveUp(this, e, comboBoxColour.Text);
                                flag = true;
                                break;
                                
                            }
                        }
                        
                    }
                }
            }
        }
        private void ValidDown(object sender, EventArgs e, string color)
        {
            bool flag = false;
            for (int i = 5; i > 0; i += -1)
            {
                if (flag == true)
                {
                    break;
                }
                for (int j = 6; j >= 0; j += -1)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        if (gameBoard[i - 1, j].BackColor == gameBoard[i, j].BackColor)
                        {
                            if (gameBoard[i + 1, j].BackColor == Color.White)
                            {
                                MoveDown(this, e, comboBoxColour.Text);
                                flag = true;
                                break;
                            }
                        }

                    }
                }
            }
        }
        private void ValidLeft(object sender, EventArgs e, string color)
        {
            bool flag = false;
            for (int i = 0; i < 7; i++)
            {
                if (flag == true)
                {
                    break;
                }
                for (int j = 1; j < 6; j++)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        if (gameBoard[i, j+1].BackColor == gameBoard[i, j].BackColor)
                        {
                            if (gameBoard[i, j - 1].BackColor == Color.White)
                            {
                                MoveLeft(this, e, comboBoxColour.Text);
                                flag = true;
                                break;
                            }
                        }

                    }
                }
            }
        }
        private void ValidRight(object sender, EventArgs e, string color)
        {
            bool flag = false;
            for (int i = 6; i >= 0; i += -1)
            {
                if (flag == true)
                {
                    break;
                }
                for (int j = 5; j > 0; j += -1)
                {
                    //checks for the selected colour (i.e. Green)
                    if (gameBoard[i, j].BackColor == Color.FromName(comboBoxColour.Text))
                    {
                        if (gameBoard[i , j - 1].BackColor == gameBoard[i, j].BackColor)
                        {
                            if (gameBoard[i , j + 1].BackColor == Color.White)
                            {
                                MoveRight(this, e, comboBoxColour.Text);
                                flag = true;
                                break;
                            }
                        }

                    }
                }
            }
        }
    }
}
