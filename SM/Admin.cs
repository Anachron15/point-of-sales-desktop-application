using SMLIB.View;
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
using System.Reflection;
using System.IO;

namespace SM
{
    public partial class Admin : Form
    {
        private List<vwUserDetail> vwUserDetails;
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
        public Admin()
        {
            InitializeComponent();
            initProduct();
        }

        public Admin(List<vwUserDetail> vwUserDetail)
        {
            InitializeComponent();
            this.vwUserDetails = vwUserDetail;
            initUser();
            initProduct();
        }

        private void initUser() {
            if (this.vwUserDetails.Count>0) {
                lblAdminName.Text = vwUserDetails[0].UserFirstName + " " + vwUserDetails[0].UserLastName;
            } 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                Login login = new Login();
                login.Show();
                this.Hide();
                this.Dispose();
                
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            try
            {
                string productImagePath = AssemblyDirectory + @"\Image\ProductImages";
                string barcodeImagePath = AssemblyDirectory + @"\Image\BarcodeImages";
                string copyOfImages = AssemblyDirectory + @"\Image\copyOfImages";
                string siteImagePath = AssemblyDirectory + @"\Image\SiteImages";

                if (!Directory.Exists(productImagePath))
                {
                    Directory.CreateDirectory(productImagePath);
                }

                if (!Directory.Exists(barcodeImagePath))
                {
                    Directory.CreateDirectory(barcodeImagePath);
                }

                if (!Directory.Exists(siteImagePath))
                {
                    Directory.CreateDirectory(siteImagePath);
                }

                if (!Directory.Exists(copyOfImages))
                {
                    Directory.CreateDirectory(copyOfImages);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<vwProduct> products = ProductRepo.retrieve(txtSearch.Text);
            fillListView(products);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct(lvProduct);
            addProduct.Show();
            this.Hide();
            
          
        }

        private void initProduct() {
            var products = ProductRepo.retrieveProductsAddedToday();
            if (products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    double unitCost = products[i].ProductUnitCost;
                    double quantity = products[i].ProductQuantity;
                    double subTotal = unitCost * quantity;
                    ListViewItem listViewItem = new ListViewItem( products[i].ProductImage);
                    listViewItem.SubItems.Add(products[i].ProductImage);
                    listViewItem.SubItems.Add(products[i].ProductId.ToString());                    
                    listViewItem.SubItems.Add(products[i].ProductName);
                    listViewItem.SubItems.Add(products[i].ProductDescription);
                    listViewItem.SubItems.Add(products[i].ProductUnitCost.ToString());
                    listViewItem.SubItems.Add(products[i].ProductQuantity.ToString());
                    listViewItem.SubItems.Add(subTotal.ToString());
                    listViewItem.SubItems.Add(products[i].ProductSku);
                    listViewItem.SubItems.Add(products[i].ProductBarcode);
                    listViewItem.SubItems.Add(products[i].ProductBarcodeImage);
                   
                    listViewItem.SubItems.Add(products[i].ProductVat.ToString());
                    listViewItem.SubItems.Add(products[i].ProductReplenishLimit.ToString());
                    listViewItem.SubItems.Add(products[i].ProductReorderLimit.ToString());
                    listViewItem.SubItems.Add(products[i].ProductDiscount.ToString());
                    listViewItem.SubItems.Add(products[i].ProductDateAdded);
                    listViewItem.SubItems.Add(products[i].ProductCategories);
                    listViewItem.SubItems.Add(products[i].ProductSubCategories);

                    listViewItem.SubItems.Add(products[i].ProductStatuses);
                    listViewItem.SubItems.Add(products[i].ProductSupplier);
                    listViewItem.SubItems.Add(products[i].ProductWarehouse);
                    listViewItem.SubItems.Add(products[i].ProductStore);
                   

                    var attib = products[i].ProductAttributes;
                    string items = "";
                    foreach (var item in attib) {
                        items += item.AttributeName + ":"+item.AttributeValue + ",";
                    }
                    listViewItem.SubItems.Add(items);
                    listViewItem.SubItems.Add(products[i].ProductUnitOfMeasurement);

                    lvProduct.Items.Add(listViewItem);


                }
            }
        }

      
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int index = 0;
            try
            {
                index = lvProduct.SelectedIndices[0];
                this.pbProductBarcode.Image.Dispose();
                this.pbProductImage.Image.Dispose();
                Guid id = Guid.Parse(lvProduct.Items[index].SubItems[2].Text);           
                AddProduct addProduct = new AddProduct(id);
                addProduct.Show();
                this.Hide();

            }
            catch (Exception)
            {
                index = 0;
                MessageBox.Show("Please select a product to edit.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                DialogResult d = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes) {
                    index = lvProduct.SelectedIndices[0];
                    Guid id = Guid.Parse(lvProduct.Items[index].SubItems[2].Text);
                    ProductRepo.delete(id);
                    MessageBox.Show("Product has been deleted.");
                    lvProduct.Items.Clear();
                    initProduct();
                }               
            }
            catch (Exception ex)
            {
                index = 0;
                MessageBox.Show(ex.Message);
            }
        }

