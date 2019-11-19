using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Data.Extenstions
{
    public static class ModelBuilderExtension
    {
        public static void AddConfiguration<TEntity>(
            this ModelBuilder modelBuilder, DbEntityConfiguration<TEntity> dbEntityConfiguration)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>(dbEntityConfiguration.Configure);
        }
    }

    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
