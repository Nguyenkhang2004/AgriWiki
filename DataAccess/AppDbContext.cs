using System;
using System.Collections.Generic;
using AgriWiki_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriWiki_Project.DataAccess;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<GrowthCondition> GrowthConditions { get; set; }

    public virtual DbSet<Pest> Pests { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<Use> Uses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PlantManagement;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("diseases_pkey");

            entity.ToTable("diseases");

            entity.Property(e => e.DiseaseId).HasColumnName("disease_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Prevention).HasColumnName("prevention");
        });

        modelBuilder.Entity<GrowthCondition>(entity =>
        {
            entity.HasKey(e => e.ConditionId).HasName("growth_conditions_pkey");

            entity.ToTable("growth_conditions");

            entity.Property(e => e.ConditionId).HasColumnName("condition_id");
            entity.Property(e => e.HumidityRange)
                .HasMaxLength(100)
                .HasColumnName("humidity_range");
            entity.Property(e => e.PlantId).HasColumnName("plant_id");
            entity.Property(e => e.SoilType)
                .HasMaxLength(100)
                .HasColumnName("soil_type");
            entity.Property(e => e.Sunlight)
                .HasMaxLength(100)
                .HasColumnName("sunlight");
            entity.Property(e => e.TemperatureRange)
                .HasMaxLength(100)
                .HasColumnName("temperature_range");
            entity.Property(e => e.Watering).HasColumnName("watering");

            entity.HasOne(d => d.Plant).WithMany(p => p.GrowthConditions)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("growth_conditions_plant_id_fkey");
        });

        modelBuilder.Entity<Pest>(entity =>
        {
            entity.HasKey(e => e.PestId).HasName("pests_pkey");

            entity.ToTable("pests");

            entity.Property(e => e.PestId).HasColumnName("pest_id");
            entity.Property(e => e.ControlMethods).HasColumnName("control_methods");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("plants_pkey");

            entity.ToTable("plants");

            entity.Property(e => e.PlantId).HasColumnName("plant_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ScientificName)
                .HasMaxLength(200)
                .HasColumnName("scientific_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Plants)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("plants_category_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Plants)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("plants_created_by_fkey");

            entity.HasMany(d => d.Diseases).WithMany(p => p.Plants)
                .UsingEntity<Dictionary<string, object>>(
                    "PlantDisease",
                    r => r.HasOne<Disease>().WithMany()
                        .HasForeignKey("DiseaseId")
                        .HasConstraintName("plant_diseases_disease_id_fkey"),
                    l => l.HasOne<Plant>().WithMany()
                        .HasForeignKey("PlantId")
                        .HasConstraintName("plant_diseases_plant_id_fkey"),
                    j =>
                    {
                        j.HasKey("PlantId", "DiseaseId").HasName("plant_diseases_pkey");
                        j.ToTable("plant_diseases");
                        j.IndexerProperty<int>("PlantId").HasColumnName("plant_id");
                        j.IndexerProperty<int>("DiseaseId").HasColumnName("disease_id");
                    });

            entity.HasMany(d => d.Pests).WithMany(p => p.Plants)
                .UsingEntity<Dictionary<string, object>>(
                    "PlantPest",
                    r => r.HasOne<Pest>().WithMany()
                        .HasForeignKey("PestId")
                        .HasConstraintName("plant_pests_pest_id_fkey"),
                    l => l.HasOne<Plant>().WithMany()
                        .HasForeignKey("PlantId")
                        .HasConstraintName("plant_pests_plant_id_fkey"),
                    j =>
                    {
                        j.HasKey("PlantId", "PestId").HasName("plant_pests_pkey");
                        j.ToTable("plant_pests");
                        j.IndexerProperty<int>("PlantId").HasColumnName("plant_id");
                        j.IndexerProperty<int>("PestId").HasColumnName("pest_id");
                    });

            entity.HasMany(d => d.Uses).WithMany(p => p.Plants)
                .UsingEntity<Dictionary<string, object>>(
                    "PlantUse",
                    r => r.HasOne<Use>().WithMany()
                        .HasForeignKey("UseId")
                        .HasConstraintName("plant_uses_use_id_fkey"),
                    l => l.HasOne<Plant>().WithMany()
                        .HasForeignKey("PlantId")
                        .HasConstraintName("plant_uses_plant_id_fkey"),
                    j =>
                    {
                        j.HasKey("PlantId", "UseId").HasName("plant_uses_pkey");
                        j.ToTable("plant_uses");
                        j.IndexerProperty<int>("PlantId").HasColumnName("plant_id");
                        j.IndexerProperty<int>("UseId").HasColumnName("use_id");
                    });
        });

        modelBuilder.Entity<Use>(entity =>
        {
            entity.HasKey(e => e.UseId).HasName("uses_pkey");

            entity.ToTable("uses");

            entity.Property(e => e.UseId).HasColumnName("use_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("'ENDUSER'::character varying")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
