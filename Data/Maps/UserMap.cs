using Expenses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250).HasColumnType("varchar(250)");
            builder.Property(x => x.Password).IsRequired();
        }
    }
}