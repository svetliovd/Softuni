﻿using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data.EntityConfigurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasMany(i => i.OrderItems)
                 .WithOne(oi => oi.Item)
                 .HasForeignKey(oi => oi.ItemId);

            builder.HasAlternateKey(i => i.Name);
        }
    }
}
