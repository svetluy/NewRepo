using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        int linesCount;
        Dictionary<string,int> winners= new Dictionary<string,int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "txt files (*.txt)|*.txt";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                string[] lines = null;
                dataGridView1.Rows.Clear();
                try
                {
                    lines = File.ReadAllLines(OPF.FileName, Encoding.Unicode);
                    linesCount = lines.Length;
                }
                catch (Exception e3)
                {
                    linesCount = 0;
                    MessageBox.Show(e3.Message);
                }

                FillTable(lines);
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SVF = new SaveFileDialog();
            SVF.Filter = "txt files (*.txt)|*.txt";
            if (SVF.ShowDialog() == DialogResult.OK)
            {
                string line = "";
                try
                {
                    using (StreamWriter sw = new StreamWriter(SVF.FileName, true, Encoding.Unicode))
                        for (int i = 0; i < linesCount; i++)
                            if (dataGridView1.Rows[i].Visible)
                            {
                                for (int j = 0; j < 4; j++)
                                    try
                                    {
                                        line += (dataGridView1.Rows[i].Cells[j].Value);
                                        if (j!=3)
                                            line += ":";
                                    }
                                    catch (Exception e1)
                                    {
                                        MessageBox.Show(e1.Message);
                                    }
                                sw.WriteLine(line);
                                line = "";
                            }
                }
                catch (Exception e3)
                {
                    MessageBox.Show(e3.Message);
                }

            }
        }

        private void FillTable(string[] lines)
        {
            int count = 0;
            int sum = 0;

            for (int i = 0; i < linesCount; i++) // Цикл добавления строк
                dataGridView1.Rows.Add();  // добавление строки 

            for (int i = 0; i < linesCount; i++)
            {
                for (int j = 0; j < 4; j++)
                    try
                    {
                        String[] substrings = lines[sum].Split(':');
                        dataGridView1.Rows[i].Cells[j].Value = substrings[j];
                        if(j==2)
                        {
                            if (winners.ContainsKey(substrings[j].Substring(0,3)))
                            {
                                winners[substrings[j].Substring(0, 3)]++;
                            }
                            else
                            winners.Add(substrings[j].Substring(0, 3), 1);
                        }
                    }
                    catch (Exception e1)
                    {
                          MessageBox.Show(e1.Message);
                    }
               // count = 0;
                sum++;
            }
        }

        private void количествоПобедToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Winners","Победители");
            dataGridView1.Columns.Add("Count", "Количество побед");

            for (int i = 0; i < linesCount; i++) // Цикл добавления строк
                dataGridView1.Rows.Add();  // добавление строки 



            int j = 0;

            try
            {
                foreach (var win in winners)
                {
                    dataGridView1.Rows[j].Cells[0].Value = win.Key;
                    dataGridView1.Rows[j].Cells[1].Value = win.Value;
                    j++;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            
        }
    }
}
