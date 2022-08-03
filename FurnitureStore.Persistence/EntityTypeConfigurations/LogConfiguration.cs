using FurnitureStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureStore.Persistence.EntityTypeConfigurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.Id)
            .IsUnique();

        builder.Property(l => l.Application)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("application");
        
        builder.Property(l => l.Logged)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("logged");
        
        builder.Property(l => l.Level)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("level");
        
        builder.Property(l => l.Message)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("message");
        
        builder.Property(l => l.Logger)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("logger");
        
        builder.Property(l => l.Callsite)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("callsite");
        
        builder.Property(l => l.Exception)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("exception");
    }
}