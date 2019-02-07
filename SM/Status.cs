using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMLIB.Repository;

namespace SM
{
    public partial class Status : Form
    {
        private ComboBox cmbStatus;
        private Guid id;

        public Status()
        {
            InitializeComponent();
        }
        public Status(ComboBox cmbStatus)
        {
            InitializeComponent();
            this.cmbStatus = cmbStatus;
        }

        private void Status_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                List<SMLIB.Entity.Status> statuses = StatusRepo.retrieve();
                if (statuses.Count > 0) {
                    if (this.cmbStatus != null) {
                        this.cmbStatus.Items.Clear();
                        foreach (var item in statuses)
                        {
                            this.cmbStatus.Items.Add(item.StatusValue);
                        }
                    }                    
                }
                this.Hide();
            }
        }
        private void fillListView(List<SMLIB.Entity.Status> statuses)
        {
            if (statuses.Count > 0)
            {
                lvStatus.Items.Clear();
                foreach (var item in statuses)
                {
                    ListViewItem lvi = new ListViewItem(item.StatusValue);
                    lvi.SubItems.Add(item.StatusId.ToString());
                    lvStatus.Items.Add(lvi);
                }
            }
        }
        private void clearAll()
        {
            txtCategorySearch.Clear();
            txtStatus.Clear();
        }
        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            if (txtStatus.Text != "")
            {
                bool b = StatusRepo.checkIfStatusExists(txtStatus.Text);
                if (b)
                {
                    MessageBox.Show("This status value already exists, please try again.");
                }
                else
                {
                    StatusRepo.insert(Guid.NewGuid(), txtStatus.Text);
                    MessageBox.Show("Status inserted.");
                    List<SMLIB.Entity.Status> statuses = StatusRepo.retrieve();
                    fillListView(statuses);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("Status can not be empty.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<SMLIB.Entity.Status> statuses = StatusRepo.retrieve();
            fillListView(statuses);
            clearAll();
        }

        private void Status_Load(object sender, EventArgs e)
        {
            List<SMLIB.Entity.Status> statuses = StatusRepo.retrieve();
            fillListView(statuses);
            clearAll();
        }

        private void lvStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvStatus.SelectedIndices[0];
                txtStatus.Text = lvStatus.Items[index].SubItems[0].Text;
                this.id = Guid.Parse(lvStatus.Items[index].SubItems[1].Text);
                btnCategoryDelete.Enabled = true;
                btnCategoryUpdate.Enabled = true;
                btnCategoryAdd.Enabled = false;
            }
            catch (Exception)
            {
                index = 0;
            }
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes)
                {
                    StatusRepo.delete(this.id);
                    MessageBox.Show("Status has been deleted.");
                    List<SMLIB.Entity.Status> statuses = StatusRepo.retrieve();
                    fillListView(statuses);
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
                    clearAll();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStatus.Text != "")
                {
                    bool d = StatusRepo.checkIfStatusExists(txtStatus.Text);
                    if (d)
                    {
                        MessageBox.Show("This status already exists");
                    }
                    else
                    {
                        StatusRepo.update(this.id, txtStatus.Text);
                        MessageBox.Show("Status has been updated.");
                        List<SMLIB.Entity.Status> statuses = StatusRepo.retrieve();
                        fillListView(statuses);
                        btnCategoryDelete.Enabled = false;
                        btnCategoryUpdate.Enabled = false;
                        btnCategoryAdd.Enabled = true;
                        clearAll();
                    }
                }
                else
                {
                    MessageBox.Show("Status has been updated.");
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Please select an item to update.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCategorySearch.Text != "") {
                List<SMLIB.Entity.Status> statuses = StatusRepo.retrieveByName(txtCategorySearch.Text);
                if (statuses.Count > 0)
                {
                    fillListView(statuses);
                }
                else {
                    MessageBox.Show("Status not found.");
                }
               
            }
        }
    }
}
