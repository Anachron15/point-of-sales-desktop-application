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
using SMLIB.Entity;

namespace SM
{
    public partial class Warehouse : Form
    {
        private ComboBox cmbWarehouse;
        private Guid id;

        public Warehouse()
        {
            InitializeComponent();
        }
        public Warehouse(ComboBox cmbWarehouse)
        {
            InitializeComponent();
            this.cmbWarehouse = cmbWarehouse;
        }
        private void Warehouse_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
               
                this.Hide();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        private void clearAll() {
            txtName.Clear();
            txtNumber.Clear();
            txtSubLocation.Clear();
            txtAddress.Clear();
            txtCategorySearch.Clear();
        }
        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes) {
                    WarehouseRepo.delete(this.id);
                    MessageBox.Show("Warehouse has been deleted.");
                    List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieve();
                    fillListView(warehouse);
                    clearAll();
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
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
                bool b = WarehouseRepo.checkIfWarehouseExists(txtName.Text);
                if (b)
                {
                    MessageBox.Show("Warehouse name already exists, please try again.");
                }
                else {
                    double number;
                    bool c = double.TryParse(txtNumber.Text, out number);
                    if (c)
                    {
                        if (txtName.Text != "")
                        {
                            WarehouseRepo.update(this.id, txtName.Text, txtAddress.Text, number, txtSubLocation.Text);
                            MessageBox.Show("Warehouse has been updated.");
                            List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieve();
                            fillListView(warehouse);
                            clearAll();
                            btnCategoryUpdate.Enabled = false;
                            btnCategoryDelete.Enabled = false;
                            btnCategoryAdd.Enabled = true;
                            
                        }
                        else
                        {
                            MessageBox.Show("Warehouse name can not be empty.");
                        }
                    }
                    else {
                        MessageBox.Show("Invalid contact number.");
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select an item to update.");                
            }
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            if ( txtName.Text != "")
            {
                double num;
                bool b = double.TryParse(txtNumber.Text, out num);
                if (b)
                {
                    bool c = WarehouseRepo.checkIfWarehouseExists(txtName.Text);
                    if (c)
                    {
                        MessageBox.Show("Warehouse already exists.");
                    }
                    else
                    {
                        WarehouseRepo.create(Guid.NewGuid(), txtName.Text, txtAddress.Text, num, txtSubLocation.Text);
                        MessageBox.Show("Warehouse has been inserted.");
                        List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieve();
                        fillListView(warehouse);
                        clearAll();
                    }

                }
                else
                {
                    MessageBox.Show("Invalid contact number.");
                }
            }
            else {
                MessageBox.Show("Please input the warehouse name.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) {
               
                List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieve();
                if (warehouse.Count > 0) {
                    if (this.cmbWarehouse != null) {
                        this.cmbWarehouse.Items.Clear();
                        foreach (var item in warehouse)
                        {
                            this.cmbWarehouse.Items.Add(item.WarehouseName);
                        }
                    }                    
                }
                this.Hide();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void fillListView(List<SMLIB.Entity.Warehouse> warehouse) {
            if (warehouse.Count > 0) {
                lvWarehouse.Items.Clear();
                foreach (var item in warehouse) {
                    ListViewItem lvi = new ListViewItem(item.WarehouseId.ToString());
                    lvi.SubItems.Add(item.WarehouseName);
                    lvi.SubItems.Add(item.WarehouseContactNumber.ToString());
                    lvi.SubItems.Add(item.WarehouseAddress);
                    lvi.SubItems.Add(item.WarehouseSubLocation);
                    lvWarehouse.Items.Add(lvi);
                }                      
            }
        }
        private void Warehouse_Load(object sender, EventArgs e)
        {
           List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieve();
            fillListView(warehouse);
        }

        private void lvWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvWarehouse.SelectedIndices[0];
                id = Guid.Parse(lvWarehouse.Items[index].SubItems[0].Text);
                txtName.Text = lvWarehouse.Items[index].SubItems[1].Text;
                txtNumber.Text = lvWarehouse.Items[index].SubItems[2].Text;
                txtSubLocation.Text = lvWarehouse.Items[index].SubItems[3].Text;
                txtAddress.Text = lvWarehouse.Items[index].SubItems[4].Text;
                btnCategoryDelete.Enabled = true;
                btnCategoryUpdate.Enabled = true;
                btnCategoryAdd.Enabled = false;
            }
            catch (Exception)
            {
                index = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieve();
            fillListView(warehouse);
            clearAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCategorySearch.Text != "")
            {
                List<SMLIB.Entity.Warehouse> warehouse = WarehouseRepo.retrieveByName(txtCategorySearch.Text);
                if (warehouse.Count > 0)
                {
                    fillListView(warehouse);
                    clearAll();
                }
                else {
                    MessageBox.Show("Item not found.");
                }
                
            }
            else {
                MessageBox.Show("Search item can not be empty, please try again.");
            }
        }
    }
}
