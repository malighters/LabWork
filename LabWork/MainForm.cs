using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LabWork
{
    public partial class MainForm : Form
    {
        public List<Animal> list = new List<Animal>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AddForm addForm = new AddForm(this))
            {
                if(addForm.ShowDialog() == DialogResult.OK)
                {
                    addForm.Close();
                }
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(rowIndex);
            list.RemoveAt(rowIndex);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
                       
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new();
            opfd.Title = "Open JSON file";
            opfd.Filter = "JSON files|*.json";
            opfd.InitialDirectory = @"C:\Users\USER\Desktop\";
            if (opfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using (StreamReader r = new StreamReader(opfd.FileName))
                    {
                        string json = r.ReadToEnd();

                        var animals = JsonConvert.DeserializeObject<List<Animal>>(json);
                        list = animals;
                        dataGridView1.Rows.Clear();
                        foreach(var animal in animals)
                        {
                            dataGridView1.Rows.Add(animal.Id, animal.Name, animal.CellNumber, animal.Gender, animal.Breeds, animal.Notes);
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file from disk.\nOriginal error: " + ex.Message);
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Animal> animals = new();
                animals = (List<Animal>)dataGridView1.DataSource;

                SaveFileDialog sfd = new();
                sfd.Title = "Save a JSON file";
                sfd.Filter = "JSON Image|*.json";
                sfd.InitialDirectory = @"C:\Users\USER\Desktop\";
                sfd.FileName = "output.json";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, JsonConvert.SerializeObject(animals, Formatting.Indented));
                }
            }
            catch
            {
                MessageBox.Show("Your table is empty");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            this.Enabled = false;
            aboutForm.Show();
        }

        private void editRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (EditForm editForm = new EditForm(this))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    editForm.Close();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var animals = from elem in list select elem;

                if (numCell.Value != 0)
                {
                    animals = animals.Where(c => c.CellNumber == numCell.Value);
                }

                if (!string.IsNullOrEmpty(textBreeds.Text))
                {
                    animals = animals.Where(c => c.Breeds.Contains(textBreeds.Text));
                }

                if (!string.IsNullOrEmpty(textNotes.Text))
                {
                    animals = animals.Where(c => c.Notes.Contains(textNotes.Text));
                }

                dataGridView1.DataSource = null;

                dataGridView1.Rows.Clear();
                foreach (var animal in animals)
                {
                    dataGridView1.Rows.Add(animal.Id, animal.Name, animal.CellNumber, animal.Gender, animal.Breeds, animal.Notes);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            dataGridView1.Rows.Clear();
            foreach (var animal in list)
            {
                dataGridView1.Rows.Add(animal.Id, animal.Name, animal.CellNumber, animal.Gender, animal.Breeds, animal.Notes);
            }

            numCell.Value = 0;
            textBreeds.Text = "";
            textNotes.Text = "";
        }
    }
}
