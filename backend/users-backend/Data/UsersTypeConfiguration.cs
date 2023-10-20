using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using users_backend.Models;

namespace users_backend.Data
{
    // Define User column types configurations
    public class UsersTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).ValueGeneratedOnAdd();
            builder
                .Property(b => b.UserName)
                .HasMaxLength(250)
                .IsRequired();

            builder
            .Property(b => b.Email)
            .HasMaxLength(250);

            builder
            .Property(b => b.UserId)
            .HasMaxLength(250)
            .IsRequired();

            builder
            .Property(b => b.Phone)
            .HasMaxLength(250);
        }
    }
}
