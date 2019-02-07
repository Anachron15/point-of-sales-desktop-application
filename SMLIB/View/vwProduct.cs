
using SMLIB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.View
{
    public class vwProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductBarcodeImage { get; set; }
        public string ProductSku { get; set; }
        public string ProductImage { get; set; }

        public Double ProductVat { get; set; }
        public Double ProductUnitCost { get; set; }
        public Double ProductQuantity { get; set; }
        public Double ProductReplenishLimit { get; set; }
        public Double ProductReorderLimit { get; set; }
        public Double ProductDiscount { get; set; }

        public string ProductDateAdded { get; set; }
       

        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public string ProductStatuses { get; set; }
        public string ProductWarehouse { get; set; }
        public string ProductSupplier { get; set; }
        public string ProductStore { get; set; }
        public string ProductCategories { get; set; }
        public string ProductSubCategories { get; set; }
        public string ProductUnitOfMeasurement { get; set; }

    }
}
