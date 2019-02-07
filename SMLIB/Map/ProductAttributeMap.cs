using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;
using System.Data.Entity.ModelConfiguration;

namespace SMLIB.Map
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap() {
            HasMany(p => p.ProductAttributes)
                .WithRequired()
                .HasForeignKey(key => key.ProductId);
        }
    }
}
