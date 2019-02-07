using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMLIB.Initializer;
using SMLIB.Repository;
using SMLIB.View;

namespace SM
{
    public partial class Cashier : Form
    {
        private List<vwUserDetail> userDetail;
        private string productName;

        static public string AssemblyDirectory
        {
            get
            {
                //Don't use Assembly.GetExecutingAssembly().Location, instead use the CodeBase property
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        static public string AssemblyVersion
        {
            get
            {
                var asm = Assembly.GetExecutingAssembly();
                //If you want the full four-part version number:
                return asm.GetName().Version.ToString(4);

                //You can reference asm.GetName().Version to get Major, Minor, MajorRevision, MinorRevision
                //components individually and do with them as you please.
            }
        }

        public Cashier()
        {
            InitializeComponent();

        }
        public Cashier(string productName)
        {
            InitializeComponent();
            this.productName = productName;         
        }

        public Cashier(List<vwUserDetail> userDetail)
        {
            InitializeComponent();

            this.userDetail = userDetail;
            initUserDetails();

        }

        private void initUserDetails()
        {
            txtSearch.Focus();
            if (this.userDetail.Count > 0)
            {
                lblCashierName.Text = this.userDetail[0].UserFirstName + " " + this.userDetail[0].UserLastName;
                lblUserId.Text = this.userDetail[0].UserDetailsId.ToString();
            }

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        private void search(string productName)
        {
            try
            {
                bool isOutOfStock = ProductRepo.checkProductQuantity(productName);

                if (isOutOfStock)
                {
                    MessageBox.Show("This product is out of stock.");
                    txtSearch.Clear();
                }
                else
                {
                    var product = ProductRepo.retrieve(productName);

                    if (product.Count > 0)
                    {
                        lblDescription.Text = product[0].ProductDescription;
                        lblQuantity.Text = product[0].ProductQuantity.ToString();
                        lblBarcode.Text = product[0].ProductBarcode.ToString();
                        lblCategory.Text = product[0].ProductCategories;

                        string productImagePath =AssemblyDirectory + @"\Image\ProductImages\" + product[0].ProductImage;
                        string barcodeImagePath = AssemblyDirectory + @"\Image\BarcodeImages\" +  product[0].ProductBarcodeImage;

                        if (File.Exists(productImagePath)) {
                            pbProductImage.Image = Image.FromFile(productImagePath);
                        }

                        if (File.Exists(barcodeImagePath))
                        {
                            pbProductBarcode.Image = Image.FromFile(barcodeImagePath);
                        }                        

                        lblName.Text = product[0].ProductName.ToString();
                        lblQuantity.Text = product[0].ProductQuantity.ToString();
                        lblSKU.Text = product[0].ProductSku.ToString();
                        lblSubCategory.Text = product[0].ProductSubCategories;
                        lblUnitCost.Text = product[0].ProductUnitCost.ToString();
                        lblMeasurement.Text = product[0].ProductUnitOfMeasurement;

                        lblAttribute.Text = "";
                        foreach (var item in product[0].ProductAttributes)
                        {
                            lblAttribute.Text = lblAttribute.Text + item.AttributeName + ": " + item.AttributeValue + "\n";
                        }//shows product attributes

                        bool productExistOnList = false;
                        if (lvProduct.Items.Count > 0)
                        {
                            for (int i = 0; i < lvProduct.Items.Count; i++)//loops through each product on the list
                            {
                                var name = lvProduct.Items[i].SubItems[0].Text;
                                var desc = lvProduct.Items[i].SubItems[1].Text;
                                var sku = lvProduct.Items[i].SubItems[6].Text;
                                var quantity = lvProduct.Items[i].SubItems[3].Text;
                                var cost = lvProduct.Items[i].SubItems[2].Text;

                                if (name == product[0].ProductName && desc == product[0].ProductDescription && sku == product[0].ProductSku)//if product is found update quantity
                                {

                                    DialogResult result = MessageBox.Show("Product with name " + product[0].ProductName + " already exist on the list, \n do you wish to update quantity?", "Product Exist on the List", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        productExistOnList = true;
                                        lblTotal.Text = "0.00";
                                        UpdateQuantity multiplyQuantity = new UpdateQuantity(Double.Parse(quantity), Double.Parse(cost), sku, i, lvProduct, lblTotal, txtSearch);
                                        multiplyQuantity.Show();
                                    }
                                    else
                                    {
                                        productExistOnList = true;
                                    }

                                }

                            } //end of for loop

                            if (productExistOnList == false)
                            {

                                var quantity1 = 1;
                                var unitCost = product[0].ProductUnitCost;
                                var subTotal = quantity1 * unitCost;

                                ListViewItem item1 = new ListViewItem(product[0].ProductName);
                                item1.SubItems.Add(product[0].ProductDescription);
                                item1.SubItems.Add((unitCost).ToString());
                                item1.SubItems.Add((quantity1).ToString());
                                item1.SubItems.Add(product[0].ProductUnitOfMeasurement);
                                item1.SubItems.Add((subTotal).ToString());
                                item1.SubItems.Add((product[0].ProductSku).ToString());
                                item1.SubItems.Add((product[0].ProductBarcode).ToString());
                                item1.SubItems.Add((product[0].ProductCategories));
                                item1.SubItems.Add((product[0].ProductSubCategories).ToString());
                                item1.SubItems.Add((product[0].ProductAttributes).ToString());
                                item1.SubItems.Add((product[0].ProductImage).ToString());
                                item1.SubItems.Add((product[0].ProductBarcodeImage).ToString());
                                item1.SubItems.Add(product[0].ProductId.ToString());
                                item1.SubItems.Add(product[0].ProductQuantity.ToString());

                                lvProduct.Items.Add(item1);
                                lblTotal.Text = "0.00";
                                int count = lvProduct.Items.Count;

                                for (int indexTotal = 0; indexTotal < count; indexTotal++)//updates subTotal
                                {

                                    lblTotal.Text = (double.Parse(lblTotal.Text) + double.Parse(lvProduct.Items[indexTotal].SubItems[5].Text)).ToString();
                                }
                                txtSearch.Clear();
                            }
                            txtSearch.Clear();
                        }
                        else
                        {
                            var quantity = 1;
                            var unitCost = product[0].ProductUnitCost;
                            var subTotal = quantity * unitCost;

                            ListViewItem item1 = new ListViewItem(product[0].ProductName);
                            item1.SubItems.Add(product[0].ProductDescription);
                            item1.SubItems.Add((unitCost).ToString());
                            item1.SubItems.Add((quantity).ToString());
                            item1.SubItems.Add(product[0].ProductUnitOfMeasurement);
                            item1.SubItems.Add((subTotal).ToString());
                            item1.SubItems.Add((product[0].ProductSku).ToString());
                            item1.SubItems.Add((product[0].ProductBarcode).ToString());
                            item1.SubItems.Add((product[0].ProductCategories));
                            item1.SubItems.Add((product[0].ProductSubCategories).ToString());
                            item1.SubItems.Add((product[0].ProductAttributes).ToString());
                            item1.SubItems.Add((product[0].ProductImage).ToString());
                            item1.SubItems.Add((product[0].ProductBarcodeImage).ToString());
                            item1.SubItems.Add(product[0].ProductId.ToString());
                            item1.SubItems.Add(product[0].ProductQuantity.ToString());

                            lvProduct.Items.Add(item1);
                            lblTotal.Text = "0.00";
                            int count = lvProduct.Items.Count;

                            for (int indexTotal = 0; indexTotal < count; indexTotal++)//updates subTotal
                            {

                                lblTotal.Text = (double.Parse(lblTotal.Text) + double.Parse(lvProduct.Items[indexTotal].SubItems[5].Text)).ToString();
                            }
                            txtSearch.Clear();
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (lvProduct.Items.Count > 0)
            {
                SettlePayment settlePayment = new SettlePayment(lvProduct, lblUserId, Guid.NewGuid(), lblTotal, txtSearch, lblCashierName.Text);
                settlePayment.Show();
                
            }
            else
            {
                MessageBox.Show("No items on the list.");
            }

        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            int index;
            try
            {
                bool b = int.TryParse(lvProduct.SelectedIndices[0].ToString(), out index);

                if (b)
                {
                    var cost = lvProduct.Items[index].SubItems[2].Text;
                    var quantity = lvProduct.Items[index].SubItems[3].Text;
                    var sku = lvProduct.Items[index].SubItems[5].Text;

                    UpdateQuantity updateQuantity = new UpdateQuantity(Double.Parse(quantity), Double.Parse(cost), sku, index, lvProduct, lblTotal, txtSearch);
                    updateQuantity.Show();

                }

            }

            catch (Exception)
            {
                index = 0;
                MessageBox.Show("Please select an item to update.");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Clearing the list means that you have to barcode all the products again. \n Are you sure you want to clear?", "Clear List", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                lvProduct.Items.Clear();
                lblTotal.Text = "0.00";
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        private void lvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            try
            {
                bool b = int.TryParse(lvProduct.SelectedIndices[0].ToString(), out index);

                if (b)
                {
                    lblName.Text = lvProduct.Items[index].SubItems[0].Text;
                    lblDescription.Text = lvProduct.Items[index].SubItems[1].Text;
                    lblUnitCost.Text = lvProduct.Items[index].SubItems[2].Text;

                    lblSKU.Text = lvProduct.Items[index].SubItems[6].Text;
                    lblBarcode.Text = lvProduct.Items[index].SubItems[7].Text;
                    lblCategory.Text = lvProduct.Items[index].SubItems[8].Text;
                    lblSubCategory.Text = lvProduct.Items[index].SubItems[9].Text;
                    lblQuantity.Text = lvProduct.Items[index].SubItems[14].Text;
                    //var attrib = lvProduct.Items[index].SubItems[9].Text;
                    //lblAttribute.Text = "";
                    pbProductImage.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Image\ProductImages\" + lvProduct.Items[index].SubItems[10].Text);
                    pbProductBarcode.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Image\BarcodeImages\" + lvProduct.Items[index].SubItems[11].Text);
                }
            }
            catch (Exception)
            {
                index = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Transaction transaction = new Transaction(this.userDetail[0].UserRole);
                transaction.Show();
            }
            catch (Exception)
            {
                Transaction transaction = new Transaction("cashier");
                transaction.Show();
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvProduct.SelectedIndices[0];

                lvProduct.Items.RemoveAt(index);

                lblTotal.Text = "0.00";
                int count = lvProduct.Items.Count;

                for (int indexTotal = 0; indexTotal < count; indexTotal++)//updates subTotal
                {

                    lblTotal.Text = (double.Parse(lblTotal.Text) + double.Parse(lvProduct.Items[indexTotal].SubItems[4].Text)).ToString();
                }

            }
            catch (Exception)
            {
                index = 0;
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = 0;

            index = lvProduct.SelectedIndices[0];
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement(lvProduct.Items[index].SubItems[4].Text, Double.Parse(lvProduct.Items[index].SubItems[2].Text), Double.Parse(lvProduct.Items[index].SubItems[3].Text), lvProduct, index);
            unitOfMeasurement.Show();

            //try
            //{
                           

            //}
            //catch (Exception)
            //{
            //    index = 0;
            //    MessageBox.Show("Please select an item to edit.");
            //}

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
                this.Dispose();
            }
        }

        private void Cashier_Load(object sender, EventArgs e)
        {
            try
            {
                string productImagePath = AssemblyDirectory+ @"\Image\ProductImages";
                string barcodeImagePath = AssemblyDirectory + @"\Image\BarcodeImages";
                string siteImagePath = AssemblyDirectory + @"\Image\SiteImages";

                if (!Directory.Exists(productImagePath)) {
                    Directory.CreateDirectory(productImagePath);
                }

                if (!Directory.Exists(barcodeImagePath))
                {
                    Directory.CreateDirectory(barcodeImagePath);
                }

                if (!Directory.Exists(barcodeImagePath))
                {
                    Directory.CreateDirectory(siteImagePath);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void label37_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label37.Text,txtSearch);
            sbi.Show();
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label19_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label19.Text, txtSearch);
            sbi.Show();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label20.Text, txtSearch);
            sbi.Show();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label21.Text, txtSearch);
            sbi.Show();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label22.Text, txtSearch);
            sbi.Show();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label23.Text, txtSearch);
            sbi.Show();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label24.Text, txtSearch);
            sbi.Show();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label25.Text, txtSearch);
            sbi.Show();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label26.Text, txtSearch);
            sbi.Show();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label27.Text, txtSearch);
            sbi.Show();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label28.Text, txtSearch);
            sbi.Show();
        }

