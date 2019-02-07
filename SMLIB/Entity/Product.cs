using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class Product
    {
        [Key]
        [Column("ProductId", Order=1)]

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductBarcodeImage { get; set; }
        public string ProductSku { get; set; }
        public string ProductImage { get; set; }
        public double ProductVat { get; set; }
        public double ProductUnitCost { get; set; }
        public double ProductQuantity { get; set; }
        public double ProductReplenishLimit { get; set; }
        public double ProductReorderLimit { get; set; }
        public double ProductDiscount { get; set; }
        public string ProductDateAdded { get; set; }
        public Guid ProductUnitOfMeasurement { get; set; }
        public Guid ProductCategory { get; set; }
        public Guid ProductSubCategory { get; set; }
        public Guid ProductStatus { get; set; }
        public Guid ProductStore { get; set; }
        public Guid ProductSupplier { get; set; }
        public Guid ProductWarehouse { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        public Product() {
            ProductAttributes = new HashSet<ProductAttribute>();
        }
       

    }
}
