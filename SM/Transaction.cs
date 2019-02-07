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
    public partial class Transaction : Form
    {
        private string role;

        public Transaction()
        {
            InitializeComponent();
        }
        public Transaction(string role)
        {
            InitializeComponent();
            this.role = role;
            initTransaction();
        }

        private void initTransaction()
        {
            var transaction = TransactionRepo.retrieveTransactionToday(DateTime.Now.ToString("d"));

            if (transaction.Count > 0)
            {
                for (int i = 0; i < transaction.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(transaction[i].TransactionId.ToString());
                    listViewItem.SubItems.Add(transaction[i].TransactionAmountReceived.ToString());
                    listViewItem.SubItems.Add(transaction[i].TransactionAmountReturned.ToString());
                    listViewItem.SubItems.Add(transaction[i].TransactionNewAmountReturned.ToString());
                    listViewItem.SubItems.Add(transaction[i].TransactionSubTotal.ToString());
                    listViewItem.SubItems.Add(transaction[i].TransactionDate);
                    listViewItem.SubItems.Add(transaction[i].TransactionTime);
                    listViewItem.SubItems.Add(transaction[i].Transaction_Status);
                    listViewItem.SubItems.Add(transaction[i].Transaction_Remarks);
                    listViewItem.SubItems.Add(transaction[i].TransactionCustomer);
                    listViewItem.SubItems.Add(transaction[i].TransactionCashier);
                    listViewItem.SubItems.Add(transaction[i].TransactionDateModified);
                    listViewItem.SubItems.Add(transaction[i].TransactionTimeModified);

                    lvTransaction.Items.Add(listViewItem);
                }

            }



        }

        private void pbDate_Click(object sender, EventArgs e)
        {
            calTransactionDate.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Hide();
            }
        }
        private void enableButtons()
        {
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void disableButtons()
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void Transaction_Load(object sender, EventArgs e)
        {
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnCancelTransaction.Enabled = false;
          
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to cancel the transaction?", "Cancel Transaction", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

            }
        }

        private void lvTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCancelTransaction.Enabled = true;

            int index = 0;
            try
            {
                index = lvTransaction.SelectedIndices[0];

                lblTransactionIndex.Text = index.ToString();

                Guid transactionId = Guid.Parse(lvTransaction.Items[index].SubItems[0].Text);

                var products = ProductRepo.retrieveProductsByTransactionId(transactionId);

                ListViewItem items;

                if (products.Count > 0)
                {
                    lvProducts.Items.Clear();
                    foreach (var item in products)
                    {
                        double unitCost = item.ProductUnitCost;
                        double orderQuantity = item.Order_Quantity;
                        double subTotal = unitCost * orderQuantity;

                        items = new ListViewItem(item.ProductName);
                        items.SubItems.Add(item.ProductDescription);
                        items.SubItems.Add(unitCost.ToString());
                        items.SubItems.Add(orderQuantity.ToString());
                        items.SubItems.Add(subTotal.ToString());
                        items.SubItems.Add(item.ProductSku);
                        items.SubItems.Add(transactionId.ToString());
                        items.SubItems.Add(item.ProductId.ToString());
                        lvProducts.Items.Add(items);
                    }
                }


            }
            catch (Exception )
            {
                index = 0;
            }
        }

        private void deleteProduct() {
            int index = 0;
            int transactionIndex = 0;
            try
            {

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove the product on the list?", "Remove", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    transactionIndex = int.Parse(lblTransactionIndex.Text);
                    index = lvProducts.SelectedIndices[0];
                    //update quantity
                    var sku = lvProducts.Items[index].SubItems[5].Text;
                    var listQuantity = lvProducts.Items[index].SubItems[3].Text;
                    var quantity = ProductRepo.getQuantityBySku(sku);

                    //adds the quantity of the product because the customer decides to remove the product in his orders
                    ProductRepo.updateQuantityBySku((quantity + Double.Parse(listQuantity)), sku);
                    //sets the new amount to be returned to the customer according to the products sub total       
                    lblTotal.Text = lvProducts.Items[index].SubItems[4].Text;
                    //total income minus sub Total
                    double totalIncome = Double.Parse(lvTransaction.Items[transactionIndex].SubItems[4].Text);
                    double newTotal = totalIncome - Double.Parse(lblTotal.Text);

                    double newChange = Double.Parse(lvTransaction.Items[transactionIndex].SubItems[3].Text) + Double.Parse(lvProducts.Items[index].SubItems[4].Text);
                    //sets the new amount returned to the customer
                    lvTransaction.Items[transactionIndex].SubItems[3].Text = newChange.ToString();
                    //sets the total income deducted by subtotal
                    lvTransaction.Items[transactionIndex].SubItems[4].Text = newTotal.ToString();


                    Guid transactionId = Guid.Parse(lvProducts.Items[index].SubItems[6].Text);
                    Guid productId = Guid.Parse(lvProducts.Items[index].SubItems[7].Text);

                    //update order quantity
                    OrderRepo.updateOrderQuantity(transactionId, productId, 0);
                    lvTransaction.Items[transactionIndex].SubItems[7].Text = "Modified";
                    lvTransaction.Items[transactionIndex].SubItems[8].Text = "Customer wished to remove the product(s).";

                    double amountReceived = Double.Parse(lvTransaction.Items[transactionIndex].SubItems[1].Text);
                    double amountReturned = Double.Parse(lvTransaction.Items[transactionIndex].SubItems[2].Text);
                    double newAmountReturned = Double.Parse(lvTransaction.Items[transactionIndex].SubItems[3].Text);
                    double subTotal = Double.Parse(lvTransaction.Items[transactionIndex].SubItems[4].Text);
                    
                    string status = lvTransaction.Items[transactionIndex].SubItems[7].Text;
                    string remarks = lvTransaction.Items[transactionIndex].SubItems[8].Text;
                    //update transaction
                    TransactionRepo.updateTransaction(transactionId, newAmountReturned, subTotal, remarks, status);
                    MessageBox.Show("Product has been successfully removed.");
                    lvProducts.Items.RemoveAt(index);
                    lblTotal.Text = lvTransaction.Items[transactionIndex].SubItems[3].Text;
                    lvTransaction.Items.Clear();
                    initTransaction();
                    disableButtons();

                }
            }
            catch (Exception )
            {
                index = 0;
                transactionIndex = 0;
                lblWarning.Visible = true;
                lblWarning.Text = "Warning, you need to select an item on the list before deleting or updating.";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteProduct();
            btnCancelTransaction.Enabled = false;
        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            Cashier cashier = new Cashier();
            cashier.Show();
            this.Hide();
        }

        private void lvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productIndex = 0;
            try
            {
                productIndex = lvProducts.SelectedIndices[0];
                lblProductRowIndex.Text = productIndex.ToString();

                double quantity = Double.Parse(lvProducts.Items[productIndex].SubItems[3].Text);
                if (quantity == 0)
                {
                    MessageBox.Show("This product could no longer be edited.");
                }
                else
                {
                    enableButtons();
                }
            }
            catch (Exception)
            {
                productIndex = 0;
            }
        }

      

        private void btnUpdate_Click(object sender, EventArgs e)
        {          
            int productIndex = 0;
            try
            {
                productIndex = lvProducts.SelectedIndices[0];

                double productQuantity = Double.Parse(lvProducts.Items[productIndex].SubItems[3].Text);
                double unitCost = Double.Parse(lvProducts.Items[productIndex].SubItems[2].Text);
                string sku = lvProducts.Items[productIndex].SubItems[5].Text;

                UpdateTransactionQuantity updateTransactionQuantity = new UpdateTransactionQuantity(productQuantity, lvProducts, lvTransaction, sku, unitCost, lblTransactionIndex,lblTotal,lblProductRowIndex);
                updateTransactionQuantity.Show();
                lvTransaction.Items.Clear();
                initTransaction();
                disableButtons();
                btnCancelTransaction.Enabled = false;
            }
            catch (Exception)
            {
                productIndex = 0;
            }
        }

        private void calTransactionDate_DateSelected(object sender, DateRangeEventArgs e)
        {

           
            calTransactionDate.Visible = false;
            
        }

        private void btnCancelTransaction_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Hide();
            }
        }
    }
}
