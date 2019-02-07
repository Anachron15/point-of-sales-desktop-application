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
    public partial class Sales_Report : Form
    {
        public Sales_Report()
        {
            InitializeComponent();
        }

        private void Sales_Report_Load(object sender, EventArgs e)
        {
            var transaction = TransactionRepo.retrieveSubTotalAndDate();
            if (transaction.Count > 0) {
                double Mondaytotal = 0;
                double Tuesdaytotal = 0;
                double Wednesdaytotal = 0;
                double Thursdaytotal = 0;
                double Fridaytotal = 0;
                double Saturdaytotal = 0;
                double Sundaytotal = 0;

                chartDaily.Series["Daily"].XValueMember = "Monday";
                chartDaily.Series["Daily"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                chartDaily.Series["Daily"].XValueMember = "Tuesday";
                chartDaily.Series["Daily"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

                for (int i = 0; i < transaction.Count; i++) {

                    DateTime dt = DateTime.Parse(transaction[i].TransactionDate);                   
                    
                    switch (dt.DayOfWeek.ToString())
                    {
                        case "Monday":
                            Mondaytotal += transaction[i].TransactionSubTotal;          
                            break;
                        case "Tuesday":
                            Tuesdaytotal += transaction[i].TransactionSubTotal;     
                            break;
                        case "Wednesday":
                            Wednesdaytotal += transaction[i].TransactionSubTotal;
                            break;
                        case "Thursday":
                            Thursdaytotal += transaction[i].TransactionSubTotal;
                            break;
                        case "Friday":
                            Fridaytotal += transaction[i].TransactionSubTotal;
                            break;
                        case "Saturday":                         
                            Saturdaytotal += transaction[i].TransactionSubTotal;
                            break;
                        case "Sunday":
                            Sundaytotal += transaction[i].TransactionSubTotal;
                            break;
                        default:
                            break;
                    }
                }

               

                chartDaily.Series["Daily"].Points.AddXY("Monday", Mondaytotal);
                chartDaily.Series["Daily"].Points.AddXY("Tuesday", Tuesdaytotal);
                chartDaily.Series["Daily"].Points.AddXY("Wednesday", Wednesdaytotal);
                chartDaily.Series["Daily"].Points.AddXY("Thursday", Thursdaytotal);
                chartDaily.Series["Daily"].Points.AddXY("Friday", Fridaytotal);
                chartDaily.Series["Daily"].Points.AddXY("Saturday", Saturdaytotal);
                chartDaily.Series["Daily"].Points.AddXY("Saturday", Sundaytotal);
            }
        }
    }
}
