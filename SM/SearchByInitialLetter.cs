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
    public partial class SearchByInitialLetter : Form
    {
        private string letter;
        private TextBox txtSearch;

        public SearchByInitialLetter()
        {
            InitializeComponent();
        }
        public SearchByInitialLetter(string letter, TextBox txtSearch)
        {
            InitializeComponent();
            this.letter = letter;
            this.txtSearch = txtSearch;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void SearchByInitialLetter_Load(object sender, EventArgs e)
        {
            try
            {
                var product = ProductRepo.retrieveByInitialLetter(this.letter);
                if (product.Count > 0)
                {
                    for (int i = 0; i < product.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem(product[i].ProductId.ToString());
                        lvi.SubItems.Add(product[i].ProductName);
                        lvi.SubItems.Add(product[i].ProductDescription);
                        lvi.SubItems.Add(product[i].ProductUnitCost.ToString());
                        lvi.SubItems.Add(product[i].ProductQuantity.ToString());
                        lvi.SubItems.Add(product[i].ProductUnitOfMeasurement.ToString());
                        lvProduct.Items.Add(lvi);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvProduct.SelectedIndices[0];
                string productName = (lvProduct.Items[index].SubItems[1].Text);
                this.txtSearch.Text = productName;
                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select an item to add on the list.");
            }
        }
    }
}
