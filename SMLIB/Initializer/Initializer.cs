 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
using System.Net.Configuration;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Initializer
{
    public class Initializer : CreateDatabaseIfNotExists<SMLIB.Context.Context>
    {
        protected override void Seed(SMLIB.Context.Context context)
        {
            var productAttributes = new ProductAttribute()
            {
                AttributeId = Guid.NewGuid(),
                AttributeName = "Color",
                AttributeValue = "Red"
            };

            var productAttributes2 = new ProductAttribute()
            {
                AttributeId = Guid.NewGuid(),
                AttributeName = "Size",
                AttributeValue = "XL"

            };

            context.Attributes.Add(productAttributes);
            context.Attributes.Add(productAttributes2);

            var categories = new Category()
            {
                CategoryId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d769"),
                CategoryValue = "Mens Shirt"
            };

            var shortcategories = new Category()
            {
                CategoryId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d783"),
                CategoryValue = "Mens Short"
            };

            context.Categories.Add(categories);

            var subCategory = new SubCategory()
            {
                SubCategoryId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d569"),
                SubCategoryValue = "Shirt"

            };

            var shortsubCategory = new SubCategory()
            {
                SubCategoryId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d777"),
                SubCategoryValue = "Short"

            };
            context.SubCategories.Add(subCategory);

            var status = new Status()
            {
                StatusId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d716"),
                StatusValue = "In Stock"
            };

            var status1 = new Status()
            {
                StatusId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d715"),
                StatusValue = "Out Of Stock"
            };

            var status2 = new Status()
            {
                StatusId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d714"),
                StatusValue = "Defective"
            };

           

            context.Statuses.Add(status);
            context.Statuses.Add(status1);
            context.Statuses.Add(status2);

            var supplier = new Supplier()
            {
                SupplierId = Guid.Parse("25d74d63-2b45-4c93-8d20-e6e8b0e3d716"),
                SupplierAddress = "Poblacion III, Lebak Sultan Kudarat",
                SupplierName = "Bethoven Acha",
                SupplierContactNumber = 09102718746

            };

            context.Suppliers.Add(supplier);

            var warehouse = new Warehouse()
            {
                WarehouseId = Guid.Parse("25d74d63-2b45-4c93-8d20-e1e8b0e3d716"),
                WarehouseAddress = "Poblacion I, Lebak Sultan Kudarat",
                WarehouseContactNumber = 09102718746,
                WarehouseName = "Digoys Warehouse"
            };

            context.Warehouses.Add(warehouse);

            List<ProductAttribute> prodAttrib = new List<ProductAttribute>();
            prodAttrib.Add(productAttributes);
            prodAttrib.Add(productAttributes2);

            context.Attributes.Add(productAttributes);
            context.Attributes.Add(productAttributes2);

            var userDetailsCustomer = new UserDetail()
            {
                UserDetailId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d766"),
                UserAddress = "Poblacion II,Lebak Sultan Kudarat",
                UserContactNumber = 09102718746,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserImage = "unknown.png",
                UserRole = "customer",
                UserStatus = "active"

            };

            var userDetailsCashier = new UserDetail()
            {
                UserDetailId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d767"),
                UserAddress = "Poblacion II,Lebak Sultan Kudarat",
                UserContactNumber = 09102718746,
                UserFirstName = "Jovelyn",
                UserLastName = "Acha",
                UserImage = "jovelyn.png",
                UserRole = "cashier",
                UserStatus = "active"

            };
            var userDetailsAdmin = new UserDetail()
            {
                UserDetailId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d768"),
                UserAddress = "Poblacion II,Lebak Sultan Kudarat",
                UserContactNumber = 09102718746,
                UserFirstName = "Edwin",
                UserLastName = "Acha",
                UserImage = "edwin.png",
                UserRole = "admin",
                UserStatus = "active"

            };

            var userCustomer = new User()
            {
                UserId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d766"),
                UserEmail = "customer@gmail.com",
                UserPassword = "password",

                UserDetail = userDetailsCustomer

            };

            var userCashier = new User()
            {
                UserId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d767"),
                UserEmail = "cashier@gmail.com",
                UserPassword = "password",
                UserDetail = userDetailsCashier
            };
            var userAdmin = new User()
            {
                UserId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d768"),
                UserEmail = "admin@gmail.com",
                UserPassword = "password",
                UserDetail = userDetailsAdmin
            };

            var store = new Store()
            {
                StoreId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d768"),
                StoreAddress = "Poblacion III, Lebak Sultan Kudarat",
                StoreName = "South Mall"
                

            };

            context.Stores.Add(store);

            context.Users.Add(userCustomer);
            context.Users.Add(userCashier);
            context.Users.Add(userAdmin);

            context.UserDetails.Add(userDetailsCustomer);
            context.UserDetails.Add(userDetailsCashier);
            context.UserDetails.Add(userDetailsAdmin);

            var itm = new UnitOfMeasurement() {
                UnitOfMeasurementId =Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d268"),
                UnitOfMeasurementName = "Item"
            };

            context.UnitOfMeasurements.Add(itm);

            var Kilogram = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d267"),
                UnitOfMeasurementName = "Kilogram"
            };

            context.UnitOfMeasurements.Add(Kilogram);

            var Gram = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d266"),
                UnitOfMeasurementName = "Gram"
            };

            context.UnitOfMeasurements.Add(Gram);

            var Milligram = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d265"),
                UnitOfMeasurementName = "Milligram"
            };

            context.UnitOfMeasurements.Add(Milligram);

            var Liter = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d264"),
                UnitOfMeasurementName = "Liter"
            };
            context.UnitOfMeasurements.Add(Liter);

            var CublicLiter = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d263"),
                UnitOfMeasurementName = "CublicLiter"
            };
            context.UnitOfMeasurements.Add(CublicLiter);

            var MilliLiter = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d262"),
                UnitOfMeasurementName = "MilliLiter"
            };
            context.UnitOfMeasurements.Add(MilliLiter);

            var Meter = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d261"),
                UnitOfMeasurementName = "Meter"
            };
            context.UnitOfMeasurements.Add(Meter);

            var Centimeter = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d221"),
                UnitOfMeasurementName = "Centimeter"
            };
            context.UnitOfMeasurements.Add(Centimeter);

            var Millimeter = new UnitOfMeasurement()
            {
                UnitOfMeasurementId = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d211"),
                UnitOfMeasurementName = "Millimeter"
            };
            context.UnitOfMeasurements.Add(Millimeter);

            Product product = new Product()
            {
                ProductId = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d556"),
                ProductName = "shirt",
                ProductDescription = "Description for shirt",
                ProductBarcode = "1234asdf",
                ProductBarcodeImage = "barcodeImage.png",
                ProductDateAdded = DateTime.Now.ToString("d"),
                ProductImage = "productImage.png",         
                ProductQuantity = 100,
                ProductReorderLimit = 25,
                ProductReplenishLimit = 50,
                ProductSku = "123sku",
                ProductUnitCost = 50,
                ProductVat = 12,
                ProductAttributes = prodAttrib,
                ProductCategory = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d769"),
                ProductSubCategory = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d569"),
                ProductStatus = Guid.Parse("25d74d64-2b45-4c93-8d20-e6e8b0e3d716"),
                ProductStore = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d768"),  
                ProductSupplier = Guid.Parse("25d74d63-2b45-4c93-8d20-e6e8b0e3d716"),
                ProductWarehouse = Guid.Parse("25d74d63-2b45-4c93-8d20-e1e8b0e3d716"),
                ProductDiscount = 150,
                ProductUnitOfMeasurement = Guid.Parse("15d74d64-2b45-4c93-8d20-e6e8b0e3d267")
            };

            context.Products.Add(product);
            context.SaveChanges();
        }
    }
}
