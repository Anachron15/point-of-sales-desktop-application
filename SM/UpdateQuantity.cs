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
    public partial class UpdateQuantity : Form
    {
        public Double quantity { get; set; }
        public String sku { get; set; }
        public int rowIndex { get; set; }
        public Double unitCost { get; set; }
        public ListView lv { get; set; }
        private Label total;
        private TextBox txtSearch { get; set; }

        public UpdateQuantity()
        {
            InitializeComponent();
            txtQuantity.Focus();
        }

        public UpdateQuantity(Double quantity, Double unitCost, String SKU, int rowIndex, ListView lv,Label total,TextBox txtSearch)
        {
            InitializeComponent();
            this.txtSearch = txtSearch;
            this.quantity = quantity;
            this.sku = SKU;
            this.rowIndex = rowIndex;
            this.unitCost = unitCost;
            this.lv = lv;
            this.total = total;
            txtQuantity.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to cancel?", "Cancel", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {                
                this.Hide();
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            updateQuantity();
        }

        private void updateQuantity() {
            double price;
            bool isDouble = Double.TryParse(txtQuantity.Text, out price);

            if (isDouble && (price != 0 && price > 0))
            {
                if (this.lv.Items.Count > 0)
                {
                    //check if quantity exceeds the product quantity on the database
                    bool b = ProductRepo.checkIfQuantityExceeds(Double.Parse(txtQuantity.Text), this.sku);

                    if (b)
                    {
                        lblWarning.Text = "The quantity you entered \n exceeds the quantity in stock.";
                    }
                    else
                    {
                        this.lv.Items[this.rowIndex].SubItems[3].Text = txtQuantity.Text;
                        var subTotal = this.unitCost * Double.Parse(txtQuantity.Text);
                        this.lv.Items[this.rowIndex].SubItems[5].Text = subTotal.ToString();

                        var count = this.lv.Items.Count;
                        this.total.Text = "0.00";

                        for (int i = 0; i < count; i++) {
                            double sub = double.Parse(this.lv.Items[i].SubItems[5].Text);
                            this.total.Text = (double.Parse(this.total.Text) + sub).ToString();
                        }
                        MessageBox.Show("Quantiy has been successfully updated.");
                        
                        this.Hide();
                    }
                }
            }
            else
            {
                lblWarning.Text = "Your input needs to be a number greater than 0.";
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            //updateQuantity();
        }

      

        private void UpdateQuantity_Load(object sender, EventArgs e)
        {
              txtQuantity.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                this.txtSearch.Focus();
                
                this.Hide();
            }
        }

        private void UpdateQuantity_Load_1(object sender, EventArgs e)
        {
            txtQuantity.Focus();
        }
    }
}
