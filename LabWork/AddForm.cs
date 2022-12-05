using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabWork
{
    public partial class AddForm : Form
    {
        MainForm frm;

        public Animal animal;
        public AddForm(MainForm _frm)
        {
            InitializeComponent();
            animal = new Animal();
            frm = _frm;
        }

        public Animal GetAnimal()
        {
            return animal;
        }
        
        private Boolean CheckId(uint id)
        {
            foreach (DataGridViewRow row in frm.dataGridView1.Rows)
            {
                if (id.CompareTo(row.Cells[0].Value) == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool action = true;
            
            if (CheckId((uint)numericId.Value))
            {
                animal.Id = (uint)numericId.Value;
            }
            else
            {
                MessageBox.Show("This id is used now, please enter another id");    
                action = false;
            
            }

            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                animal.Name = (string)txtName.Text;
            }
            else
            {
                if (action)
                {
                    MessageBox.Show("Please enter the Name row propely!");
                    action = false;
                }
            }

            animal.CellNumber = (uint)numericCell.Value;

            if (radioButtonM.Checked == false && radioButtonF.Checked == false)
            {
                if (action)
                {
                    MessageBox.Show("Please choose the Gender!");
                    action = false;
                }
            }
            else
            {
                animal.Gender = radioButtonM.Checked ? 'M' : 'F';
            }
            
            
            if (!string.IsNullOrWhiteSpace(txtBreeds.Text))
            {
                animal.Breeds = (string)txtBreeds.Text;
            }
            else
            {
                if (action)
                {
                    MessageBox.Show("Please enter the Breeds row propely!");
                    action = false;
                }
            }

            animal.Notes = (string)txtNotes.Text;

            if (action)
            {
                frm.dataGridView1.Rows.Add(animal.Id, animal.Name, animal.CellNumber, animal.Gender, animal.Breeds, animal.Notes);
                frm.list.Add(animal);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
