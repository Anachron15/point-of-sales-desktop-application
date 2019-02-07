using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Repository
{
    public class SupplierRepo
    {
        public static List<Supplier> suppliers() {
            List<Supplier> suppliers;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                suppliers = (from sup in context.Suppliers
                             select sup).ToList();
            }
            return suppliers;
        }
        public static Guid getSupplierId(string supplier) {
            Guid id;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                id = (from sup in context.Suppliers
                      where sup.SupplierName == supplier
                      select sup.SupplierId).FirstOrDefault();
            }
            return id;
        }
        public static void delete(Guid id) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var sup = (from s in context.Suppliers where s.SupplierId == id select s).FirstOrDefault();

                context.Suppliers.Remove(sup);
                context.SaveChanges();
            }
        }
        public static void update(Guid id, string name,string address,double contact) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var sup = (from s in context.Suppliers where s.SupplierId == id select s).FirstOrDefault();
                sup.SupplierName = name;
                sup.SupplierAddress = address;
                sup.SupplierContactNumber = contact;

                context.SaveChanges();
            }
        }
        public static void insert(Guid id, string name, string address, double contact) {
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                var sup = new Supplier() {
                    SupplierAddress = address,
                    SupplierContactNumber = contact,
                    SupplierId = id,
                    SupplierName = name
                };
                context.Suppliers.Add(sup);
                context.SaveChanges();
            }
        }
        public static List<Supplier> retrieveByName(string name) {
            List<Supplier> supplier;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                supplier = (from s in context.Suppliers where s.SupplierName == name select s).ToList();
            }
            return supplier;
        }
        public static bool checkIfSupplierExists(string name) {
            bool b = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                b = context.Suppliers.Any(x => x.SupplierName == name);
            }
            return b;
        }
    }
}
