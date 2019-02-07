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
    public partial class AddCategory : Form
    {
        private ComboBox cmbCategory;

        public AddCategory()
        {
            InitializeComponent();
            initCategory();
        }

        public AddCategory(ComboBox cmbCategory)
        {
            InitializeComponent();
            initCategory();
            this.cmbCategory = cmbCategory;
        }

        private void initCategory()
        {
            lvCategory.Items.Clear();
            var category = CategoryRepo.retrieve();
            if (category.Count > 0)
            {
                for (int i = 0; i < category.Count; i++)
                {

                    ListViewItem item = new ListViewItem(category[i].CategoryValue);
                    item.SubItems.Add(category[i].CategoryId.ToString());

                    lvCategory.Items.Add(item);
                }
            }
            txtCategorySearch.Clear();
            txtCategoryValue.Clear();
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            if (txtCategoryValue.Text != "")
            {
                bool existing = CategoryRepo.checkIfCategoryExists(txtCategoryValue.Text);
                if (existing)
                {
                    MessageBox.Show("This category already exists.");
                }
                else
                {
                    CategoryRepo.create(txtCategoryValue.Text);
                    MessageBox.Show("Category has been created.");
                    initCategory();
                    txtCategoryValue.Clear();
                }
            }
            else {
                MessageBox.Show("Category value can not be empty, please try again.");
            }

        }

        private void AddCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                this.Hide();
            }
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                if (txtCategoryValue.Text != "")
                {
                    bool existing = CategoryRepo.checkIfCategoryExists(txtCategoryValue.Text);
                    if (existing)
                    {
                        MessageBox.Show("This category already exists, please give a new name");
                    }
                    else
                    {
                        index = lvCategory.SelectedIndices[0];
                        CategoryRepo.update(Guid.Parse(lvCategory.Items[index].SubItems[1].Text), txtCategoryValue.Text);
                        initCategory();
                        txtCategoryValue.Clear();
                        MessageBox.Show("Category has been updated.");
                        btnCategoryDelete.Enabled = false;
                        btnCategoryUpdate.Enabled = false;
                        btnCategoryAdd.Enabled = true;
                        txtCategoryValue.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Update value can not be empty, please try again.");
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
                }




            }
            catch (Exception)
            {
                txtCategoryValue.Clear();
                index = 0;
                MessageBox.Show("Please select an item to update.");
            }

        }

        private void lvCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                btnCategoryAdd.Enabled = false;
                btnCategoryDelete.Enabled = true;
                btnCategoryUpdate.Enabled = true;
                index = lvCategory.SelectedIndices[0];
                txtCategoryValue.Text = lvCategory.Items[index].SubItems[0].Text;
            }
            catch (Exception)
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
                    index = lvCategory.SelectedIndices[0];
                    bool b = CategoryRepo.checkIfCategoryIsUsed(Guid.Parse(lvCategory.Items[index].SubItems[1].Text));

                    if (b)
                    {
                        DialogResult d = MessageBox.Show("This category is being used by product(s), \n Do you wish to proceed?", "Delete", MessageBoxButtons.YesNo);
                        if (d == DialogResult.Yes)
                        {
                            CategoryRepo.delete(Guid.Parse(lvCategory.Items[index].SubItems[1].Text));
                            btnCategoryDelete.Enabled = false;
                            btnCategoryUpdate.Enabled = false;
                            btnCategoryAdd.Enabled = true;
                            txtCategoryValue.Clear();
                            MessageBox.Show("Category deleted.");
                            
                            initCategory();
                        }
                    }
                    else
                    {
                        CategoryRepo.delete(Guid.Parse(lvCategory.Items[index].SubItems[1].Text));
                        btnCategoryDelete.Enabled = false;
                        btnCategoryUpdate.Enabled = false;
                        btnCategoryAdd.Enabled = true;
                        txtCategoryValue.Clear();
                        MessageBox.Show("Category deleted.");
                        initCategory();
                    }
                }
                else
                {
                    MessageBox.Show("Delete value can not be empty, please try again.");
                    btnCategoryDelete.Enabled = false;
                    btnCategoryUpdate.Enabled = false;
                    btnCategoryAdd.Enabled = true;
                }

            }
            catch (Exception)
            {

                index = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtCategoryValue.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initCategory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var category = CategoryRepo.retrieveCategoryByName(txtCategorySearch.Text);

            if (category.Count > 0)
            {
                lvCategory.Items.Clear();
                ListViewItem item = new ListViewItem(category[0].CategoryValue);
                item.SubItems.Add(category[0].CategoryId.ToString());

                lvCategory.Items.Add(item);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                var category = CategoryRepo.retrieve();
                if (this.cmbCategory != null) {
                    this.cmbCategory.Items.Clear();
                    foreach (var itm in category)
                    {
                        this.cmbCategory.Items.Add(itm.CategoryValue);
                    }
                }                                          

                this.Hide();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
