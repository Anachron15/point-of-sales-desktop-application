using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Repository
{
   public class SubCategoryRepo
    {
        public static List<SubCategory> retrieveByName(string subCategory) {
            List<SubCategory> subcateg;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                subcateg = (from subcat in context.SubCategories where subcat.SubCategoryValue == subCategory select subcat).ToList();

            }
            return subcateg;
        }
        public static List<SubCategory> retrieve() {
            List<SubCategory> subCategories;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                subCategories = (from subcat in context.SubCategories
                                select subcat).ToList();
            }
            return subCategories;
        }
        public static Guid getSubCategoryId(string subcategory) {
            Guid id;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context())
            {
                id = (from subcat in context.SubCategories
                      where subcat.SubCategoryValue == subcategory
                                 select subcat.SubCategoryId).FirstOrDefault();
            }
            return id;
        }

        public static bool checkIfSubCategoryExists(string subCategory) {
            bool b = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                b = context.SubCategories.Any(x => x.SubCategoryValue == subCategory);
            }
            return b;
        }
        public static void insert(Guid id, string subcategory) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var sc = new SubCategory() {
                    SubCategoryId = id,
                    SubCategoryValue = subcategory
                };
                context.SubCategories.Add(sc);
                context.SaveChanges();
            }
        }
        public static void update(Guid id, string subcategory) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var sc = (from subcat in context.SubCategories where subcat.SubCategoryId == id select subcat).FirstOrDefault();
                sc.SubCategoryValue = subcategory;
                context.SaveChanges();
            }

        }
        public static void delete(Guid id) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var sc = (from subcat in context.SubCategories where subcat.SubCategoryId == id select subcat).FirstOrDefault();
                context.SubCategories.Remove(sc);
                context.SaveChanges();
            }
        }
    }
}
