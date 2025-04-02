using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NaturguidenServerPrototype.Models;

namespace NaturguidenServerPrototype.Data;

public partial class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    public virtual DbSet<NatureSpot> NatureSpots { get; set; }

    public virtual DbSet<PlaceVisit> PlaceVisits { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<NatureSpot>(entity => {
            entity.HasKey(e => e.Id).HasName("nature_spots_pkey");

            entity.ToTable("nature_spots");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HasCarParking)
                .HasDefaultValue(false)
                .HasColumnName("has_car_parking");
            entity.Property(e => e.HasElectricity)
                .HasDefaultValue(false)
                .HasColumnName("has_electricity");
            entity.Property(e => e.HasToilets)
                .HasDefaultValue(false)
                .HasColumnName("has_toilets");
            entity.Property(e => e.HasWater)
                .HasDefaultValue(false)
                .HasColumnName("has_water");
            entity.Property(e => e.Latitude)
                .HasPrecision(9, 6)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(9, 6)
                .HasColumnName("longitude");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<PlaceVisit>(entity => {
            entity.HasKey(e => new { e.UserId, e.PlaceId }).HasName("place_visits_pkey");

            entity.ToTable("place_visits");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.CreatedTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Place).WithMany()
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("place_visits_place_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PlaceVisits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("place_visits_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
