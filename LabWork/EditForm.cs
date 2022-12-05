using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabWork
{
    public partial class EditForm : Form
    {
        MainForm frm;
        public Animal animal;

        public EditForm(MainForm _frm)
        {
            InitializeComponent();
            animal = new Animal();
            frm = _frm;
        }

        private Boolean CheckId(uint id, int rowIndex)
        {
            try {
                foreach (DataGridViewRow row in frm.dataGridView1.Rows)
                {
                    if (id.CompareTo(row.Cells[0].Value) == 0 && row.Index != rowIndex)
                        return false;

                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool action = true;
            int rowIndex = frm.dataGridView1.CurrentCell.RowIndex;
            if (CheckId((uint)numericUpDown1.Value, rowIndex))
            {
                animal.Id = (uint)numericUpDown1.Value;
            }
            else
            {
                MessageBox.Show("This id is used now, please enter another id");
                action = false;
            }

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                animal.Name = (string)textBox1.Text;
            }
            else
            {
                MessageBox.Show("Please enter the Name row propely!");
                action = false;
            }

            animal.CellNumber = (uint)numericUpDown2.Value;

            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Please choose the Gender!");
                action = false;
            }
            else
            {
                animal.Gender = radioButton1.Checked ? 'M' : 'F';
            }


            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                animal.Breeds = (string)textBox2.Text;
            }
            else
            {
                MessageBox.Show("Please enter the Breeds row propely!");
                action = false;
            }

            animal.Notes = (string)textBox3.Text;

            if (action)
            {
                DataGridViewRow row = frm.dataGridView1.Rows[rowIndex];
                row.Cells[0].Value = numericUpDown1.Value;
                row.Cells[1].Value = textBox1.Text;
                row.Cells[2].Value = numericUpDown2.Value;
                row.Cells[3].Value = radioButton1.Checked ? 'M' : 'F';
                row.Cells[4].Value = textBox2.Text;
                row.Cells[5].Value = textBox3.Text;
                
                frm.list[rowIndex] = new Animal(Convert.ToUInt32(numericUpDown1.Value), textBox1.Text, Convert.ToUInt32(numericUpDown2.Value), radioButton1.Checked ? 'M' : 'F', textBox2.Text, textBox3.Text);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            int rowIndex = frm.dataGridView1.CurrentCell.RowIndex;
            DataGridViewRow row = frm.dataGridView1.Rows[rowIndex];
            numericUpDown1.Value = (uint)row.Cells[0].Value;
            textBox1.Text = (string)row.Cells[1].Value;
            numericUpDown2.Value = (uint)row.Cells[2].Value;
            if (row.Cells[3].Value.ToString() == "M")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            textBox2.Text = (string)row.Cells[4].Value;
            textBox3.Text = (string)row.Cells[5].Value;
        }
    }
}
