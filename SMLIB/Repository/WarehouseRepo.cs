using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;
namespace SMLIB.Repository
{
    public class WarehouseRepo
    {
        public static List<Warehouse> retrieve() {
            List<Warehouse> warehouses;
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                warehouses = (from w in context.Warehouses
                              select w).ToList();
            }
            return warehouses;
        }

        public static Guid getWarehouseId(string warehouse) {
            Guid id;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                id = (from w in context.Warehouses
                      where w.WarehouseName == warehouse
                      select w.WarehouseId).FirstOrDefault();
            }
            return id;
        }
        public static void delete(Guid id) {
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                var warehouse = (from w in context.Warehouses where w.WarehouseId == id select w).FirstOrDefault();
                context.Warehouses.Remove(warehouse);
                context.SaveChanges();
            }
        }
        public static void update(Guid id, string name, string address,double contact, string sublocation) {
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                var warehouse = (from w in context.Warehouses where w.WarehouseId == id select w).FirstOrDefault();
                warehouse.WarehouseName = name;
                warehouse.WarehouseAddress = address;
                warehouse.WarehouseContactNumber = contact;
                warehouse.WarehouseSubLocation = sublocation;

                context.SaveChanges();
            }
        }
        public static List<Warehouse> retrieveByName(string name) {
            List<Warehouse> warehouse;
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                warehouse = (from w in context.Warehouses where w.WarehouseName == name select w).ToList();
            }
            return warehouse;
        }
        public static bool checkIfWarehouseExists(string name) {
            bool b = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                b = context.Warehouses.Any(x => x.WarehouseName == name);
            }
            return b;
        }
        public static void create(Guid id, string name, string address, double contact, string sublocation) {
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                var warehouse = new Warehouse() {
                    WarehouseAddress = address,
                    WarehouseContactNumber = contact,
                    WarehouseId = id,
                    WarehouseName= name,
                    WarehouseSubLocation = sublocation
                };
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
            }
        }
    }
}
