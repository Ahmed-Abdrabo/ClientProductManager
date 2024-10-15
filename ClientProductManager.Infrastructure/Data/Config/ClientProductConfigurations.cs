using ClientProductManager.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Infrastructure.Data.Config
{
    internal class ClientProductConfigurations : IEntityTypeConfiguration<ClientProducts>
    {
        public void Configure(EntityTypeBuilder<ClientProducts> builder)
        {
            builder.HasKey(cp => new { cp.ClientId, cp.ProductId });
          
        }
    }
}
