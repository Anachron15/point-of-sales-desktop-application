using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Repository
{
    public class ProductAttributeRepo
    {
        public static List<ProductAttribute> retrieve() {
            List<ProductAttribute> attributes;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                attributes = (from attrib in context.Attributes
                              select attrib) .Distinct().ToList();
            }
            return attributes;
        }
        public static Guid getAttributeId(string attributeName) {
            Guid id;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                id = (from attrib in context.Attributes
                      where attrib.AttributeName == attributeName
                      select attrib.AttributeId).FirstOrDefault();
            }
            return id;
        }
    }
}
