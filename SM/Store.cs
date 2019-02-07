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
    public partial class Store : Form
    {
        private ComboBox cmbStore;
        private Guid id;

        public Store()
        {
            InitializeComponent();
        }
        public Store(ComboBox cmbStore)
        {
            InitializeComponent();
            this.cmbStore = cmbStore;
            List<SMLIB.Entity.Store> s = StoreRepo.retrieve();
            initStore(s);
        }

        private void initStore(List<SMLIB.Entity.Store> s)
        {

            if (s.Count > 0)
            {
                lvStore.Items.Clear();
                foreach (var item in s)
                {
                    ListViewItem lvi = new ListViewItem(item.StoreId.ToString());
                    lvi.SubItems.Add(item.StoreName);
                    lvi.SubItems.Add(item.StoreContactNumber.ToString());
                    lvi.SubItems.Add(item.StoreAddress);
                    lvStore.Items.Add(lvi);
                }
            }
        }
        private void Store_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                if (this.cmbStore != null) {
                    this.cmbStore.Items.Clear();
                    List<SMLIB.Entity.Store> store = StoreRepo.retrieve();
                    for (int i = 0; i < store.Count; i++)
                    {
                        if (store.Count > 0)
                        {
                            this.cmbStore.Items.Add(store[i].StoreName);
                        }
                    }
                }                            
                this.Hide();
            }
        }

        private void lvStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvStore.SelectedIndices[0];
                this.id = Guid.Parse(lvStore.Items[index].SubItems[0].Text);

                txtStoreName.Text = lvStore.Items[index].SubItems[1].Text;
                txtContactNumber.Text = lvStore.Items[index].SubItems[2].Text;
                txtAddress.Text = lvStore.Items[index].SubItems[3].Text;
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
                    StoreRepo.delete(this.id);
                    List<SMLIB.Entity.Store> s = StoreRepo.retrieve();
                    initStore(s);
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
                    MessageBox.Show("Store has been deleted.");
                    clearAll();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }
        private void clearAll()
        {
            txtAddress.Clear();
            txtCategorySearch.Clear();
            txtContactNumber.Clear();
            txtStoreName.Clear();
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvStore.SelectedIndices[0];
                if (txtStoreName.Text != "")
                {
                    bool b = StoreRepo.checkIfStoreExists(txtStoreName.Text);
                    if (b)
                    {
                        MessageBox.Show("This store name already exists, \n do you wish to proceed?.");
                        double num = 0;
                        bool c = double.TryParse(txtContactNumber.Text, out num);
                        if (c)
                        {
                            StoreRepo.update(this.id, txtStoreName.Text, txtAddress.Text, num);
                            btnCategoryAdd.Enabled = true;
                            btnCategoryDelete.Enabled = false;
                            btnCategoryUpdate.Enabled = false;
                            List<SMLIB.Entity.Store> s = StoreRepo.retrieve();
                            initStore(s);
                            MessageBox.Show("Update successful.");
                            clearAll();
                        }
                        else
                        {
                            MessageBox.Show("Invalid contact number.");
                        }

                    }
                    else
                    {
                        double num = 0;
                        bool c = double.TryParse(txtContactNumber.Text, out num);
                        if (c)
                        {
                            StoreRepo.update(this.id, txtStoreName.Text, txtAddress.Text, num);
                            btnCategoryAdd.Enabled = true;
                            btnCategoryDelete.Enabled = false;
                            btnCategoryUpdate.Enabled = false;
                            List<SMLIB.Entity.Store> s = StoreRepo.retrieve();
                            initStore(s);
                            MessageBox.Show("Update successful.");
                            clearAll();
                        }
                        else
                        {
                            MessageBox.Show("Invalid contact number.");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Update value can not be empty.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please select an item to update");
            }
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStoreName.Text != "")
                {
                    double num;
                    bool b = double.TryParse(txtContactNumber.Text, out num);
                    if (b)
                    {
                        StoreRepo.insert(Guid.NewGuid(), txtStoreName.Text, txtAddress.Text, num);
                        List<SMLIB.Entity.Store> s = StoreRepo.retrieve();
                        initStore(s);
                        MessageBox.Show("Store inserted.");
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("Invalid contact number.");
                    }
                }
                else
                {
                    MessageBox.Show("Store name can not be empty.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Please contact your software provider.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<SMLIB.Entity.Store> s = StoreRepo.retrieve();
            initStore(s);
            clearAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategorySearch.Text != "")
                {
                    SMLIB.Entity.Store store = StoreRepo.retrieveByName(txtCategorySearch.Text);
                    if (store != null)
                    {
                        lvStore.Items.Clear();
                        ListViewItem lvi = new ListViewItem(store.StoreId.ToString());
                        lvi.SubItems.Add(store.StoreName);
                        lvi.SubItems.Add(store.StoreContactNumber.ToString());
                        lvi.SubItems.Add(store.StoreAddress);
                        lvStore.Items.Add(lvi);
                    }
                    else
                    {
                        MessageBox.Show("Search item not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Search item can not be empty, please try again.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Item not found.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
