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
    public partial class Supplier : Form
    {
        private ComboBox cmbSupplier;

        public Supplier()
        {
            InitializeComponent();
        }
        public Supplier(ComboBox cmbSupplier)
        {
            InitializeComponent();
            this.cmbSupplier = cmbSupplier;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                int index = 0;
                try
                {
                    index = lvSupplier.SelectedIndices[0];
                    SupplierRepo.delete(Guid.Parse(lvSupplier.Items[index].SubItems[0].Text));
                    fillListView();
                    clearAll();
                    MessageBox.Show("Supplier has been deleted.");
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
                    btnClear.Enabled = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select a supplier to delete.");
                }
            }

        }

        private void Supplier_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void clearAll()
        {
            txtSupplierName.Clear();
            txtContactNumber.Clear();
            txtAddress.Clear();
        }
        private void fillListView()
        {
            var sup = SupplierRepo.suppliers();
            if (sup.Count > 0)
            {
                lvSupplier.Items.Clear();
                foreach (var item in sup)
                {
                    ListViewItem lv = new ListViewItem(item.SupplierId.ToString());
                    lv.SubItems.Add(item.SupplierName);
                    lv.SubItems.Add(item.SupplierAddress);
                    lv.SubItems.Add(item.SupplierContactNumber.ToString());
                    lvSupplier.Items.Add(lv);
                }
            }
        }
        private void Supplier_Load(object sender, EventArgs e)
        {
            fillListView();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                if (this.cmbSupplier != null) {
                    this.cmbSupplier.Items.Clear();
                    var sup = SupplierRepo.suppliers();
                    if (sup.Count > 0)
                    {
                        foreach (var item in sup)
                        {
                            this.cmbSupplier.Items.Add(item.SupplierName);
                        }
                    }
                }
                
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var supplier = SupplierRepo.retrieveByName(txtSearch.Text);

                if (supplier.Count > 0)
                {
                    lvSupplier.Items.Clear();
                    foreach (var item in supplier)
                    {
                        ListViewItem lv = new ListViewItem(item.SupplierId.ToString());
                        lv.SubItems.Add(item.SupplierName);
                        lv.SubItems.Add(item.SupplierAddress);
                        lv.SubItems.Add(item.SupplierContactNumber.ToString());
                        lvSupplier.Items.Add(lv);
                    }
                }
                else {
                    MessageBox.Show("Item not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            if (txtSupplierName.Text != "")
            {
                if (SupplierRepo.checkIfSupplierExists(txtSupplierName.Text))
                {
                    MessageBox.Show("Supplier already exists.");
                }
                else
                {
                    double num;
                    bool b = double.TryParse(txtContactNumber.Text, out num);
                    if (b)
                    {
                        SupplierRepo.insert(Guid.NewGuid(), txtSupplierName.Text, txtAddress.Text, double.Parse(txtContactNumber.Text));
                        fillListView();
                        clearAll();
                        MessageBox.Show("Supplier has been added.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid contact number");
                    }

                }
            }
            else
            {
                MessageBox.Show("Supplier name must not be empty");
            }
        }

        private void lvSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                btnCategoryDelete.Enabled = true;
                btnCategoryUpdate.Enabled = true;
                btnCategoryAdd.Enabled = false;
                btnClear.Enabled = false;
                
                index = lvSupplier.SelectedIndices[0];

                txtSupplierName.Text = lvSupplier.Items[index].SubItems[1].Text;
                txtAddress.Text = lvSupplier.Items[index].SubItems[2].Text;
                txtContactNumber.Text = lvSupplier.Items[index].SubItems[3].Text;
            }
            catch (Exception)
            {

                index = 0;
            }
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvSupplier.SelectedIndices[0];
                if (txtSupplierName.Text != "" && txtContactNumber.Text != "" && txtAddress.Text != "")
                {
                    double num;
                    bool b = double.TryParse(txtContactNumber.Text, out num);
                    if (b)
                    {
                        SupplierRepo.update(Guid.Parse(lvSupplier.Items[index].SubItems[0].Text), txtSupplierName.Text, txtAddress.Text, num);
                        MessageBox.Show("Supplier details have been been updated.");
                        btnCategoryDelete.Enabled = false;
                        btnCategoryUpdate.Enabled = false;
                        btnCategoryAdd.Enabled = true;
                        btnClear.Enabled = true;
                        fillListView();
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("Invalid contact number.");
                    }

                }
                else
                {
                    MessageBox.Show("All fields are required.");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Please select an item to update.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fillListView();
            clearAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
