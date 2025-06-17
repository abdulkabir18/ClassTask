using ClassTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassTask.Configurations
{
    public class MediaUserEntityTypeConfiguration : IEntityTypeConfiguration<MediaUser>
    {
        public void Configure(EntityTypeBuilder<MediaUser> builder)
        {
            builder.ToTable("MediaUsers");

            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Email);
            builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(m => m.UserName).IsRequired(false).HasMaxLength(15);
            builder.Property(m => m.FirstName).IsRequired().HasMaxLength(20);
            builder.Property(m => m.LastName).IsRequired().HasMaxLength(25);
            builder.Property(m => m.Address).IsRequired(false).HasMaxLength(100);
            builder.Property(m => m.DateOfBirth).IsRequired().HasConversion(c => c.ToDateTime(TimeOnly.MinValue), c => DateOnly.FromDateTime(c)).HasColumnType("datetime");
            builder.Property(m => m.DateAdded);
        }
    }
}
