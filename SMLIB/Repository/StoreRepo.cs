using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Repository
{
   public class StoreRepo
    {
        public static List<Store> retrieve() {
            List<Store> stores;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                stores = (from s in context.Stores
                          select s
                         ).ToList();
            }
            return stores;
        }

        public static Guid getStoreId(string store) {
            Guid id;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                id = (from s in context.Stores where s.StoreName == store select s.StoreId).FirstOrDefault();
            }
            return id;
        }
        public static void delete(Guid id) {
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                Store store = (from s in context.Stores where s.StoreId == id select s).FirstOrDefault();
                context.Stores.Remove(store);
                context.SaveChanges();
            }
        }
        public static void update(Guid id, string name, string address, double contact) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                Store store = (from s in context.Stores where s.StoreId == id select s).FirstOrDefault();
                store.StoreName = name;
                store.StoreAddress = address;
                store.StoreContactNumber = contact;

                context.SaveChanges();
            }
        }
        public static void insert(Guid id, string name, string address, double contact) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                Store store = new Store() {
                    StoreId = id,
                    StoreName = name,
                    StoreAddress = address,
                    StoreContactNumber = contact                
                };

                context.Stores.Add(store);
                context.SaveChanges();
            }
        }
        public static Store retrieveByName(string name) {
            Store store;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                store = (from s in context.Stores where s.StoreName == name select s).FirstOrDefault();
            }
            return store;
        }
        public static bool checkIfStoreExists(string name) {
            bool b = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                b = context.Stores.Any(x => x.StoreName == name);
            }
            return b;
        }
    }
}
