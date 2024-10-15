using ClientProductManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Infrastructure.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

         
            builder.HasMany(p => p.ClientProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}