        private void label29_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label29.Text, txtSearch);
            sbi.Show();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label30.Text, txtSearch);
            sbi.Show();
        }

        private void label31_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label31.Text, txtSearch);
            sbi.Show();
        }

        private void label32_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label32.Text, txtSearch);
            sbi.Show();
        }

        private void label33_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label33.Text, txtSearch);
            sbi.Show();
        }

        private void label34_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label34.Text, txtSearch);
            sbi.Show();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label35.Text, txtSearch);
            sbi.Show();
        }

        private void label36_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label36.Text, txtSearch);
            sbi.Show();
        }

        private void label38_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label38.Text, txtSearch);
            sbi.Show();
        }

        private void label39_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label39.Text, txtSearch);
            sbi.Show();
        }

        private void label40_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label40.Text, txtSearch);
            sbi.Show();
        }

        private void label41_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label41.Text, txtSearch);
            sbi.Show();
        }

        private void label42_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label42.Text, txtSearch);
            sbi.Show();
        }

        private void label43_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label43.Text, txtSearch);
            sbi.Show();
        }

        private void label44_Click(object sender, EventArgs e)
        {
            SearchByInitialLetter sbi = new SearchByInitialLetter(label44.Text, txtSearch);
            sbi.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) {
                Login l = new Login();
                l.Show();
                this.Hide();
            }
        }
    }
}
