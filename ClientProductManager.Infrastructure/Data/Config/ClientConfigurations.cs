using ClientProductManager.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProductManager.Core.Enums;

namespace ClientProductManager.Infrastructure.Data.Config
{
    internal class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Code).IsUnique();
            builder.Property(c => c.Class)
              .IsRequired()
              .HasConversion(
                  cClass => cClass.ToString(), 
                  cClass => (ClientClass)Enum.Parse(typeof(ClientClass), cClass) 
              );

            builder.Property(c => c.State)
                .IsRequired()
                .HasConversion(
                    state => state.ToString(), 
                    state => (ClientState)Enum.Parse(typeof(ClientState), state) 
                );
            builder.HasMany(c => c.ClientProducts)
                .WithOne(cp => cp.Client)
                .HasForeignKey(cp => cp.ClientId);
        }
    }
}
