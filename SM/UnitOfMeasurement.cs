using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMLIB.Repository;

namespace SM
{
    public partial class UnitOfMeasurement : Form
    {
        private string unitOfMeasurement;
        private double unitCost;
        private double quantity;
        private int rowIndex = 0;
        private ListView lv;

        public UnitOfMeasurement()
        {
            InitializeComponent();
        }

        public UnitOfMeasurement(string unitOfMeasurement, double unitCost, double quantity, ListView lv, int rowIndex)
        {
            InitializeComponent();
            this.unitOfMeasurement = unitOfMeasurement;
            this.unitCost = unitCost;
            this.quantity = quantity;
            this.rowIndex = rowIndex;
            this.lv = lv;
            initFields();
        }
        private void setLabel() {
            lblUnitOfMeasurement.Text = "Unit Of Measurement: " + cmbUnitMeasurement.Text;
            lblUnitCost.Text = "Unit Cost: per " + this.unitOfMeasurement;
            lblQuantity.Text = "Quantity: per " + this.unitOfMeasurement;
        }
        private void initFields()
        {

            switch (this.unitOfMeasurement)
            {
                case "KiloGram":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("Gram");
                    cmbUnitMeasurement.Items.Add("MilliGram");
                    setFields();
                    break;
                case "Gram":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("KiloGram");
                    cmbUnitMeasurement.Items.Add("MilliGram");
                    setFields();
                    break;
                case "MilliGram":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("KiloGram");
                    cmbUnitMeasurement.Items.Add("Gram");
                    setFields();
                    break;
                case "Liter":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("CubicCentimeter");
                    cmbUnitMeasurement.Items.Add("MilliLiter");
                    setFields();
                    break;
                case "CubicCentimeter":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("Liter");
                    cmbUnitMeasurement.Items.Add("MilliLiter");
                    setFields();
                    break;
                case "MilliLiter":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("Liter");
                    cmbUnitMeasurement.Items.Add("CubicCentimeter");
                    setFields();
                    break;
                case "Meter":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("CentiMeter");
                    cmbUnitMeasurement.Items.Add("MilliMeter");
                    setFields();
                    break;
                case "CentiMeter":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("Meter");
                    cmbUnitMeasurement.Items.Add("MilliMeter");
                    setFields();
                    break;
                case "MilliMeter":
                    setLabel();
                    cmbUnitMeasurement.Items.Add("Meter");
                    cmbUnitMeasurement.Items.Add("CentiMeter");
                    setFields();
                    break;
                default:
                    setFields();
                    break;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnitMeasurement.Text != "" && txtNewSubTotal.Text != "0" && txtNewUnitCost.Text != "0")
                {
                    this.lv.Items[this.rowIndex].SubItems[2].Text = txtNewUnitCost.Text;
                    this.lv.Items[this.rowIndex].SubItems[3].Text = txtQuantity.Text;
                    this.lv.Items[this.rowIndex].SubItems[4].Text = cmbUnitMeasurement.SelectedItem.ToString();
                    this.lv.Items[this.rowIndex].SubItems[5].Text = txtNewSubTotal.Text;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("New Sub-Total and unit cost can not be 0.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " \n \n Please contact your software provider.");
            }
        }
        private void setFields()
        {           
            txtQuantity.Text = this.quantity.ToString();
            txtUnitCost.Text = this.unitCost.ToString();
        }

        private void UnitOfMeasurement_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Cashier cashier = new Cashier();
                cashier.Show();
                this.Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                this.Hide();
            }
        }
        private double convertMilliGramToGram(double milliGram) {
            return milliGram / 1000;
        }

        private double convertKilogramToGram(double kilogram)
        {
            return kilogram * 1000;
        }
        private double convertGramToMilliGram(double gram)
        {
            return gram * 1000;
        }
        private double calculateKilogramToGramSubTotal(string quantity, string cost)
        {
            double unitCost = 0;
            double numQuantity;
            bool b = double.TryParse(quantity, out numQuantity);
            double numCost;
            bool c = double.TryParse(cost, out numCost);
            if (c && b)
            {
                unitCost = (numQuantity * numCost) / 1000;
            }
            return unitCost;
        }
        private double calculateKilogramToGramNewUnitCost(double subTotal, double quantity)
        {            
            return subTotal / quantity;
        }
        private double convertGramToKilogram(double gram)
        {
            return gram / 1000;
        }
        private double calculateKilogramToMilliGramNewUnitCost(double quantity, double subTotal) {
            return subTotal / quantity;
        }

        private void cmbUnitMeasurement_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantity.Text = this.quantity.ToString();
            txtQuantity.Enabled = true;
            double quantity;
            bool b = double.TryParse(txtQuantity.Text, out quantity);

