using SMLIB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;

namespace SMLIB.Repository
{
    public class CategoryRepo
    {
        public static List<Category> retrieve() {
            List<Category> categories;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                 categories = (from category in context.Categories                 
                                 select  category).Distinct().ToList();
            }
            return categories;
        }
        public static Guid getCategoryId(string category) {
            Guid id;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                id = (from cat in context.Categories
                     where cat.CategoryValue == category
                     select cat.CategoryId).FirstOrDefault();
            }
            return id;
        }
        public static bool checkIfCategoryExists(string category) {
            bool exist = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                exist = context.Categories.Any(c=>c.CategoryValue==category.ToLower());
            }
            return exist;
        }
        public static void create(string category) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var categ = new Category() {
                    CategoryId=Guid.NewGuid(),
                    CategoryValue = category.ToLower()
                };

                context.Categories.Add(categ);
                context.SaveChanges();
            }
        }
        public static void update(Guid categoryId,string category) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var categ = (from c in context.Categories
                            where c.CategoryId==categoryId
                            select c).FirstOrDefault();
                 categ.CategoryValue = category;
                context.SaveChanges();
               
            }
        }
        public static bool checkIfCategoryIsUsed(Guid id) {
            bool isUsed;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                isUsed = context.Products.Any(x => x.ProductCategory == id);
            }
            return isUsed;
        }
        public static void delete(Guid id) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var cat = (from c in context.Categories where c.CategoryId == id select c).FirstOrDefault();

                context.Categories.Remove(cat);
                context.SaveChanges();

            }
        }
        public static List<Category> retrieveCategoryByName(string category) {
            List<Category> categories;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                categories = (from c in context.Categories where c.CategoryValue == category select c).ToList();
            }
            return categories;
        }
    }
}
