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
            winners.Clear();
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "txt files (*.txt)|*.txt";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                string[] lines = null;
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
                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
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
            Clear();
            int sum = 0;

            for (int i = 0; i < linesCount; i++) // Цикл добавления строк
                dataGridView1.Rows.Add();  // добавление строки 

            for (int i = 0; i < linesCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
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

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            dataGridView1.Dispose();

            dataGridView1 = new System.Windows.Forms.DataGridView();
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Year,
            this.Country,
            this.Winner,
            this.PrizeWinner});
            dataGridView1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridView1.Location = new System.Drawing.Point(0, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;
            dataGridView1.Size = new System.Drawing.Size(673, 402);
            dataGridView1.TabIndex = 1;

            Controls.Add(this.dataGridView1);
        }
    }
}