        private void pbProductImage_Click(object sender, EventArgs e)
        {

        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                Transaction transaction = new Transaction("admin");
                transaction.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = new Warehouse();
            warehouse.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Store store = new Store();
            store.Show();
        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Hide();
                Login l = new Login();
                l.Show();
                
            }
        }

        private void label19_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Login l = new Login();
                l.Show();
                this.Hide();
            }
        }

        private void fillListView(List<vwProduct> products) {
            lvProduct.Items.Clear();
            if (products.Count > 0)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    double unitCost = products[i].ProductUnitCost;
                    double quantity = products[i].ProductQuantity;
                    double subTotal = unitCost * quantity;
                    ListViewItem listViewItem = new ListViewItem(products[i].ProductImage);
                    listViewItem.SubItems.Add(products[i].ProductImage);
                    listViewItem.SubItems.Add(products[i].ProductId.ToString());
                    listViewItem.SubItems.Add(products[i].ProductName);
                    listViewItem.SubItems.Add(products[i].ProductDescription);
                    listViewItem.SubItems.Add(products[i].ProductUnitCost.ToString());
                    listViewItem.SubItems.Add(products[i].ProductQuantity.ToString());
                    listViewItem.SubItems.Add(subTotal.ToString());
                    listViewItem.SubItems.Add(products[i].ProductSku);
                    listViewItem.SubItems.Add(products[i].ProductBarcode);
                    listViewItem.SubItems.Add(products[i].ProductBarcodeImage);

                    listViewItem.SubItems.Add(products[i].ProductVat.ToString());
                    listViewItem.SubItems.Add(products[i].ProductReplenishLimit.ToString());
                    listViewItem.SubItems.Add(products[i].ProductReorderLimit.ToString());
                    listViewItem.SubItems.Add(products[i].ProductDiscount.ToString());
                    listViewItem.SubItems.Add(products[i].ProductDateAdded);
                    listViewItem.SubItems.Add(products[i].ProductCategories);
                    listViewItem.SubItems.Add(products[i].ProductSubCategories);

                    listViewItem.SubItems.Add(products[i].ProductStatuses);
                    listViewItem.SubItems.Add(products[i].ProductSupplier);
                    listViewItem.SubItems.Add(products[i].ProductWarehouse);
                    listViewItem.SubItems.Add(products[i].ProductStore);

                    var attib = products[i].ProductAttributes;
                    string items = "";
                    foreach (var item in attib)
                    {
                        items += item.AttributeName + ":" + item.AttributeValue + ",";
                    }
                    listViewItem.SubItems.Add(items);
                    lvProduct.Items.Add(listViewItem);
                }
            }
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            Sales_Report sales = new Sales_Report();
            sales.Show();
        }

        private void lvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = lvProduct.SelectedIndices[0];
                pbProductImage.Image = Image.FromFile(AssemblyDirectory + @"\Image\ProductImages\" + lvProduct.Items[index].SubItems[1].Text);
                pbProductBarcode.Image = Image.FromFile(AssemblyDirectory + @"\Image\BarcodeImages\" + lvProduct.Items[index].SubItems[10].Text);
                lblBarcode.Text = lvProduct.Items[index].SubItems[9].Text;
                lblSKU.Text = lvProduct.Items[index].SubItems[8].Text;
                lblName.Text = lvProduct.Items[index].SubItems[3].Text;
                lblDescription.Text = lvProduct.Items[index].SubItems[4].Text;
                lblUnitCost.Text = lvProduct.Items[index].SubItems[5].Text;
                lblQuantity.Text = lvProduct.Items[index].SubItems[6].Text;
                lblMeasurement.Text = lvProduct.Items[index].SubItems[23].Text;
                lblCategory.Text = lvProduct.Items[index].SubItems[16].Text;
                lblSubCategory.Text = lvProduct.Items[index].SubItems[17].Text;
            }
            catch (Exception)
            {
                index = 0;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes) {
                Login L = new Login();
                L.Show();
                this.Hide();
            }
        }
    }
}
