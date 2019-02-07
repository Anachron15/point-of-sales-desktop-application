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
    public partial class UpdateTransactionQuantity : Form
    {
        private double listQuantity;
        private ListView lvProduct;
        private ListView lvTransaction;
        private string sku;
        private double unitCost;
        private Label transactionRowId;
        private Label lblNewChange;
        private Label productRowIndex;

        public UpdateTransactionQuantity()
        {
            InitializeComponent();
        }

        public UpdateTransactionQuantity(double listQuantity,ListView lvProduct, ListView lvTransaction, string sku, double unitCost,Label transactionRowId,Label lblNewChange,Label productRowIndex)
        {
            InitializeComponent();
            this.listQuantity = listQuantity;
            this.lvProduct = lvProduct;
            this.lvTransaction = lvTransaction;
            this.sku = sku;
            this.unitCost = unitCost;        
            this.transactionRowId = transactionRowId;
            this.lblNewChange = lblNewChange;
            this.productRowIndex = productRowIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            double quantity;
            bool isDouble = Double.TryParse(txtQuantity.Text, out quantity);

           var sign = Math.Sign(quantity);
          

            if (isDouble && (quantity != 0 && sign.ToString() !="-1"))
            {
                if (this.listQuantity == quantity)
                {
                    lblWarning.Text = ("Please use the remove product button.");
                }
                else if (quantity > this.listQuantity)
                {
                    lblWarning.Text = ("If you wish to add product,\n please use the cashier panel.");
                }
                else {
                    double change = quantity * this.unitCost;
                    double newchange = double.Parse(this.lvTransaction.Items[int.Parse(this.transactionRowId.Text)].SubItems[3].Text) + change;

                    this.lvTransaction.Items[int.Parse(this.transactionRowId.Text)].SubItems[3].Text = newchange.ToString();//ok

                    this.lblNewChange.Text = newchange.ToString();

                    double totalIncome = double.Parse(this.lvTransaction.Items[int.Parse(this.transactionRowId.Text)].SubItems[4].Text);
                    double previousQuantity = double.Parse(this.lvProduct.Items[int.Parse(this.productRowIndex.Text)].SubItems[3].Text);
                    double newQuantity = previousQuantity - quantity;

                    double newAmountToReturn = quantity * this.unitCost;
                    double newTotalIncome = totalIncome - newAmountToReturn;//ok

                   

                    this.lvTransaction.Items[int.Parse(this.transactionRowId.Text)].SubItems[4].Text = newTotalIncome.ToString();

                    this.lvProduct.Items[int.Parse(this.productRowIndex.Text)].SubItems[3].Text = newQuantity.ToString();//ok

                    double newSubTotal = this.unitCost * newQuantity;

                    this.lvProduct.Items[int.Parse(this.productRowIndex.Text)].SubItems[4].Text = newSubTotal.ToString();

                    this.lvTransaction.Items[int.Parse(this.transactionRowId.Text)].SubItems[7].Text = "Modified";
                    this.lvTransaction.Items[int.Parse(this.transactionRowId.Text)].SubItems[8].Text = "Customer wished to deduct product quantity.";

                    Guid transactionId = Guid.Parse(this.lvProduct.Items[int.Parse(this.productRowIndex.Text)].SubItems[6].Text);
                    Guid productId = Guid.Parse(this.lvProduct.Items[int.Parse(this.productRowIndex.Text)].SubItems[7].Text);
                    //update order
                    OrderRepo.updateOrderQuantity(transactionId, productId, newQuantity);
                    //update transaction
                    TransactionRepo.updateTransaction(transactionId, newAmountToReturn, newTotalIncome, "Customer wants to reduce the quantity of the order.", "Modified");
                    MessageBox.Show("Update successful.");
                    this.Hide();
                }
            }
            else
            {
               lblWarning.Text = ("Invalid Input.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to cancel?", "Cancel", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                this.Hide();
            }
        }

        private void UpdateTransactionQuantity_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblWarning_Click(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) {
                this.Hide();
            }
        }
    }
}
