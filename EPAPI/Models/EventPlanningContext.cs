using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EPAPI.Models;

public partial class EventPlanningContext : DbContext
{
    public EventPlanningContext()
    {
    }

    public EventPlanningContext(DbContextOptions<EventPlanningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatalogItem> CatalogItems { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventItem> EventItems { get; set; }

    public virtual DbSet<IdentificationType> IdentificationTypes { get; set; }

    public virtual DbSet<ItemCategory> ItemCategories { get; set; }

    public virtual DbSet<Referral> Referrals { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<SellerSocialMedium> SellerSocialMedia { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationRequest> VerificationRequests { get; set; }

    public virtual DbSet<VerificationStatus> VerificationStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)

        {

            IConfigurationRoot configuration = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

                        .AddJsonFile("appsettings.json")

                        .Build();

            var connectionString = configuration.GetConnectionString("EventPlannerDB");

            optionsBuilder.UseMySQL(connectionString);

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("catalog_item");

            entity.HasIndex(e => e.ItemCategoryId, "item_category_id");

            entity.HasIndex(e => e.SellerId, "seller_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsService).HasColumnName("is_service");
            entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(1000)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPrice)
                .HasPrecision(10)
                .HasColumnName("item_price");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");

            entity.HasOne(d => d.ItemCategory).WithMany(p => p.CatalogItems)
                .HasForeignKey(d => d.ItemCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("catalog_item_ibfk_2");

            entity.HasOne(d => d.Seller).WithMany(p => p.CatalogItems)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("catalog_item_ibfk_1");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cities");

            entity.HasIndex(e => e.StateId, "state_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cities_ibfk_1");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("countries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.BuyerId, "buyer_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.EventBudget)
                .HasPrecision(10)
                .HasColumnName("event_budget");
            entity.Property(e => e.EventDate)
                .HasColumnType("date")
                .HasColumnName("event_date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(1000)
                .HasColumnName("event_description");
            entity.Property(e => e.EventLocation)
                .HasMaxLength(100)
                .HasColumnName("event_location");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .HasColumnName("event_name");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Events)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_ibfk_1");
        });

        modelBuilder.Entity<EventItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("event_items");

            entity.HasIndex(e => new { e.EventId, e.ItemId }, "event_id").IsUnique();

            entity.HasIndex(e => e.ItemId, "item_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Event).WithMany(p => p.EventItems)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_items_ibfk_1");

            entity.HasOne(d => d.Item).WithMany(p => p.EventItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_items_ibfk_2");
        });

        modelBuilder.Entity<IdentificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("identification_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ItemCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("item_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
        });

        modelBuilder.Entity<Referral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("referrals");

            entity.HasIndex(e => e.SellerId, "seller_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");

            entity.HasOne(d => d.Seller).WithMany(p => p.Referrals)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("referrals_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sellers");

            entity.HasIndex(e => e.IdentificationTypeId, "identification_type_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasColumnName("company_name");
            entity.Property(e => e.ExperienceYears).HasColumnName("experience_years");
            entity.Property(e => e.Freelance).HasColumnName("freelance");
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(100)
                .HasColumnName("identification_number");
            entity.Property(e => e.IdentificationTypeId).HasColumnName("identification_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.IdentificationType).WithMany(p => p.Sellers)
                .HasForeignKey(d => d.IdentificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sellers_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Sellers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sellers_ibfk_1");
        });

        modelBuilder.Entity<SellerSocialMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("seller_social_media");

            entity.HasIndex(e => e.SellerId, "seller_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.SocialMediaName)
                .HasMaxLength(50)
                .HasColumnName("social_media_name");
            entity.Property(e => e.SocialMediaUrl)
                .HasMaxLength(255)
                .HasColumnName("social_media_url");

            entity.HasOne(d => d.Seller).WithMany(p => p.SellerSocialMedia)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seller_social_media_ibfk_1");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("states");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("states_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contact_phone");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsCompany).HasColumnName("is_company");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MailVisible).HasColumnName("mail_visible");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PhoneVisible).HasColumnName("phone_visible");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<VerificationRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("verification_requests");

            entity.HasIndex(e => e.AdminId, "admin_id");

            entity.HasIndex(e => e.SellerId, "seller_id");

            entity.HasIndex(e => e.StatusId, "status_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminComments)
                .HasMaxLength(255)
                .HasColumnName("admin_comments");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TransacDate)
                .HasColumnType("date")
                .HasColumnName("transac_date");

            entity.HasOne(d => d.Admin).WithMany(p => p.VerificationRequests)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("verification_requests_ibfk_2");

            entity.HasOne(d => d.Seller).WithMany(p => p.VerificationRequests)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("verification_requests_ibfk_1");

            entity.HasOne(d => d.Status).WithMany(p => p.VerificationRequests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("verification_requests_ibfk_3");
        });

        modelBuilder.Entity<VerificationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("verification_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
