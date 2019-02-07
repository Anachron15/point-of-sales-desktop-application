using SMLIB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.View
{
   public  class vwProductOrder
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

        public string ProductDateAdded { get; set; }
        public string ProductDateRemoved { get; set; }

        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public Guid ProductStatuses { get; set; }
        public Guid ProductWarehouse { get; set; }
        public Guid ProductSupplier { get; set; }
        public Guid ProductStore { get; set; }
        public Guid ProductCategories { get; set; }
        public Guid ProductSubCategories { get; set; }

        public Guid OrderId { get; set; }
        public Guid Order_CashierId { get; set; }
        public DateTime Order_DateTime { get; set; }
        public Guid Order_Transaction { get; set; }
        public Guid Order_User { get; set; }
        public Guid Order_ProductId { get; set; }
        public double Order_Quantity { get; set; }
    }
}