            if (b && quantity != 0 && quantity > 0)
            {
                if (this.unitOfMeasurement == "KiloGram" && cmbUnitMeasurement.Text == "Gram")
                {
                    txtQuantity.Text = convertKilogramToGram(quantity).ToString();
                    lblQuantity.Text = "Quantity: per " + cmbUnitMeasurement.Text;
                }
                else if (this.unitOfMeasurement == "KiloGram" && cmbUnitMeasurement.Text == "MilliGram")
                {
                    double gram = convertKilogramToGram(quantity);
                    txtQuantity.Text = convertGramToMilliGram(gram).ToString();
                    lblQuantity.Text = "Quantity: per " + cmbUnitMeasurement.Text;
                }
                else if (this.unitOfMeasurement == "Gram" && cmbUnitMeasurement.Text == "KiloGram") {                   
                    txtQuantity.Text = convertGramToKilogram(quantity).ToString();
                    lblQuantity.Text = "Quantity: per " + cmbUnitMeasurement.Text;
                }
            }
            else
            {
                MessageBox.Show("Quantity should be a number greater than 0.");
            }

            lblNewSubTotal.Text = " New Sub-Total per " + cmbUnitMeasurement.SelectedItem.ToString();
            lblNewUnitCost.Text = " New Unit Cost per " + cmbUnitMeasurement.SelectedItem.ToString();
        }

        private bool checkIfQuantityExceeds(double quantity,string sku) {
            bool d = ProductRepo.checkIfQuantityExceeds(quantity, sku);
            if (d)
            {
                MessageBox.Show("Quantity exceeds the amount in stock.");
                txtNewSubTotal.Text = "0";
                txtNewUnitCost.Text = "0";
            }
            return d;
        }
        private double calculateMilliGramToKilogramSubTotal(double quantity, double unitCost) {
            return (quantity * unitCost) / 1000000;
        }
        private double calculateGramToKilogramSubTotal(double gram, double unitCost) {
            return gram * unitCost;
        }
        private double calculateGramToKilogramNewUnitCost(double subTotal, double quantity)
        {
            return subTotal /  quantity;
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            double quantity;
            bool b = double.TryParse(txtQuantity.Text, out quantity);
            string sku = this.lv.Items[this.rowIndex].SubItems[6].Text;

            if (b && quantity != 0 && quantity > 0)
            {
                //Convert quantity to original unit of measurement
                //CHECK IF QUANTITY EXCEEDS THE STOCK
                //if quantity does not exceed deduct the quantity in payment
                bool c = ProductRepo.checkIfQuantityExceeds(quantity, sku);

                if (cmbUnitMeasurement.Text == "")
                {
                    MessageBox.Show("Please select a unit of measurement");
                }
                else if (cmbUnitMeasurement.Text == "Gram" && this.unitOfMeasurement == "KiloGram")//KILOGRAM TO GRAM
                {
                    double kilogram = convertGramToKilogram(quantity);
                    bool d = checkIfQuantityExceeds(kilogram, sku);
                    if (!d) {
                        txtNewSubTotal.Text = calculateKilogramToGramSubTotal(quantity.ToString(), txtUnitCost.Text).ToString();
                        txtNewUnitCost.Text = calculateKilogramToGramNewUnitCost(double.Parse(txtNewSubTotal.Text), quantity).ToString();
                    }
                }
                else if (cmbUnitMeasurement.Text == "MilliGram" && this.unitOfMeasurement == "KiloGram") {//KILOGRAM TO MILLIGRAM
                    double gram = convertMilliGramToGram(quantity);
                    double kilogram = convertGramToKilogram(gram);
                    bool d = checkIfQuantityExceeds(kilogram, sku);
                    if (!d)
                    {
                        txtNewSubTotal.Text = (calculateMilliGramToKilogramSubTotal(quantity, double.Parse(txtUnitCost.Text))).ToString();
                        double newUnitCost = (calculateKilogramToMilliGramNewUnitCost(quantity, double.Parse(txtNewSubTotal.Text)));
                        int count = newUnitCost.ToString().Length;
                        txtNewUnitCost.Text = newUnitCost.ToString("F" + count, CultureInfo.InvariantCulture);
                    }
                }
                else if (cmbUnitMeasurement.Text == "KiloGram" && this.unitOfMeasurement == "Gram") {//GRAM TO KILOGRAM
                    double Gram = convertKilogramToGram(quantity);
                    bool f = checkIfQuantityExceeds(Gram, sku);
                    if (!f) {
                        txtNewSubTotal.Text = calculateGramToKilogramSubTotal(Gram, double.Parse(txtUnitCost.Text)).ToString();
                        double unitCost = calculateGramToKilogramNewUnitCost(double.Parse(txtNewSubTotal.Text), quantity);
                        int count = unitCost.ToString().Length;
                        txtNewUnitCost.Text = unitCost.ToString("F" + count, CultureInfo.InvariantCulture);
                    }
                }
                else if (cmbUnitMeasurement.Text == "MilliGram" && this.unitOfMeasurement == "Gram")
                {
                    
                }
            }
            else
            {
                txtNewSubTotal.Text = "0";
                txtNewUnitCost.Text = "0";
                MessageBox.Show("Input must be a number greater than 0.");
            }

        }

        private void UnitOfMeasurement_Load(object sender, EventArgs e)
        {

        }
    }
}
