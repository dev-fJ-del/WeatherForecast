using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WeatherForecast.Models
{
    public partial class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext()
        {
          
        }

        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Main> Mains { get; set; }
        public virtual DbSet<UserWeather> UserWeathers { get; set; }
        public virtual DbSet<Weather> Weathers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Main>(entity =>
            {
                entity.HasKey(x => x.WeatherId);

                entity.ToTable("Main");

                entity.Property(e => e.WeatherId)
                    .ValueGeneratedNever()
                    .HasColumnName("weatherId");

                entity.Property(e => e.Feels_Like).HasColumnName("feels_like");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.MainId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("mainId");

                entity.Property(e => e.Pressure).HasColumnName("pressure");

                entity.Property(e => e.Temp).HasColumnName("temp");

                entity.Property(e => e.Temp_Max).HasColumnName("temp_max");

                entity.Property(e => e.Temp_Min).HasColumnName("temp_min");

                entity.HasOne(d => d.Weather)
                    .WithOne(p => p.Main)
                    .HasForeignKey<Main>(x => x.WeatherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Main_Weather");
            });

            modelBuilder.Entity<UserWeather>(entity =>
            {
                entity.ToTable("UserWeather");
            });

            modelBuilder.Entity<Weather>(entity =>
            {
                entity.ToTable("Weather");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("icon");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
