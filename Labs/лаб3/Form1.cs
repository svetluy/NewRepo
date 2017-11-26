using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лаб3
{
    public partial class Form1 : Form
    {
        TextBox[,] boxArr = new TextBox[10,10];
        int[,] arr = new int[10,10];

        public Form1()
        {
            InitializeComponent();
            CreateTable();
        }

        private void CreateTable()
        {
            foreach (var box in boxArr)
                Controls.Remove(box);

            Point location = new Point(10,30);
            Size size = new Size(30,30);
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                for (int j = 0; j < numericUpDown2.Value; j++)
                {
                    location.X += 40;
                    boxArr[i, j] = new TextBox
                    {
                        Location = location,
                        Size = size
                    };
                    boxArr[i, j].Click += TextBox_click;
                    Controls.Add(boxArr[i,j]);
                }
                location.X = 10;
                location.Y += 30;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CreateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                for (int j = 0; j < numericUpDown2.Value; j++)
                {
                    arr[i, j] = rand.Next(101)-50;
                    boxArr[i, j].Text = $"{arr[i,j]}";
                }
            }
        }

        private void TextBox_click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDown1.Value; i++)
                for (int j = 0; j < numericUpDown2.Value; j++)
                    if (boxArr[i, j].Equals((TextBox)sender))
                        Sum(arr, i, j);

            for (int i = 0; i < numericUpDown1.Value; i++)
                for (int j = 0; j < numericUpDown2.Value; j++)
                    boxArr[i, j].Text = $"{arr[i, j]}";
        }

        private void Sum(int[,] arr, int line, int column)
        {
            for (int i = line - 1; i < line + 2; i++)
                if (i > -1 && i < 10)
                    for (int j = column - 1; j < column + 2; j++)
                        if (j > -1 && j < 10)
                            if (!(i == line && j == column))
                               arr[line,column] += arr[i, j];
        }
    }
}
