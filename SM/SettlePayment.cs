using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMLIB.Entity;
using SMLIB.Repository;

namespace SM
{
    public partial class SettlePayment : Form
    {
        PrintDocument pdoc = null;
        PrintDialog pd = null;
        private ListView lv;
        private Label userId;
        private Guid transactionId;
        private Label total;
        private TextBox txtSearch;
        private string cashier;

        public SettlePayment()
        {
            InitializeComponent();
        }

        public SettlePayment(ListView lv, Label UserId, Guid transactionId, Label total, TextBox txtSearch, string cashier)
        {
            InitializeComponent();
            this.lv = lv;
            this.userId = UserId;
            this.transactionId = transactionId;
            initSubTotal();
            txtChange.Enabled = false;
            btnPay.Enabled = false;
            lblTransactionNumber.Text = this.transactionId.ToString();
            this.total = total;
            this.txtSearch = txtSearch;
            this.cashier = cashier;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                this.Hide();
            }

        }

        private void printReceipt()
        {
            pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Calibri", 12);
            PaperSize psize = new PaperSize("Custom", 100, 200);
            ps.DefaultPageSettings.PaperSize = psize;
            pd.Document = pdoc;

            //pd.Document.DefaultPageSettings.PaperSize = psize;
            //pd.PrinterSettings.PrinterName = "XP-58(copy 1)";
            //pd.PrinterSettings.PrintRange = PrintRange.AllPages;
            //pd.PrinterSettings.Copies = 1;
            //pd.AllowPrintToFile = false;
            //pd.UseEXDialog = true;         

            pdoc.DefaultPageSettings.PaperSize.Height = 820;
            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            //pdoc.Print();

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                pdoc.Print();
                //PrintPreviewDialog pp = new PrintPreviewDialog();
                //pp.Document = pdoc;
                //result = pp.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //    pdoc.Print();
                //}
            }
        }

        public void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Calibri", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 0;
            int Offset = 0;
            graphics.DrawString("South Mall Inc.", new Font("Calibri", 12, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset += 20;
            graphics.DrawString("Pob. 3, Lebak, Sultan Kudarat:\nEDWIN ACHA - Prop. \nTIN: 222-222-222-2222 \n",
                     new Font("Calibri", 8, FontStyle.Regular),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset += 40;
            String underLine = "-------------------------------------------------------------------------------------";
            graphics.DrawString(underLine, new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 12;
            graphics.DrawString("DATE AND TIME:" + DateTime.Now.ToString(),
                     new Font("Calibri", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 12;
            graphics.DrawString("CASHIER:" + this.cashier,
                     new Font("Calibri", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 12;

            string[] tid = new string[5];

            tid = this.transactionId.ToString().Split('-');

            graphics.DrawString("TRANSACTION NO: \n" + tid[0].ToString() + "-" + tid[1].ToString() + "-" + tid[2].ToString() + "\n" + "-" + tid[3].ToString() + "-" + tid[4].ToString(),
                     new Font("Calibri", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 35;
            graphics.DrawString(underLine, new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("ITEM \t QTY \t PRICE \t SUB-TOTAL",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString(underLine, new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            int itemCount = this.lv.Items.Count;

            for (int i = 0; i < itemCount; i++)
            {
                Offset = Offset + 10;
                graphics.DrawString(this.lv.Items[i].SubItems[0].Text,
                     new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(
                    ("\t" + this.lv.Items[i].SubItems[3].Text).ToString() + "\t"
                   + (this.lv.Items[i].SubItems[2].Text).ToString() + "\t"
                   + (this.lv.Items[i].SubItems[5].Text).ToString(),
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            }
            Offset = Offset + 10;
            //FOR UNDERLINE
            graphics.DrawString(underLine, new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("TOTAL: \t\t\t" + this.total.Text.ToString(),
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString("AMOUNT RECEIVED:\t" + txtAmountTendered.Text,
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("CHANGE:\t\t\t" + txtChange.Text,
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("VAT Sales\t\t" + this.total.Text,
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("VAT(12%)\t\t 0.00",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("VAT Exempt Sales\t\t 0.00",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;

            int quantity = 0;
            int count = this.lv.Items.Count;
            for (int i = 0; i < count; i++)
            {
                quantity += Convert.ToInt32(this.lv.Items[i].SubItems[3].Text);
            }
            graphics.DrawString("ITEMS:\t\t\t" + quantity.ToString(),
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            //FOR UNDERLINE
            graphics.DrawString(underLine, new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);


            Offset = Offset + 10;
            graphics.DrawString("POS SOFTWARE PROVIDER:",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("BACHA SOFTWARE SOLUTIONS",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Poblacion I, Lebak, Sultan Kudarat",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Contact #: 09102718746",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("TIN: 278-081-323",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("ACCR. #: 1234123412341234123",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("PERMIT #: FPJ12341234-KARDO-1234",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);


            Offset = Offset + 20;
            graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID \n FOR FIVE YEARS FROM THE DATE OF \n THE PERMIT TO USE.",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 50;
            graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("THANK YOU COME AGAIN!",
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            graphics.DrawString(underLine,
                    new Font("Calibri", 8),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            //FOR UNDERLINE
            graphics.DrawString(underLine, new Font("Calibri", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lv.Items.Count > 0)
                {
                    Guid OrderOwnerId = Guid.NewGuid();
                    Guid cashierId = Guid.Parse(this.userId.Text);
                    Guid customerId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d766");

                    for (int i = 0; i < this.lv.Items.Count; i++)
                    {
                        Guid OrderId = Guid.NewGuid();
                        double originalQuantity = double.Parse(this.lv.Items[i].SubItems[14].Text);
                        double listQuantity = double.Parse(this.lv.Items[i].SubItems[3].Text);
                        string sku = this.lv.Items[i].SubItems[6].Text;
                        Guid productId = Guid.Parse(this.lv.Items[i].SubItems[13].Text);
                        //Update the quantity in the database
                        ProductRepo.updateQuantityBySku((originalQuantity - listQuantity), sku);
                        //Records the orders
                        OrderRepo.insertOrder(OrderId, this.transactionId, customerId, productId, listQuantity);
                    }
                    //Records the transactions
                    TransactionRepo.insertTransaction(this.transactionId, cashierId, Double.Parse(txtAmountTendered.Text), Double.Parse(txtChange.Text), Double.Parse(txtSubTotal.Text), customerId);
                    //print receipt    
                    printReceipt();
                    
                    txtAmountTendered.Clear();
                    txtChange.Clear();
                    txtSubTotal.Clear();

                    this.txtSearch.Focus();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void initSubTotal()
        {
            Double subTotal = 0;

            if (this.lv.Items.Count > 0)
            {
                for (int i = 0; i < this.lv.Items.Count; i++)
                {
                    subTotal += Double.Parse(this.lv.Items[i].SubItems[5].Text);
                }
                txtSubTotal.Text = subTotal.ToString();
            }

        }
        private void txtAmountTendered_TextChanged(object sender, EventArgs e)
        {
            Double amountTendered;
            Double subTotal = Double.Parse(txtSubTotal.Text);
            Boolean isDouble = Double.TryParse(txtAmountTendered.Text, out amountTendered);

            if (isDouble && (amountTendered != 0 && amountTendered > 0))
            {
                if (amountTendered > subTotal)
                {
                    txtChange.Text = (amountTendered - subTotal).ToString();
                    lblWarning.Text = "";
                    btnPay.Enabled = true;
                    btnPay.Focus();

                }
                else if (amountTendered == subTotal)
                {
                    txtChange.Text = "0";
                    btnPay.Enabled = true;
                    btnPay.Focus();
                }
                else
                {
                    txtChange.Clear();
                    btnPay.Enabled = false;
                    lblWarning.Text = "Amount should be greater than sub total.";
                }
            }

            else
            {
                lblWarning.Text = "Your input should be a number greater than 0.";
                btnPay.Enabled = false;
            }


        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.txtSearch.Focus();

                this.Hide();
            }
        }
    }
}
