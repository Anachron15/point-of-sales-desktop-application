
using SMLIB.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Repository
{
    public class ProductRepo
    {
        public static bool checkIfProductNameExists(string name) {
            bool b = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                b = context.Products.Any(x => x.ProductName == name);
            }
            return b;
        }
        public static List<vwProduct> retrieveByInitialLetter(string letter) {
            List<vwProduct> prod;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                prod = (from p in context.Products where p.ProductName.StartsWith(letter) select new vwProduct {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,                   
                    ProductUnitOfMeasurement = (from m in context.UnitOfMeasurements where m.UnitOfMeasurementId == p.ProductUnitOfMeasurement select m.UnitOfMeasurementName).FirstOrDefault(),
                    ProductDescription = p.ProductDescription,                    
                    ProductQuantity = p.ProductQuantity,
                    ProductSku = p.ProductSku,
                    ProductUnitCost = p.ProductUnitCost                   
                }).ToList();
            }
            return prod;
        } 
        public static List<vwProduct> retrieveProductByCategoryId(Guid categoryId)
        {
            List<vwProduct> product;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                product = (from p in context.Products
                           where p.ProductCategory == categoryId
                           select new vwProduct {
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               ProductBarcode = p.ProductBarcode,
                               ProductBarcodeImage = p.ProductBarcodeImage,
                               ProductDateAdded = p.ProductDateAdded,
                               ProductUnitOfMeasurement = (from m in context.UnitOfMeasurements where m.UnitOfMeasurementId == p.ProductUnitOfMeasurement select m.UnitOfMeasurementName).FirstOrDefault(),
                               ProductDescription = p.ProductDescription,
                               ProductImage = p.ProductImage,
                               ProductQuantity = p.ProductQuantity,
                               ProductSku = p.ProductSku,
                               ProductUnitCost = p.ProductUnitCost,
                               ProductVat = p.ProductVat,
                               ProductReorderLimit = p.ProductReorderLimit,
                               ProductReplenishLimit = p.ProductReplenishLimit,
                               ProductAttributes = p.ProductAttributes,
                               ProductCategories = (from category in context.Categories where category.CategoryId == p.ProductCategory select category.CategoryValue).FirstOrDefault(),
                               ProductStatuses = (from status in context.Statuses where status.StatusId == p.ProductStatus select status.StatusValue).FirstOrDefault(),
                               ProductSubCategories = (from subcategory in context.SubCategories where subcategory.SubCategoryId == p.ProductSubCategory select subcategory.SubCategoryValue).FirstOrDefault(),
                               ProductStore = (from store in context.Stores where store.StoreId == p.ProductStore select store.StoreName).FirstOrDefault(),
                               ProductSupplier = (from supplier in context.Suppliers where supplier.SupplierId == p.ProductSupplier select supplier.SupplierName).FirstOrDefault(),
                               ProductWarehouse = (from warehouse in context.Warehouses where warehouse.WarehouseId == p.ProductWarehouse select warehouse.WarehouseName).FirstOrDefault()

                           }).ToList();
            }
            return product;
        }
        public static List<vwProduct> retrieveAll()
        {
            List<vwProduct> products;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                products = (from p in context.Products
                            select new vwProduct
                            {
                                ProductName = p.ProductName,
                                ProductAttributes = p.ProductAttributes,
                                ProductBarcode = p.ProductBarcode,
                                ProductBarcodeImage = p.ProductBarcodeImage,
                                ProductCategories = (from category in context.Categories where category.CategoryId == p.ProductCategory select category.CategoryValue).FirstOrDefault(),
                                ProductDateAdded = p.ProductDateAdded,
                                ProductId = p.ProductId,
                                ProductImage = p.ProductImage,
                                ProductQuantity = p.ProductQuantity,
                                ProductReorderLimit = p.ProductReorderLimit,
                                ProductReplenishLimit = p.ProductReplenishLimit,
                                ProductSku = p.ProductSku,
                                ProductStatuses = (from stat in context.Statuses where stat.StatusId == p.ProductStatus select stat.StatusValue).FirstOrDefault(),
                                ProductStore = (from s in context.Stores where s.StoreId == p.ProductStore select s.StoreName).FirstOrDefault(),
                                ProductSubCategories = (from subcat in context.SubCategories where subcat.SubCategoryId == p.ProductSubCategory select subcat.SubCategoryValue).FirstOrDefault(),
                                ProductSupplier = (from sup in context.Suppliers where sup.SupplierId == p.ProductSupplier select sup.SupplierName).FirstOrDefault(),
                                ProductUnitCost = p.ProductUnitCost,
                                ProductVat = p.ProductVat,
                                ProductWarehouse = (from warehouse in context.Warehouses where warehouse.WarehouseId == p.ProductWarehouse select warehouse.WarehouseName).FirstOrDefault(),
                                ProductDiscount = p.ProductDiscount,
                                ProductDescription = p.ProductDescription,
                                ProductUnitOfMeasurement = (from uom in context.UnitOfMeasurements where uom.UnitOfMeasurementId == p.ProductUnitOfMeasurement select uom.UnitOfMeasurementName).FirstOrDefault()

                            }).ToList();
            }
            return products;
        }
        public static void delete(Guid id)
        {
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                var product = (from p in context.Products where p.ProductId == id select p).FirstOrDefault();
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        public static void insert(string productName, string description, Guid category,
                                  Guid subcategory, double unitCost, double quantity, string barcode,
                                  string barcodeImage, string sku, double vat, string productImage, Guid status, Guid store,
                                  string dateAdded, double replenish, double reorder, double discount,
                                  Guid supplier, Guid warehouse, List<ProductAttribute> productAttributes, Guid unitOfMeasurement
                                  )
        {

            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                var product = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = productName,
                    ProductDescription = description,
                    ProductBarcode = barcode,
                    ProductBarcodeImage = barcodeImage,
                    ProductSku = sku,
                    ProductImage = productImage,
                    ProductVat = vat,
                    ProductUnitCost = unitCost,
                    ProductQuantity = quantity,
                    ProductReplenishLimit = replenish,
                    ProductReorderLimit = reorder,
                    ProductDiscount = discount,
                    ProductDateAdded = dateAdded,
                    ProductUnitOfMeasurement = unitOfMeasurement,
                    ProductCategory = category,
                    ProductSubCategory = subcategory,
                    ProductStatus = status,
                    ProductStore = store,
                    ProductSupplier = supplier,
                    ProductWarehouse = warehouse,
                    ProductAttributes = productAttributes
                };

                context.Products.Add(product);
                context.SaveChanges();
            }
        }


        public static bool checkProductQuantity(string search)
        {
            bool b;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                b = context.Products.All(product => product.ProductQuantity == 0 && (product.ProductName == search || product.ProductBarcode == search || product.ProductSku == search));

            }
            return b;
        }

        public static List<vwProduct> retrieve(string search)
        {
            List<vwProduct> product = new List<vwProduct>();
            Guid prodId;

            bool isId = Guid.TryParse(search, out prodId);
            if (isId)
            {

                using (SMLIB.Context.Context context = new SMLIB.Context.Context())
                {
                    product = (from p in context.Products

                               where p.ProductId == prodId
                               select new vwProduct
                               {
                                   ProductId = p.ProductId,
                                   ProductName = p.ProductName,
                                   ProductBarcode = p.ProductBarcode,
                                   ProductBarcodeImage = p.ProductBarcodeImage,
                                   ProductDateAdded = p.ProductDateAdded,
                                   ProductUnitOfMeasurement = (from m in context.UnitOfMeasurements where m.UnitOfMeasurementId == p.ProductUnitOfMeasurement select m.UnitOfMeasurementName).FirstOrDefault(),
                                   ProductDescription = p.ProductDescription,
                                   ProductImage = p.ProductImage,
                                   ProductQuantity = p.ProductQuantity,
                                   ProductSku = p.ProductSku,
                                   ProductUnitCost = p.ProductUnitCost,
                                   ProductVat = p.ProductVat,
                                   ProductReorderLimit = p.ProductReorderLimit,
                                   ProductReplenishLimit = p.ProductReplenishLimit,
                                   ProductAttributes = p.ProductAttributes,
                                   ProductCategories = (from category in context.Categories where category.CategoryId == p.ProductCategory select category.CategoryValue).FirstOrDefault(),
                                   ProductStatuses = (from status in context.Statuses where status.StatusId == p.ProductStatus select status.StatusValue).FirstOrDefault(),
                                   ProductSubCategories = (from subcategory in context.SubCategories where subcategory.SubCategoryId == p.ProductSubCategory select subcategory.SubCategoryValue).FirstOrDefault(),
                                   ProductStore = (from store in context.Stores where store.StoreId == p.ProductStore select store.StoreName).FirstOrDefault(),
                                   ProductSupplier = (from supplier in context.Suppliers where supplier.SupplierId == p.ProductSupplier select supplier.SupplierName).FirstOrDefault(),
                                   ProductWarehouse = (from warehouse in context.Warehouses where warehouse.WarehouseId == p.ProductWarehouse select warehouse.WarehouseName).FirstOrDefault()

                               }).ToList();

                }

            }
            else
            {
                using (SMLIB.Context.Context context = new SMLIB.Context.Context())
                {
                    product = (from p in context.Products

                               where p.ProductBarcode == search || p.ProductSku == search || p.ProductName == search
                               select new vwProduct
                               {
                                   ProductId = p.ProductId,
                                   ProductName = p.ProductName,
                                   ProductBarcode = p.ProductBarcode,
                                   ProductBarcodeImage = p.ProductBarcodeImage,
                                   ProductDateAdded = p.ProductDateAdded,
                                   ProductDescription = p.ProductDescription,
                                   ProductImage = p.ProductImage,
                                   ProductQuantity = p.ProductQuantity,
                                   ProductSku = p.ProductSku,
                                   ProductUnitCost = p.ProductUnitCost,
                                   ProductVat = p.ProductVat,
                                   ProductReorderLimit = p.ProductReorderLimit,
                                   ProductReplenishLimit = p.ProductReplenishLimit,
                                   ProductAttributes = p.ProductAttributes,
                                   ProductCategories = (from category in context.Categories where category.CategoryId == p.ProductCategory select category.CategoryValue).FirstOrDefault(),
                                   ProductStatuses = (from status in context.Statuses where status.StatusId == p.ProductStatus select status.StatusValue).FirstOrDefault(),
                                   ProductSubCategories = (from subcategory in context.SubCategories where subcategory.SubCategoryId == p.ProductSubCategory select subcategory.SubCategoryValue).FirstOrDefault(),
                                   ProductStore = (from store in context.Stores where store.StoreId == p.ProductStore select store.StoreName).FirstOrDefault(),
                                   ProductSupplier = (from supplier in context.Suppliers where supplier.SupplierId == p.ProductSupplier select supplier.SupplierName).FirstOrDefault(),
                                   ProductWarehouse = (from warehouse in context.Warehouses where warehouse.WarehouseId == p.ProductWarehouse select warehouse.WarehouseName).FirstOrDefault(),
                                   ProductUnitOfMeasurement = (from uom in context.UnitOfMeasurements where uom.UnitOfMeasurementId == p.ProductUnitOfMeasurement select uom.UnitOfMeasurementName).FirstOrDefault()

                               }).ToList();

                }
            }




            return product;
        }

        public static bool checkIfQuantityExceeds(Double quantity, String sku)
        {
            bool b = false;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                var product = context.Products;

                b = product.All(x => x.ProductSku == sku && x.ProductQuantity < quantity);

            }
            return b;
        }

        public static void updateQuantityBySku(Double quantity, String sku)
        {
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                var product = (from p in context.Products
                               where p.ProductSku == sku
                               select p).FirstOrDefault();
                product.ProductQuantity = quantity;
                context.SaveChanges();
            }
        }

        public static double getQuantityBySku(string sku)
        {
            List<double> quantity;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                quantity = (from p in context.Products
                            where p.ProductSku == sku
                            select p.ProductQuantity).ToList();
            }
            return quantity[0];
        }
        public static List<vwProductOrder> retrieveProductsByTransactionId(Guid transactionId)
        {
            List<vwProductOrder> products;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                products = (from p in context.Products
                            from o in context.Orders
                            where o.Order_Transaction == transactionId && p.ProductId == o.Order_ProductId
                            select new vwProductOrder
                            {
                                ProductId = p.ProductId,
                                ProductAttributes = p.ProductAttributes,
                                ProductBarcode = p.ProductBarcode,
                                ProductBarcodeImage = p.ProductBarcodeImage,
                                ProductCategories = p.ProductCategory,
                                ProductDateAdded = p.ProductDateAdded,

                                ProductDescription = p.ProductDescription,
                                ProductImage = p.ProductImage,
                                ProductName = p.ProductName,
                                ProductQuantity = p.ProductQuantity,
                                ProductReorderLimit = p.ProductReorderLimit,
                                ProductReplenishLimit = p.ProductReplenishLimit,
                                ProductSku = p.ProductSku,
                                ProductStatuses = p.ProductStatus,
                                ProductStore = p.ProductStore,
                                ProductSubCategories = p.ProductSubCategory,
                                ProductSupplier = p.ProductSupplier,
                                ProductUnitCost = p.ProductUnitCost,
                                ProductVat = p.ProductVat,
                                ProductWarehouse = p.ProductWarehouse,
                                OrderId = o.OrderId,
                                Order_DateTime = o.Order_DateTime,
                                Order_ProductId = o.Order_ProductId,
                                Order_Transaction = o.Order_Transaction,
                                Order_User = o.Order_UserId,
                                Order_Quantity = o.Order_Quantity
                            }).ToList();

            }
            return products;
        }
        public static List<vwProduct> retrieveProductsAddedToday()
        {
            List<vwProduct> vwProducts;
            string datenow = DateTime.Now.ToString("d");
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                vwProducts = (from p in context.Products
                              where p.ProductDateAdded == datenow
                              select new vwProduct
                              {
                                  ProductName = p.ProductName,
                                  ProductDescription = p.ProductDescription,
                                  ProductBarcode = p.ProductBarcode,
                                  ProductBarcodeImage = p.ProductBarcodeImage,
                                  ProductCategories = (from category in context.Categories where category.CategoryId == p.ProductCategory select category.CategoryValue).FirstOrDefault(),
                                  ProductDateAdded = p.ProductDateAdded,
                                  ProductId = p.ProductId,
                                  ProductImage = p.ProductImage,
                                  ProductQuantity = p.ProductQuantity,
                                  ProductReorderLimit = p.ProductReorderLimit,
                                  ProductReplenishLimit = p.ProductReplenishLimit,
                                  ProductSku = p.ProductSku,
                                  ProductStatuses = (from stat in context.Statuses where stat.StatusId == p.ProductStatus select stat.StatusValue).FirstOrDefault(),
                                  ProductStore = (from s in context.Stores where s.StoreId == p.ProductStore select s.StoreName).FirstOrDefault(),
                                  ProductSubCategories = (from subcat in context.SubCategories where subcat.SubCategoryId == p.ProductSubCategory select subcat.SubCategoryValue).FirstOrDefault(),
                                  ProductSupplier = (from sup in context.Suppliers where sup.SupplierId == p.ProductSupplier select sup.SupplierName).FirstOrDefault(),
                                  ProductUnitCost = p.ProductUnitCost,
                                  ProductVat = p.ProductVat,
                                  ProductWarehouse = (from warehouse in context.Warehouses where warehouse.WarehouseId == p.ProductWarehouse select warehouse.WarehouseName).FirstOrDefault(),
                                  ProductAttributes = (from attrib in context.Attributes where attrib.ProductId == p.ProductId select attrib).ToList(),
                                  ProductDiscount = p.ProductDiscount,
                                  ProductUnitOfMeasurement = (from m in context.UnitOfMeasurements where m.UnitOfMeasurementId == p.ProductUnitOfMeasurement select m.UnitOfMeasurementName).FirstOrDefault()


                              }).ToList();

            }
            return vwProducts;
        }

        public static void update(Guid productId, string productName, string description, string barcode,
                                string barcodeImage, Guid categoryId, string dateAdded, double discount,
                                string productImage, double quantity, double reoderLimit, double replenish,
                                string sku, Guid StatusId, Guid storeId, Guid subCategoryId, Guid supplierId,
                                double unitCost, Guid uomId, double vat, Guid warehouseId, List<ProductAttribute> attrib
                                )
        {
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                var product = (from prod in context.Products where prod.ProductId == productId select prod).FirstOrDefault();

                product.ProductName = productName;
                product.ProductDescription = description;
                product.ProductBarcode = barcode;
                product.ProductBarcodeImage = barcodeImage;
                product.ProductCategory = categoryId;
                product.ProductDateAdded = dateAdded;
                product.ProductDiscount = discount;
                product.ProductImage = productImage;
                product.ProductQuantity = quantity;
                product.ProductReorderLimit = reoderLimit;
                product.ProductReplenishLimit = replenish;
                product.ProductSku = sku;
                product.ProductStatus = StatusId;
                product.ProductStore = storeId;
                product.ProductSubCategory = subCategoryId;
                product.ProductSupplier = supplierId;
                product.ProductUnitCost = unitCost;
                product.ProductUnitOfMeasurement = uomId;
                product.ProductVat = vat;
                product.ProductWarehouse = warehouseId;

                for (int i = 0; i < attrib.Count; i++)
                {
                    Guid id = attrib[i].AttributeId;
                    var attr = (from attrb in context.Attributes where attrb.AttributeId == id select attrb).FirstOrDefault();
                    attr.AttributeName = attrib[i].AttributeName;
                    attr.AttributeValue = attrib[i].AttributeValue;
                }

                context.SaveChanges();
            }
        }
    }
}
