using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMLIB.Entity;
using SMLIB.Repository;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace SM
{
    public partial class AddProduct : Form
    {
        private List<ProductAttribute> attributes = new List<ProductAttribute>();
        private Guid attributeId;
        private Guid CategoryId;
        private ListView lvProduct;
        private Guid subcategory;
        private Guid status;
        private Guid store;
        private Guid supplier;
        private Guid warehouse;
        private string productImageDestinationPath = "";
        private string productImageSourcePath = "";
        private string barcodeImageDestinationPath = "";
        private string barcodeImageSourcePath = "";
        private Guid unitOfMeasurementId;


        private Guid ProductId;

        public AddProduct()
        {
            InitializeComponent();
            initCombo();
        }


        public AddProduct(ListView lvProduct)
        {
            InitializeComponent();
            this.lvProduct = lvProduct;
            initCombo();

            btnUpdate.Enabled = false;

        }

        public AddProduct(Guid productId)
        {
            InitializeComponent();
            this.ProductId = productId;
            initProduct();
            initCombo();
            btnInsert.Enabled = false;
        }

        public void initProduct()
        {
            if (this.ProductId != null)
            {
                
                var product = ProductRepo.retrieve(this.ProductId.ToString());

                if (product.Count > 0)
                {
                    txtName.Text = product[0].ProductName;
                    txtSku.Text = product[0].ProductSku;
                    txtDescription.Text = product[0].ProductDescription;
                    txtUnitCost.Text = product[0].ProductUnitCost.ToString();
                    txtQuantity.Text = product[0].ProductQuantity.ToString();
                    txtReorderLimit.Text = product[0].ProductReorderLimit.ToString();
                    txtReplenishLimit.Text = product[0].ProductReplenishLimit.ToString();
                    txtVat.Text = product[0].ProductVat.ToString();
                    txtDiscount.Text = product[0].ProductDiscount.ToString();
                    cmbCategory.Text = product[0].ProductCategories;
                    cmbSubCategory.Text = product[0].ProductSubCategories;
                    cmbSupplier.Text = product[0].ProductSupplier;
                    cmbWarehouse.Text = product[0].ProductWarehouse;
                    cmbStatus.Text = product[0].ProductStatuses;
                    cmbStore.Text = product[0].ProductStore;
                    cmbUnitOfMeasurement.Text = product[0].ProductUnitOfMeasurement;
                    txtBarcode.Text = product[0].ProductBarcode;
                    lblProductImage.Text = product[0].ProductImage;
                    lblBarcodeImage.Text = product[0].ProductBarcodeImage;

                    string productPath = AssemblyDirectory + @"\Image\ProductImages\" + product[0].ProductImage;

                    if (File.Exists(productPath)) {
                        Image productImage = Image.FromFile(productPath);
                        Bitmap tempBitmap = new Bitmap(productImage);
                        pbProductImage.Image = tempBitmap;
                    }

                    string barcodePath = AssemblyDirectory + @"\Image\BarcodeImages\" + product[0].ProductBarcodeImage;

                    if (File.Exists(barcodePath)) {
                        Image barcodeImage = Image.FromFile(barcodePath);
                        Bitmap tempbarcodeBitmap = new Bitmap(barcodeImage);
                        pbBarcodeImage.Image = tempbarcodeBitmap;
                    }                       

                    foreach (var item in product[0].ProductAttributes)
                    {
                        cmbAddedAttributes.Items.Add(item.AttributeName + ":" + item.AttributeValue);
                    }
                    attributes = (product[0].ProductAttributes).ToList();

                }
            }
        }

        public void initCombo()
        {
            var unitOfMeasurement = UnitOfMeasurementRepo.retrieve();

            if (unitOfMeasurement.Count > 0)
            {
                foreach (var item in unitOfMeasurement)
                {
                    cmbUnitOfMeasurement.Items.Add(item.UnitOfMeasurementName.ToString());
                }
            }
            var categories = CategoryRepo.retrieve();

            if (categories.Count > 0)
            {
                foreach (var item in categories)
                {
                    cmbCategory.Items.Add(item.CategoryValue);
                }
            }

            var subcategory = SubCategoryRepo.retrieve();

            if (subcategory.Count > 0)
            {
                foreach (var item in subcategory)
                {
                    cmbSubCategory.Items.Add(item.SubCategoryValue);
                }
            }

            var supplier = SupplierRepo.suppliers();

            if (supplier.Count > 0)
            {
                foreach (var item in supplier)
                {
                    cmbSupplier.Items.Add(item.SupplierName);
                }
            }

            var warehouse = WarehouseRepo.retrieve();

            if (warehouse.Count > 0)
            {
                foreach (var item in warehouse)
                {
                    cmbWarehouse.Items.Add(item.WarehouseName);
                }
            }

            var status = StatusRepo.retrieve();

            if (status.Count > 0)
            {
                foreach (var item in status)
                {
                    cmbStatus.Items.Add(item.StatusValue);
                }
            }

            List<SMLIB.Entity.ProductAttribute> attributes = ProductAttributeRepo.retrieve();

            if (attributes.Count > 0)
            {
                foreach (var item in attributes)
                {
                    cmbAttributes.Items.Add(item.AttributeName);
                }
            }

            var store = StoreRepo.retrieve();

            if (store.Count > 0)
            {
                foreach (var item in store)
                {
                    cmbStore.Items.Add(item.StoreName);
                }
            }
        }
        private void imageCheck() {
            try
            {            
                pbProductImage.Image.Dispose();
                pbBarcodeImage.Image.Dispose();

                string pathProduct = AssemblyDirectory + @"\Image\ProductImages\" + lblProductImage.Text;
                string pathBarcode = AssemblyDirectory + @"\Image\BarcodeImages\" + lblBarcodeImage.Text;

                string newDestinationOfProductImage = AssemblyDirectory + @"\Image\copyOfImages\" + lblProductImage.Text;
                string newDestinationOfBarcodeImage = AssemblyDirectory + @"\Image\copyOfImages\" + lblBarcodeImage.Text;

                if ((this.productImageDestinationPath == "" && this.productImageSourcePath == "") || (this.barcodeImageDestinationPath == "" && this.barcodeImageSourcePath == ""))
                {
                    if (File.Exists(pathProduct))//if image already exists delete the image in the new destination
                    {
                        if (File.Exists(newDestinationOfProductImage))
                        {
                            File.Delete(newDestinationOfProductImage);
                        }

                        File.Copy(pathProduct, newDestinationOfProductImage);//copies the image to the new destination
                    
                        this.productImageSourcePath = newDestinationOfProductImage; //sets the source path to new destination
                    }

                    if (File.Exists(pathBarcode))//if image already exists delete the image in the new destination
                    {
                        if (File.Exists(newDestinationOfBarcodeImage))
                        {
                            File.Delete(newDestinationOfBarcodeImage);
                        }

                        File.Copy(pathBarcode, newDestinationOfBarcodeImage);//copies the image to the new destination
                                                                             //File.Delete(pathBarcode);//deletes the image
                        this.barcodeImageSourcePath = newDestinationOfBarcodeImage;//sets the source path to new destination
                    }
                    this.barcodeImageDestinationPath = pathBarcode;
                    this.productImageDestinationPath = pathProduct;

                    if (!File.Exists(this.productImageDestinationPath))
                    {
                        File.Copy(this.productImageSourcePath, this.productImageDestinationPath);
                    }

                    if (!File.Exists(this.barcodeImageDestinationPath))
                    {
                        File.Copy(this.barcodeImageSourcePath, this.barcodeImageDestinationPath);
                    }

                    if (File.Exists(newDestinationOfProductImage)) {
                        File.Delete(newDestinationOfProductImage);
                    }

                    if (File.Exists(newDestinationOfBarcodeImage)) {
                        File.Delete(newDestinationOfBarcodeImage);
                    }


                }
                else {

                    if (File.Exists(this.productImageDestinationPath))
                    {
                        pbProductImage.Image = Image.FromFile(this.productImageDestinationPath);
                    }
                    else {
                        File.Copy(this.productImageSourcePath, this.productImageDestinationPath);
                    }

                    if (File.Exists(this.barcodeImageDestinationPath))
                    {
                        pbBarcodeImage.Image = Image.FromFile(this.barcodeImageDestinationPath);
                    }
                    else {
                        File.Copy(this.barcodeImageSourcePath, this.barcodeImageDestinationPath);
                    }                
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
                      
                double reorderLimit;
                bool isDoubleReorderLimit = double.TryParse(txtReorderLimit.Text, out reorderLimit);
                double replenishLimit;
                bool isDoubleReplenish = double.TryParse(txtReplenishLimit.Text, out replenishLimit);
                double unitCost;
                bool isDoubleCost = double.TryParse(txtUnitCost.Text, out unitCost);
                double quantity;
                bool isDoubleQuantity = double.TryParse(txtQuantity.Text, out quantity);
                double vat;
                bool isdoubleVat = double.TryParse(txtVat.Text, out vat);
                double discount;
                bool isDoubleDiscount = double.TryParse(txtDiscount.Text, out discount);


                if (txtName.Text == "")
                {
                    MessageBox.Show("Invalid name.");
                }
                else if (txtBarcode.Text == "")
                {
                    MessageBox.Show("Please use the scanner to generate the barcode.");
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Invalid description.");
                }
                else if (!isDoubleCost)
                {
                    MessageBox.Show("Invalid unit cost input.");
                }
                else if (!isDoubleQuantity)
                {
                    MessageBox.Show("Invalid quantity input.");
                }
                else if (!isDoubleReorderLimit)
                {
                    MessageBox.Show("Invalid reorder limit input.");
                }
                else if (!isDoubleReplenish)
                {
                    MessageBox.Show("Invalid replenish limit input.");
                }

                else if (!isdoubleVat)
                {
                    MessageBox.Show("Invalid vat input.");
                }
                else if (!isDoubleDiscount)
                {
                    MessageBox.Show("Invalid discount input.");
                }
                else if (cmbUnitOfMeasurement.Text == "")
                {
                    MessageBox.Show("Invalid unit of measurement.");
                }
                else if (cmbCategory.Text == "")
                {
                    MessageBox.Show("Invalid category.");
                }
                else if (cmbSubCategory.Text == "")
                {
                    MessageBox.Show("Invalid sub category.");
                }
                else if (cmbSupplier.Text == "")
                {
                    MessageBox.Show("Invalid supplier.");
                }
                else if (cmbWarehouse.Text == "")
                {
                    MessageBox.Show("Invalid warehouse.");
                }
                else if (cmbStatus.Text == "")
                {
                    MessageBox.Show("Invalid status.");
                }
                else if (cmbStore.Text == "")
                {
                    MessageBox.Show("Invalid store.");
                }
                else
                {
                    bool productExists = ProductRepo.checkIfProductNameExists(txtName.Text);
                    if (productExists)
                    {
                        MessageBox.Show("Product name already exists on the record, please try again.");
                    }
                    else
                    {
                       imageCheck();
                       ProductRepo.insert(txtName.Text.ToLower(), txtDescription.Text.ToLower(), this.CategoryId, this.subcategory, unitCost,
                                       quantity, txtBarcode.Text, lblBarcodeImage.Text, txtSku.Text.ToLower(), vat,
                                       lblProductImage.Text, this.status, this.store, DateTime.Now.ToString("d"), replenishLimit,
                                       reorderLimit, discount, this.supplier, this.warehouse, this.attributes, this.unitOfMeasurementId
                                       );

                        MessageBox.Show("Product has been successfully inserted.");
                        Admin admin = new Admin();
                        admin.Show();
                        this.Hide();
                    }

                }

           

        }

        private void pbProductImage_Click(object sender, EventArgs e)
        {
            try
            {                   
                pbProductImage.Image.Dispose();
                pbBarcodeImage.Image.Dispose();             

                ofdProduct.Title = "Browse Product Image Files";
                ofdProduct.Filter = "Image files (*.jpg, *.jpeg, *.gif, *.png, *.bmp, *.tiff) | *.jpg; *.jpeg; *.gif; *.png; *.bmp; *.tiff";

                if (ofdProduct.ShowDialog() == DialogResult.OK)
                {
                    lblProductImage.Text = ofdProduct.SafeFileName;

                    string productImagepath = AssemblyDirectory + @"\Image\ProductImages\" + lblProductImage.Text;

                    this.productImageDestinationPath = productImagepath;
                    this.productImageSourcePath = ofdProduct.FileName;

                    Image productImage = Image.FromFile(ofdProduct.FileName);
                    Bitmap ProductBitmap = new Bitmap(productImage);
                    pbProductImage.Image = ProductBitmap;                   

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }


        }

        private void panelProduct_Paint(object sender, PaintEventArgs e)
        {

        }

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

        private void AddProduct_Load(object sender, EventArgs e)
        {
            try
            {
                Guid categoryId = CategoryRepo.getCategoryId(cmbCategory.Text);
                if (categoryId != null) {
                    this.CategoryId = categoryId;
                }
                Guid SubcategoryId = SubCategoryRepo.getSubCategoryId(cmbSubCategory.Text);
                if (SubcategoryId != null) {
                    this.subcategory = SubcategoryId;
                }
                Guid SupplierId = SupplierRepo.getSupplierId(cmbSupplier.Text);
                if (SupplierId != null) {
                    this.supplier = SupplierId;
                }
               Guid WarehouseId = WarehouseRepo.getWarehouseId(cmbWarehouse.Text);
                if (WarehouseId != null) {
                    this.warehouse = WarehouseId;
                }
                Guid statusId = StatusRepo.getStatusId(cmbStatus.Text);
                if (statusId != null) {
                    this.status = statusId;
                }
                Guid StoreId = StoreRepo.getStoreId(cmbStore.Text);
                if (StoreId != null) {
                    this.store = StoreId;
                }
                Guid id = UnitOfMeasurementRepo.retrieveId(cmbUnitOfMeasurement.Text);
                if (id != null)
                {
                    this.unitOfMeasurementId = id;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CategoryId = CategoryRepo.getCategoryId(cmbCategory.Text);
        }

        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.subcategory = SubCategoryRepo.getSubCategoryId(cmbSubCategory.Text);

        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.supplier = SupplierRepo.getSupplierId(cmbSupplier.Text);

        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.warehouse = WarehouseRepo.getWarehouseId(cmbWarehouse.Text);

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.status = StatusRepo.getStatusId(cmbStatus.Text);

        }

        private void cmbStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.store = StoreRepo.getStoreId(cmbStore.Text);

        }

        private void cmbAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.attributeId = ProductAttributeRepo.getAttributeId(cmbAttributes.Text);

        }

        private void pbAddAttribute_Click(object sender, EventArgs e)
        {
            ProductAttribute productAttribute = new ProductAttribute();
            productAttribute.AttributeId = Guid.NewGuid();
            productAttribute.AttributeName = cmbAttributes.Text;
            productAttribute.AttributeValue = txtAttributeValue.Text;

            this.attributes.Add(productAttribute);
            cmbAddedAttributes.Items.Add(cmbAttributes.Text + ":" + productAttribute.AttributeValue);
            cmbAttributes.Text = "";
            txtAttributeValue.Text = "";

        }

        private void fillAttributes()
        {
            cmbAddedAttributes.Items.Clear();
            for (int i = 0; i < this.attributes.Count; i++)
            {
                cmbAddedAttributes.Items.Add(this.attributes[i].AttributeName + ":" + this.attributes[i].AttributeValue);
            }
        }
        private void pbBarcodeImage_Click(object sender, EventArgs e)
        {
                     
                pbProductImage.Image.Dispose();
                pbBarcodeImage.Image.Dispose();
               
                ofdBarcode.Title = "Browse Barcode Image Files";
                ofdBarcode.Filter = "Image files (*.jpg, *.jpeg, *.gif, *.png, *.bmp, *.tiff) | *.jpg; *.jpeg; *.gif; *.png; *.bmp; *.tiff";
                ofdBarcode.CheckFileExists = true;
                ofdBarcode.CheckPathExists = true;
                ofdBarcode.RestoreDirectory = true;

                if (ofdBarcode.ShowDialog() == DialogResult.OK)
                {
                    lblBarcodeImage.Text = ofdBarcode.SafeFileName;
                    string barcodeImagePath = AssemblyDirectory + @"\Image\BarcodeImages\" + lblBarcodeImage.Text;

                    this.barcodeImageDestinationPath = barcodeImagePath;
                    this.barcodeImageSourcePath = ofdBarcode.FileName;

                    Image barcodeImage = Image.FromFile(ofdBarcode.FileName);
                    Bitmap barcodeBitmap = new Bitmap(barcodeImage);
                    pbBarcodeImage.Image = barcodeBitmap;

                }
            
        }

        private void AddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }

        private void pbCategory_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory(cmbCategory);
            addCategory.Show();
        }

        private void pbSubCategory_Click(object sender, EventArgs e)
        {
            SubCategory subcat = new SubCategory(cmbSubCategory);
            subcat.Show();

        }

        private void pbSupplier_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier(cmbSupplier);
            supplier.Show();

        }

        private void pbWarehouse_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = new Warehouse(cmbWarehouse);
            warehouse.Show();

        }

        private void pbStatus_Click(object sender, EventArgs e)
        {
            Status status = new Status(cmbStatus);
            status.Show();

        }

        private void pbStore_Click(object sender, EventArgs e)
        {
            Store store = new Store(cmbStore);
            store.Show();

        }

        private void pbAttributes_Click(object sender, EventArgs e)
        {
            Quality quality = new Quality();
            quality.Show();

        }

        private void cmbUnitOfMeasurement_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid id = UnitOfMeasurementRepo.retrieveId(cmbUnitOfMeasurement.Text);
            if (id != null) {
                this.unitOfMeasurementId = id;
            }
        }

        private void clearAll()
        {
            txtBarcode.Clear();
            txtSku.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtDiscount.Clear();
            txtQuantity.Clear();
            txtUnitCost.Clear();
            txtVat.Clear();
            txtReorderLimit.Clear();
            txtReplenishLimit.Clear();
            txtVatPercent.Clear();
            txtAttributeValue.Clear();
            cmbUnitOfMeasurement.Text = "";
            cmbCategory.Text = "";
            cmbSubCategory.Text = "";
            cmbSupplier.Text = "";
            cmbWarehouse.Text = "";
            cmbStore.Text = "";
            cmbStatus.Text = "";
            cmbAttributes.Text = "";
            txtDiscountPercent.Clear();
            cmbAddedAttributes.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            
                double reorderLimit;
                bool isDoubleReorderLimit = double.TryParse(txtReorderLimit.Text, out reorderLimit);
                double replenishLimit;
                bool isDoubleReplenish = double.TryParse(txtReplenishLimit.Text, out replenishLimit);
                double unitCost;
                bool isDoubleCost = double.TryParse(txtUnitCost.Text, out unitCost);
                double quantity;
                bool isDoubleQuantity = double.TryParse(txtQuantity.Text, out quantity);
                double vat;
                bool isdoubleVat = double.TryParse(txtVat.Text, out vat);
                double discount;
                bool isDoubleDiscount = double.TryParse(txtDiscount.Text, out discount);


                if (txtName.Text == "")
                {
                    MessageBox.Show("Invalid name.");
                }
                else if (txtBarcode.Text == "")
                {
                    MessageBox.Show("Please use the scanner to generate the barcode.");
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Invalid description.");
                }
                else if (!isDoubleCost)
                {
                    MessageBox.Show("Invalid unit cost input.");
                }
                else if (!isDoubleQuantity)
                {
                    MessageBox.Show("Invalid quantity input.");
                }
                else if (!isDoubleReorderLimit)
                {
                    MessageBox.Show("Invalid reorder limit input.");
                }
                else if (!isDoubleReplenish)
                {
                    MessageBox.Show("Invalid replenish limit input.");
                }
                else if (!isdoubleVat)
                {
                    MessageBox.Show("Invalid vat input.");
                }
                else if (!isDoubleDiscount)
                {
                    MessageBox.Show("Invalid discount input.");
                }
                else if (cmbUnitOfMeasurement.Text == "")
                {
                    MessageBox.Show("Invalid unit of measurement.");
                }
                else if (cmbCategory.Text == "")
                {
                    MessageBox.Show("Invalid category.");
                }
                else if (cmbSubCategory.Text == "")
                {
                    MessageBox.Show("Invalid sub category.");
                }
                else if (cmbSupplier.Text == "")
                {
                    MessageBox.Show("Invalid supplier.");
                }
                else if (cmbWarehouse.Text == "")
                {
                    MessageBox.Show("Invalid warehouse.");
                }
                else if (cmbStatus.Text == "")
                {
                    MessageBox.Show("Invalid status.");
                }
                else if (cmbStore.Text == "")
                {
                    MessageBox.Show("Invalid store.");
                }
                else
                {
                    pbProductImage.Image.Dispose();
                    pbBarcodeImage.Image.Dispose();

                    imageCheck();

                    ProductRepo.update(this.ProductId, txtName.Text, txtDescription.Text, txtBarcode.Text,
                                        lblBarcodeImage.Text, this.CategoryId, DateTime.Now.ToString("d"),
                                        double.Parse(txtDiscount.Text), lblProductImage.Text, double.Parse(txtQuantity.Text),
                                        double.Parse(txtReorderLimit.Text), double.Parse(txtReplenishLimit.Text), txtSku.Text,
                                        this.status, this.store, this.subcategory, this.supplier, double.Parse(txtUnitCost.Text),
                                        this.unitOfMeasurementId, double.Parse(txtVat.Text), this.warehouse, this.attributes
                                        );
                    MessageBox.Show("Product has been updated.");
                    clearAll();
                    Admin admin = new Admin();
                    admin.Show();
                    this.Hide();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating the product. \n" + ex.Message);
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbAddedAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = cmbAddedAttributes.SelectedIndex;
                string[] attrib = new string[2];
                attrib = cmbAddedAttributes.Items[index].ToString().Split(':');

                cmbAttributes.Text = attrib[0];
                txtAttributeValue.Text = attrib[1];
            }
            catch (Exception)
            {
                index = 0;
                MessageBox.Show("You need to select a quality to update.");
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = cmbAddedAttributes.SelectedIndex;
                this.attributes[index].AttributeName = cmbAttributes.Text;
                this.attributes[index].AttributeValue = txtAttributeValue.Text;
                cmbAttributes.Text = "";
                txtAttributeValue.Text = "";
                cmbAddedAttributes.Text = "";
                fillAttributes();
                MessageBox.Show("Quality " + cmbAttributes.Text + " has been updated.");

            }
            catch (Exception)
            {
                index = 0;
                MessageBox.Show("You need to select a quality to update.");
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                Admin admin = new Admin();
                admin.Show();
                this.Hide();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void txtVatPercent_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblCategory_Click(object sender, EventArgs e)
        {

        }

        private void txtReorderLimit_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblReplenishLimit_Click(object sender, EventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUnitOfMeasurement_MouseClick(object sender, MouseEventArgs e)
        {
            cmbUnitOfMeasurement.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbCategory_MouseClick(object sender, MouseEventArgs e)
        {
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbSubCategory_MouseClick(object sender, MouseEventArgs e)
        {
            cmbSubCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbSupplier_MouseClick(object sender, MouseEventArgs e)
        {
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbWarehouse_MouseClick(object sender, MouseEventArgs e)
        {
            cmbWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbStatus_MouseClick(object sender, MouseEventArgs e)
        {
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbStore_MouseClick(object sender, MouseEventArgs e)
        {
            cmbStore.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmbAttributes_MouseClick(object sender, MouseEventArgs e)
        {
            cmbAttributes.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
