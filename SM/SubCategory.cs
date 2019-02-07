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
    public partial class SubCategory : Form
    {
        private ComboBox cmbSubCategory;

        public SubCategory()
        {
            InitializeComponent();
            fillListView();
        }
        public SubCategory(ComboBox cmbSubCategory)
        {
            InitializeComponent();
            fillListView();
            this.cmbSubCategory = cmbSubCategory;
        }

        private void SubCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) {                
                this.Hide();
            }
        }

        private void SubCategory_Load(object sender, EventArgs e)
        {
           
        }

        private void fillListView() {
            lvSubCategory.Items.Clear();
            var subcat = SubCategoryRepo.retrieve();
            if (subcat.Count > 0) {
                for (int i = 0; i < subcat.Count; i++) {
                    ListViewItem itm = new ListViewItem(subcat[i].SubCategoryValue);
                    itm.SubItems.Add(subcat[i].SubCategoryId.ToString());
                    lvSubCategory.Items.Add(itm);
                }             
            }
            
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryValue.Text != "")
                {
                    bool b = SubCategoryRepo.checkIfSubCategoryExists(txtCategoryValue.Text);
                    if (b)
                    {
                        MessageBox.Show("This sub category already exists.");
                        txtCategoryValue.Clear();
                    }
                    else
                    {
                        SubCategoryRepo.insert(Guid.NewGuid(), txtCategoryValue.Text);
                        fillListView();
                        MessageBox.Show("Sub Category has been inserted.");
                        txtCategoryValue.Clear();
                    }
                }
                else {
                    MessageBox.Show("Sub-Category value can not be empty, please try again.");
                }            
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Please contact your software provider.");
            }
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                if (txtCategoryValue.Text != "")
                {
                    bool b = SubCategoryRepo.checkIfSubCategoryExists(txtCategoryValue.Text);
                    if (b)
                    {
                        MessageBox.Show("This sub-category already exists, please try a new name");
                    }
                    else
                    {
                        index = lvSubCategory.SelectedIndices[0];
                        SubCategoryRepo.update(Guid.Parse(lvSubCategory.Items[index].SubItems[1].Text), txtCategoryValue.Text);
                        MessageBox.Show("Sub Category has been updated.");
                        btnCategoryAdd.Enabled = true;
                        btnCategoryDelete.Enabled = false;
                        btnCategoryUpdate.Enabled = false;
                        fillListView();
                        txtCategoryValue.Clear();
                    }
                }
                else {
                    MessageBox.Show("Update value can not be empty, please try again.");
                    btnCategoryAdd.Enabled = true;
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                }
               
            }
            catch (Exception)
            {

                MessageBox.Show("Please select an item to update");
            }
        }

        private void lvSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                btnCategoryDelete.Enabled = true;
                btnCategoryUpdate.Enabled = true;
                btnCategoryAdd.Enabled = false;
                index = lvSubCategory.SelectedIndices[0];
                txtCategoryValue.Text = lvSubCategory.Items[index].SubItems[0].Text;
            }
            catch (Exception ex)
            {
                index = 0;
            }
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                if (txtCategoryValue.Text != "")
                {
                    index = lvSubCategory.SelectedIndices[0];
                    SubCategoryRepo.delete(Guid.Parse(lvSubCategory.Items[index].SubItems[1].Text));
                    fillListView();
                    MessageBox.Show("Sub Category has been deleted.");
                    txtCategoryValue.Clear();
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
                }
                else {
                    MessageBox.Show("Delete Value can not be empty, please try again.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            fillListView();
            txtCategoryValue.Clear();
            txtCategorySearch.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtCategoryValue.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var subcat = SubCategoryRepo.retrieveByName(txtCategorySearch.Text);

                if (subcat.Count > 0) {
                    lvSubCategory.Items.Clear();
                    ListViewItem itm = new ListViewItem(subcat[0].SubCategoryValue);
                    itm.SubItems.Add(subcat[0].SubCategoryId.ToString());
                    lvSubCategory.Items.Add(itm);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) {
                this.cmbSubCategory.Items.Clear();
                var subcat = SubCategoryRepo.retrieve();
                if (subcat.Count > 0)
                {
                    for (int i = 0; i < subcat.Count; i++)
                    {
                        this.cmbSubCategory.Items.Add(subcat[i].SubCategoryValue.ToString());
                    }
                }
                this.Hide();
            }
        }
    }
}
