using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace LeaseBridge.API.Data;

public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<LeaseStatus> LeaseStatuses { get; set; }

    public virtual DbSet<MaintenanceAssignment> MaintenanceAssignments { get; set; }

    public virtual DbSet<MaintenanceAttachment> MaintenanceAttachments { get; set; }

    public virtual DbSet<MaintenanceCategory> MaintenanceCategories { get; set; }

    public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

    public virtual DbSet<MaintenanceStatus> MaintenanceStatuses { get; set; }

    public virtual DbSet<MaintenanceUpdate> MaintenanceUpdates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<PriorityType> PriorityTypes { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitImage> UnitImages { get; set; }

    public virtual DbSet<UnitStatus> UnitStatuses { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }
    



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.AmenityId).HasName("PK__Amenitie__842AF50BC6F1CC26");

            entity.HasIndex(e => e.Name, "UQ__Amenitie__737584F62D4066A9").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__AppUsers__1788CC4C4ADB3655");

            entity.HasIndex(e => e.IdentityUserId, "UQ_AppUsers_IdentityUserId").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);

            entity.HasMany(d => d.Skills).WithMany(p => p.Staff)
                .UsingEntity<Dictionary<string, object>>(
                    "StaffSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StaffSkills_Skill"),
                    l => l.HasOne<AppUser>().WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StaffSkil__Staff__5FB337D6"),
                    j =>
                    {
                        j.HasKey("StaffId", "SkillId");
                        j.ToTable("StaffSkills");
                        j.IndexerProperty<int>("SkillId").ValueGeneratedOnAdd();
                    });
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4C99F7AA0BFA");

            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.Applications)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Statu__534D60F1");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Applications)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Tenan__5165187F");

            entity.HasOne(d => d.Unit).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__UnitI__52593CB8");
        });

        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Applicat__C8EE206385352328");

            entity.ToTable("ApplicationStatus");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD6B9779A51");

            entity.ToTable("Feedback");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(255);

            entity.HasOne(d => d.Request).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_Feedback_Request");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Tenant__6754599E");
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.HasKey(e => e.LeaseId).HasName("PK__Leases__21FA58C114FB33A2");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.Leases)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leases__StatusId__5629CD9C");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Leases)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leases__TenantId__5441852A");

            entity.HasOne(d => d.Unit).WithMany(p => p.Leases)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leases__UnitId__5535A963");
        });

        modelBuilder.Entity<LeaseStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__LeaseSta__C8EE20637552008F");

            entity.ToTable("LeaseStatus");

            entity.HasIndex(e => e.Name, "UQ_LeaseStatus_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<MaintenanceAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Maintena__32499E7757CAB96A");

            entity.HasIndex(e => new { e.RequestId, e.StaffId }, "UQ_Request_Staff").IsUnique();

            entity.Property(e => e.AssignedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.MaintenanceAssignments)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Reque__5DCAEF64");

            entity.HasOne(d => d.Staff).WithMany(p => p.MaintenanceAssignments)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Staff__5EBF139D");
        });

        modelBuilder.Entity<MaintenanceAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__Maintena__442C64BEB509A6F4");

            entity.Property(e => e.FileUrl)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Request).WithMany(p => p.MaintenanceAttachments)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Reque__58D1301D");
        });

        modelBuilder.Entity<MaintenanceCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Maintena__19093A0BCD2F05BF");

            entity.HasIndex(e => e.Name, "UQ_MaintenanceCategories_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<MaintenanceRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Maintena__33A8517A6EB8F73D");

            entity.HasIndex(e => e.TicketNumber, "UQ_Ticket").IsUnique();

            entity.HasIndex(e => e.TicketNumber, "UQ__Maintena__CBED06DAB3AAE172").IsUnique();

            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TicketNumber).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Categ__59063A47");

            entity.HasOne(d => d.Priority).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Prior__59FA5E80");

            entity.HasOne(d => d.Status).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Statu__5AEE82B9");

            entity.HasOne(d => d.Tenant).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Tenan__571DF1D5");

            entity.HasOne(d => d.Unit).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__UnitI__5812160E");
        });

        modelBuilder.Entity<MaintenanceStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Maintena__C8EE20636D4895EC");

            entity.ToTable("MaintenanceStatus");

            entity.HasIndex(e => e.Name, "UQ_MaintenanceStatus_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<MaintenanceUpdate>(entity =>
        {
            entity.HasKey(e => e.UpdateId).HasName("PK__Maintena__7A0CF3C55F5D63B3");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);

            entity.HasOne(d => d.NewStatus).WithMany(p => p.MaintenanceUpdateNewStatuses)
                .HasForeignKey(d => d.NewStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MU_NewStatus");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.MaintenanceUpdateOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .HasConstraintName("FK_MU_OldStatus");

            entity.HasOne(d => d.Request).WithMany(p => p.MaintenanceUpdates)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Reque__5BE2A6F2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MaintenanceUpdates)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MU_User");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1255D49916");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.NotificationType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("InApp");
            entity.Property(e => e.TargetUrl)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Application).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__Notificat__Appli__66603565");

            entity.HasOne(d => d.MaintenanceRequest).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.MaintenanceRequestId)
                .HasConstraintName("FK__Notificat__Maint__656C112C");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__6477ECF3");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A3867B7C952");

            entity.HasIndex(e => e.TransactionReference, "UQ_Transaction").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionReference)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Lease).WithMany(p => p.Payments)
                .HasForeignKey(d => d.LeaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__LeaseI__619B8048");

            entity.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Method__628FA481");

            entity.HasOne(d => d.Status).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Status__6383C8BA");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__PaymentM__FC681851F4CF137C");

            entity.HasIndex(e => e.Name, "UQ_PaymentMethods_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__PaymentS__C8EE2063B7DD4DC4");

            entity.ToTable("PaymentStatus");

            entity.HasIndex(e => e.Name, "UQ_PaymentStatus_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<PriorityType>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("PK__Priority__D0A3D0BEAE1C0E68");

            entity.HasIndex(e => e.Name, "UQ_PriorityTypes_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Properti__70C9A735812141E8");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Manager).WithMany(p => p.Properties)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Properties_Manager");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skills__DFA0918772BE8E6B");

            entity.HasIndex(e => e.Name, "UQ__Skills__737584F651195CDD").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Units__44F5ECB524C2F27E");

            entity.Property(e => e.RentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitNumber).HasMaxLength(255);

            entity.HasOne(d => d.Property).WithMany(p => p.Units)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Units__PropertyI__4E88ABD4");

            entity.HasOne(d => d.Status).WithMany(p => p.Units)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Units__StatusId__5070F446");

            entity.HasOne(d => d.Type).WithMany(p => p.Units)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Units__TypeId__4F7CD00D");

            entity.HasMany(d => d.Amenities).WithMany(p => p.Units)
                .UsingEntity<Dictionary<string, object>>(
                    "UnitAmenity",
                    r => r.HasOne<Amenity>().WithMany()
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UnitAmeni__Ameni__5F7E2DAC"),
                    l => l.HasOne<Unit>().WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UnitAmeni__UnitI__5E8A0973"),
                    j =>
                    {
                        j.HasKey("UnitId", "AmenityId").HasName("PK__UnitAmen__ECB743E5CEF7A8AA");
                        j.ToTable("UnitAmenities");
                    });
        });

        modelBuilder.Entity<UnitImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__UnitImag__7516F70C8FA81AA8");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitImages)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UnitImage__UnitI__55F4C372");
        });

        modelBuilder.Entity<UnitStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__UnitStat__C8EE2063A09CE406");

            entity.ToTable("UnitStatus");

            entity.HasIndex(e => e.Name, "UQ_UnitStatus_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__UnitType__516F03B52D43BEAD");

            entity.HasIndex(e => e.Name, "UQ_UnitTypes_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        // SEED DATA
 
        //Unit Types
        modelBuilder.Entity<UnitType>().HasData(
            new UnitType { TypeId = 1, Name = "Apartment" },
            new UnitType { TypeId = 2, Name = "Studio" },
            new UnitType { TypeId = 3, Name = "Villa" },
            new UnitType { TypeId = 4, Name = "Office" }
        );


        // Unit Status
        modelBuilder.Entity<UnitStatus>().HasData(
            new UnitStatus { StatusId = 1, Name = "Available" },
            new UnitStatus { StatusId = 2, Name = "Reserved" },
            new UnitStatus { StatusId = 3, Name = "Occupied" },
            new UnitStatus { StatusId = 4, Name = "UnderMaintenance" },
            new UnitStatus { StatusId = 5, Name = "Pending Inspection" }
        );


        // Payment Status
        modelBuilder.Entity<PaymentStatus>().HasData(
            new PaymentStatus { StatusId = 1, Name = "Pending" },
            new PaymentStatus { StatusId = 2, Name = "Partial" },
            new PaymentStatus { StatusId = 3, Name = "Paid" },
            new PaymentStatus { StatusId = 4, Name = "Overdue" }
        );


        // Payment Methods
        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { MethodId = 1, Name = "Cash" },
            new PaymentMethod { MethodId = 2, Name = "Card" },
            new PaymentMethod { MethodId = 3, Name = "Bank Transfer" },
            new PaymentMethod { MethodId = 4, Name = "BenefitPay" }
        );


        //Maintenance Status
        modelBuilder.Entity<MaintenanceStatus>().HasData(
            new MaintenanceStatus { StatusId = 1, Name = "Submitted" },
            new MaintenanceStatus { StatusId = 2, Name = "Assigned" },
            new MaintenanceStatus { StatusId = 3, Name = "In Progress" },
            new MaintenanceStatus { StatusId = 4, Name = "Resolved" },
            new MaintenanceStatus { StatusId = 5, Name = "Closed" }
        );


        //Maintenance Categories
        modelBuilder.Entity<MaintenanceCategory>().HasData(
            new MaintenanceCategory { CategoryId = 1, Name = "Plumbing" },
            new MaintenanceCategory { CategoryId = 2, Name = "Electrical" },
            new MaintenanceCategory { CategoryId = 3, Name = "HVAC" },
            new MaintenanceCategory { CategoryId = 4, Name = "General Maintenance" }
        );


        //Priority Types
        modelBuilder.Entity<PriorityType>().HasData(
            new PriorityType { PriorityId = 1, Name = "Low" },
            new PriorityType { PriorityId = 2, Name = "Medium" },
            new PriorityType { PriorityId = 3, Name = "High" }
        );


        //Skills
        modelBuilder.Entity<Skill>().HasData(
            new Skill { SkillId = 1, Name = "Plumbing" },
            new Skill { SkillId = 2, Name = "Electrical" },
            new Skill { SkillId = 3, Name = "HVAC" },
            new Skill { SkillId = 4, Name = "Carpentry" },
            new Skill { SkillId = 5, Name = "Painting" }
        );


        //Amenities
        modelBuilder.Entity<Amenity>().HasData(
            new Amenity { AmenityId = 1, Name = "Parking" },
            new Amenity { AmenityId = 2, Name = "Gym" },
            new Amenity { AmenityId = 3, Name = "Pool" },
            new Amenity { AmenityId = 4, Name = "WiFi" }
        );

        //Application Status
        modelBuilder.Entity<ApplicationStatus>().HasData(
            new ApplicationStatus { StatusId = 1, Name = "Submitted" },
            new ApplicationStatus { StatusId = 2, Name = "Screening" },
            new ApplicationStatus { StatusId = 3, Name = "Approved" },
            new ApplicationStatus { StatusId = 4, Name = "Rejected" }
        );

        //Lease Status
        
        modelBuilder.Entity<LeaseStatus>().HasData(
            new LeaseStatus { StatusId = 1, Name = "Draft" },
            new LeaseStatus { StatusId = 2, Name = "Active" },
            new LeaseStatus { StatusId = 3, Name = "Expired" },
            new LeaseStatus { StatusId = 4, Name = "Renewal" },
            new LeaseStatus { StatusId = 5, Name = "Terminated" }
        );

        // Properties
        modelBuilder.Entity<Property>().HasData(

            new Property
            {
                PropertyId = 1,
                Name = "Palm Heights",
                Location = "Manama",
                Description = "Luxury residential apartments",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 2,
                Name = "Seef Towers",
                Location = "Seef",
                Description = "Modern high-rise residential building",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 3,
                Name = "Marina Residences",
                Location = "Amwaj Islands",
                Description = "Waterfront luxury residences",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 4,
                Name = "Business Bay Offices",
                Location = "Diplomatic Area",
                Description = "Premium office spaces",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 5,
                Name = "Green Gardens",
                Location = "Riffa",
                Description = "Family-friendly villa compound",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 6,
                Name = "City View Apartments",
                Location = "Juffair",
                Description = "Affordable city apartments",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 7,
                Name = "Pearl Residency",
                Location = "Muharraq",
                Description = "Residential apartments near airport",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 8,
                Name = "Skyline Plaza",
                Location = "Seef",
                Description = "Mixed-use commercial property",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 9,
                Name = "Lagoon Villas",
                Location = "Durrat Al Bahrain",
                Description = "Luxury beachfront villas",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 10,
                Name = "University Residences",
                Location = "Isa Town",
                Description = "Student accommodation complex",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 11,
                Name = "Al Naseem Tower",
                Location = "Manama",
                Description = "High-end residential tower",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 12,
                Name = "Harbor Offices",
                Location = "Bahrain Bay",
                Description = "Corporate office building",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 13,
                Name = "Sunset Compound",
                Location = "Saar",
                Description = "Private residential compound",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 14,
                Name = "Royal Suites",
                Location = "Juffair",
                Description = "Luxury serviced apartments",
                ManagerId = 1
            },

            new Property
            {
                PropertyId = 15,
                Name = "Tech Park Offices",
                Location = "Hidd",
                Description = "Technology and startup offices",
                ManagerId = 1
            }
        );

        // Units
        modelBuilder.Entity<Unit>().HasData(

            new Unit
            {
                UnitId = 1,
                PropertyId = 1,
                UnitNumber = "A101",
                TypeId = 1,
                RentAmount = 450,
                StatusId = 1,
                Size = 120
            },

            new Unit
            {
                UnitId = 2,
                PropertyId = 1,
                UnitNumber = "A102",
                TypeId = 1,
                RentAmount = 470,
                StatusId = 2,
                Size = 125
            },

            new Unit
            {
                UnitId = 3,
                PropertyId = 2,
                UnitNumber = "B201",
                TypeId = 2,
                RentAmount = 350,
                StatusId = 1,
                Size = 90
            },

            new Unit
            {
                UnitId = 4,
                PropertyId = 2,
                UnitNumber = "B202",
                TypeId = 2,
                RentAmount = 360,
                StatusId = 3,
                Size = 92
            },

            new Unit
            {
                UnitId = 5,
                PropertyId = 3,
                UnitNumber = "C301",
                TypeId = 3,
                RentAmount = 1200,
                StatusId = 1,
                Size = 350
            },

            new Unit
            {
                UnitId = 6,
                PropertyId = 3,
                UnitNumber = "C302",
                TypeId = 3,
                RentAmount = 1250,
                StatusId = 2,
                Size = 360
            },

            new Unit
            {
                UnitId = 7,
                PropertyId = 4,
                UnitNumber = "OFF-1",
                TypeId = 4,
                RentAmount = 800,
                StatusId = 1,
                Size = 200
            },

            new Unit
            {
                UnitId = 8,
                PropertyId = 4,
                UnitNumber = "OFF-2",
                TypeId = 4,
                RentAmount = 850,
                StatusId = 4,
                Size = 220
            },

            new Unit
            {
                UnitId = 9,
                PropertyId = 5,
                UnitNumber = "V101",
                TypeId = 3,
                RentAmount = 1500,
                StatusId = 1,
                Size = 400
            },

            new Unit
            {
                UnitId = 10,
                PropertyId = 6,
                UnitNumber = "D401",
                TypeId = 1,
                RentAmount = 500,
                StatusId = 2,
                Size = 130
            },

            new Unit
            {
                UnitId = 11,
                PropertyId = 7,
                UnitNumber = "E501",
                TypeId = 2,
                RentAmount = 320,
                StatusId = 1,
                Size = 85
            },

            new Unit
            {
                UnitId = 12,
                PropertyId = 8,
                UnitNumber = "COM-1",
                TypeId = 4,
                RentAmount = 950,
                StatusId = 3,
                Size = 250
            },

            new Unit
            {
                UnitId = 13,
                PropertyId = 9,
                UnitNumber = "L101",
                TypeId = 3,
                RentAmount = 1800,
                StatusId = 1,
                Size = 500
            },

            new Unit
            {
                UnitId = 14,
                PropertyId = 10,
                UnitNumber = "STU-1",
                TypeId = 2,
                RentAmount = 280,
                StatusId = 1,
                Size = 70
            },

            new Unit
            {
                UnitId = 15,
                PropertyId = 11,
                UnitNumber = "F601",
                TypeId = 1,
                RentAmount = 650,
                StatusId = 2,
                Size = 150
            }
        );

        // Leases
        modelBuilder.Entity<Lease>().HasData(

            new Lease
            {
                LeaseId = 1,
                TenantId = 3,
                UnitId = 1,
                StartDate = new DateTime(2026, 1, 1),
                EndDate = new DateTime(2026, 12, 31),
                StatusId = 2,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 2,
                TenantId = 4,
                UnitId = 2,
                StartDate = new DateTime(2026, 2, 1),
                EndDate = new DateTime(2027, 1, 31),
                StatusId = 2,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 3,
                TenantId = 5,
                UnitId = 3,
                StartDate = new DateTime(2025, 6, 1),
                EndDate = new DateTime(2026, 5, 31),
                StatusId = 4,
                IsActive = false
            },

            new Lease
            {
                LeaseId = 4,
                TenantId = 6,
                UnitId = 4,
                StartDate = new DateTime(2026, 3, 1),
                EndDate = new DateTime(2027, 2, 28),
                StatusId = 2,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 5,
                TenantId = 7,
                UnitId = 5,
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 12, 31),
                StatusId = 5,
                IsActive = false
            },

            new Lease
            {
                LeaseId = 6,
                TenantId = 8,
                UnitId = 6,
                StartDate = new DateTime(2026, 4, 1),
                EndDate = new DateTime(2027, 3, 31),
                StatusId = 2,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 7,
                TenantId = 9,
                UnitId = 7,
                StartDate = new DateTime(2026, 5, 1),
                EndDate = new DateTime(2027, 4, 30),
                StatusId = 2,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 8,
                TenantId = 10,
                UnitId = 8,
                StartDate = new DateTime(2025, 7, 1),
                EndDate = new DateTime(2026, 6, 30),
                StatusId = 3,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 9,
                TenantId = 11,
                UnitId = 9,
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2027, 7, 31),
                StatusId = 2,
                IsActive = true
            },

            new Lease
            {
                LeaseId = 10,
                TenantId = 12,
                UnitId = 10,
                StartDate = new DateTime(2025, 10, 1),
                EndDate = new DateTime(2026, 9, 30),
                StatusId = 1,
                IsActive = false
            }
        );

        // Payments
        modelBuilder.Entity<Payment>().HasData(

            new Payment
            {
                PaymentId = 1,
                LeaseId = 1,
                MethodId = 1,
                Amount = 450,
                PaymentDate = new DateTime(2026, 1, 5),
                StatusId = 2,
                DueDate = new DateTime(2026, 1, 1),
                TransactionReference = "TXN-1001",
                CreatedAt = new DateTime(2026, 1, 5)
            },

            new Payment
            {
                PaymentId = 2,
                LeaseId = 2,
                MethodId = 2,
                Amount = 470,
                PaymentDate = new DateTime(2026, 2, 3),
                StatusId = 2,
                DueDate = new DateTime(2026, 2, 1),
                TransactionReference = "TXN-1002",
                CreatedAt = new DateTime(2026, 2, 3)
            },

            new Payment
            {
                PaymentId = 3,
                LeaseId = 3,
                MethodId = 3,
                Amount = 350,
                PaymentDate = null,
                StatusId = 1,
                DueDate = new DateTime(2026, 3, 1),
                TransactionReference = "TXN-1003",
                CreatedAt = new DateTime(2026, 2, 25)
            },

            new Payment
            {
                PaymentId = 4,
                LeaseId = 4,
                MethodId = 1,
                Amount = 360,
                PaymentDate = new DateTime(2026, 3, 7),
                StatusId = 2,
                DueDate = new DateTime(2026, 3, 1),
                TransactionReference = "TXN-1004",
                CreatedAt = new DateTime(2026, 3, 7)
            },

            new Payment
            {
                PaymentId = 5,
                LeaseId = 5,
                MethodId = 2,
                Amount = 1200,
                PaymentDate = null,
                StatusId = 4,
                DueDate = new DateTime(2026, 4, 1),
                TransactionReference = "TXN-1005",
                CreatedAt = new DateTime(2026, 3, 28)
            },

            new Payment
            {
                PaymentId = 6,
                LeaseId = 6,
                MethodId = 3,
                Amount = 1250,
                PaymentDate = new DateTime(2026, 4, 4),
                StatusId = 2,
                DueDate = new DateTime(2026, 4, 1),
                TransactionReference = "TXN-1006",
                CreatedAt = new DateTime(2026, 4, 4)
            },

            new Payment
            {
                PaymentId = 7,
                LeaseId = 7,
                MethodId = 1,
                Amount = 800,
                PaymentDate = null,
                StatusId = 1,
                DueDate = new DateTime(2026, 5, 1),
                TransactionReference = "TXN-1007",
                CreatedAt = new DateTime(2026, 4, 27)
            },

            new Payment
            {
                PaymentId = 8,
                LeaseId = 8,
                MethodId = 2,
                Amount = 850,
                PaymentDate = new DateTime(2026, 5, 6),
                StatusId = 2,
                DueDate = new DateTime(2026, 5, 1),
                TransactionReference = "TXN-1008",
                CreatedAt = new DateTime(2026, 5, 6)
            },

            new Payment
            {
                PaymentId = 9,
                LeaseId = 9,
                MethodId = 3,
                Amount = 1500,
                PaymentDate = null,
                StatusId = 3,
                DueDate = new DateTime(2026, 6, 1),
                TransactionReference = "TXN-1009",
                CreatedAt = new DateTime(2026, 5, 29)
            },

            new Payment
            {
                PaymentId = 10,
                LeaseId = 10,
                MethodId = 1,
                Amount = 500,
                PaymentDate = new DateTime(2026, 6, 2),
                StatusId = 2,
                DueDate = new DateTime(2026, 6, 1),
                TransactionReference = "TXN-1010",
                CreatedAt = new DateTime(2026, 6, 2)
            },

            new Payment
            {
                PaymentId = 11,
                LeaseId = 1,
                MethodId = 2,
                Amount = 450,
                PaymentDate = new DateTime(2026, 2, 5),
                StatusId = 2,
                DueDate = new DateTime(2026, 2, 1),
                TransactionReference = "TXN-1011",
                CreatedAt = new DateTime(2026, 2, 5)
            },

            new Payment
            {
                PaymentId = 12,
                LeaseId = 2,
                MethodId = 3,
                Amount = 470,
                PaymentDate = null,
                StatusId = 4,
                DueDate = new DateTime(2026, 3, 1),
                TransactionReference = "TXN-1012",
                CreatedAt = new DateTime(2026, 2, 27)
            },

            new Payment
            {
                PaymentId = 13,
                LeaseId = 4,
                MethodId = 1,
                Amount = 360,
                PaymentDate = new DateTime(2026, 4, 3),
                StatusId = 2,
                DueDate = new DateTime(2026, 4, 1),
                TransactionReference = "TXN-1013",
                CreatedAt = new DateTime(2026, 4, 3)
            },

            new Payment
            {
                PaymentId = 14,
                LeaseId = 6,
                MethodId = 2,
                Amount = 1250,
                PaymentDate = null,
                StatusId = 1,
                DueDate = new DateTime(2026, 5, 1),
                TransactionReference = "TXN-1014",
                CreatedAt = new DateTime(2026, 4, 28)
            },

            new Payment
            {
                PaymentId = 15,
                LeaseId = 8,
                MethodId = 3,
                Amount = 850,
                PaymentDate = new DateTime(2026, 6, 4),
                StatusId = 2,
                DueDate = new DateTime(2026, 6, 1),
                TransactionReference = "TXN-1015",
                CreatedAt = new DateTime(2026, 6, 4)
            }
        );

        // Maintenance Requests
        modelBuilder.Entity<MaintenanceRequest>().HasData(

            new MaintenanceRequest
            {
                RequestId = 1,
                TenantId = 3,
                UnitId = 1,
                CategoryId = 1, // Plumbing
                TicketNumber = "MR-1001",
                Title = "Leaking kitchen sink",
                Description = "Water leaking under the sink cabinet.",
                PriorityId = 2,
                StatusId = 1,
                CreatedAt = new DateTime(2026, 1, 5)
            },

            new MaintenanceRequest
            {
                RequestId = 2,
                TenantId = 4,
                UnitId = 2,
                CategoryId = 2, // Electrical
                TicketNumber = "MR-1002",
                Title = "Power outage in bedroom",
                Description = "Bedroom outlets are not working.",
                PriorityId = 3,
                StatusId = 2,
                CreatedAt = new DateTime(2026, 1, 8)
            },

            new MaintenanceRequest
            {
                RequestId = 3,
                TenantId = 5,
                UnitId = 3,
                CategoryId = 3, // HVAC
                TicketNumber = "MR-1003",
                Title = "Air conditioner leaking",
                Description = "AC leaking water continuously.",
                PriorityId = 2,
                StatusId = 1,
                CreatedAt = new DateTime(2026, 1, 12)
            },

            new MaintenanceRequest
            {
                RequestId = 4,
                TenantId = 6,
                UnitId = 4,
                CategoryId = 1, // Plumbing
                TicketNumber = "MR-1004",
                Title = "Bathroom pipe blockage",
                Description = "Drain water backing up.",
                PriorityId = 3,
                StatusId = 2,
                CreatedAt = new DateTime(2026, 1, 15)
            },

            new MaintenanceRequest
            {
                RequestId = 5,
                TenantId = 7,
                UnitId = 5,
                CategoryId = 4, // General Maintenance
                TicketNumber = "MR-1005",
                Title = "Broken door lock",
                Description = "Front door lock jammed.",
                PriorityId = 2,
                StatusId = 3,
                CreatedAt = new DateTime(2026, 1, 18),
                CompletedAt = new DateTime(2026, 1, 20)
            },

            new MaintenanceRequest
            {
                RequestId = 6,
                TenantId = 8,
                UnitId = 6,
                CategoryId = 2, // Electrical
                TicketNumber = "MR-1006",
                Title = "Flickering lights",
                Description = "Living room lights flickering.",
                PriorityId = 1,
                StatusId = 1,
                CreatedAt = new DateTime(2026, 1, 21)
            },

            new MaintenanceRequest
            {
                RequestId = 7,
                TenantId = 9,
                UnitId = 7,
                CategoryId = 4, // General Maintenance
                TicketNumber = "MR-1007",
                Title = "Loose cabinet door",
                Description = "Kitchen cabinet hinge is loose.",
                PriorityId = 1,
                StatusId = 2,
                CreatedAt = new DateTime(2026, 1, 23)
            },

            new MaintenanceRequest
            {
                RequestId = 8,
                TenantId = 10,
                UnitId = 8,
                CategoryId = 3, // HVAC
                TicketNumber = "MR-1008",
                Title = "AC not cooling",
                Description = "Cooling system stopped working.",
                PriorityId = 3,
                StatusId = 1,
                CreatedAt = new DateTime(2026, 1, 25)
            },

            new MaintenanceRequest
            {
                RequestId = 9,
                TenantId = 11,
                UnitId = 9,
                CategoryId = 4, // General Maintenance
                TicketNumber = "MR-1009",
                Title = "Wall repaint request",
                Description = "Bedroom wall paint peeling.",
                PriorityId = 1,
                StatusId = 3,
                CreatedAt = new DateTime(2026, 1, 27),
                CompletedAt = new DateTime(2026, 1, 30)
            },

            new MaintenanceRequest
            {
                RequestId = 10,
                TenantId = 12,
                UnitId = 10,
                CategoryId = 1, // Plumbing
                TicketNumber = "MR-1010",
                Title = "Toilet leaking",
                Description = "Water leaking around toilet base.",
                PriorityId = 2,
                StatusId = 1,
                CreatedAt = new DateTime(2026, 2, 1)
            }
        );

        modelBuilder.Entity<MaintenanceAssignment>().HasData(

    new MaintenanceAssignment
    {
        AssignmentId = 1,
        RequestId = 1,
        StaffId = 13,
        AssignedDate = new DateTime(2026, 1, 5)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 2,
        RequestId = 2,
        StaffId = 14,
        AssignedDate = new DateTime(2026, 1, 8)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 3,
        RequestId = 3,
        StaffId = 15,
        AssignedDate = new DateTime(2026, 1, 12)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 4,
        RequestId = 4,
        StaffId = 16,
        AssignedDate = new DateTime(2026, 1, 15)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 5,
        RequestId = 5,
        StaffId = 17,
        AssignedDate = new DateTime(2026, 1, 18)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 6,
        RequestId = 6,
        StaffId = 13,
        AssignedDate = new DateTime(2026, 1, 21)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 7,
        RequestId = 7,
        StaffId = 14,
        AssignedDate = new DateTime(2026, 1, 23)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 8,
        RequestId = 8,
        StaffId = 15,
        AssignedDate = new DateTime(2026, 1, 25)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 9,
        RequestId = 9,
        StaffId = 16,
        AssignedDate = new DateTime(2026, 1, 27)
    },

    new MaintenanceAssignment
    {
        AssignmentId = 10,
        RequestId = 10,
        StaffId = 17,
        AssignedDate = new DateTime(2026, 2, 1)
    }
);

        modelBuilder.Entity<Application>().HasData(

    new Application
    {
        ApplicationId = 1,
        TenantId = 3,
        UnitId = 11,
        ApplicationDate = new DateTime(2026, 1, 5),
        StatusId = 1,
        CreatedAt = new DateTime(2026, 1, 5)
    },

    new Application
    {
        ApplicationId = 2,
        TenantId = 4,
        UnitId = 12,
        ApplicationDate = new DateTime(2026, 1, 7),
        StatusId = 2,
        CreatedAt = new DateTime(2026, 1, 7)
    },

    new Application
    {
        ApplicationId = 3,
        TenantId = 5,
        UnitId = 13,
        ApplicationDate = new DateTime(2026, 1, 10),
        StatusId = 3,
        CreatedAt = new DateTime(2026, 1, 10)
    },

    new Application
    {
        ApplicationId = 4,
        TenantId = 6,
        UnitId = 14,
        ApplicationDate = new DateTime(2026, 1, 12),
        StatusId = 1,
        CreatedAt = new DateTime(2026, 1, 12)
    },

    new Application
    {
        ApplicationId = 5,
        TenantId = 7,
        UnitId = 15,
        ApplicationDate = new DateTime(2026, 1, 15),
        StatusId = 2,
        CreatedAt = new DateTime(2026, 1, 15)
    },

    new Application
    {
        ApplicationId = 6,
        TenantId = 8,
        UnitId = 11,
        ApplicationDate = new DateTime(2026, 1, 18),
        StatusId = 3,
        CreatedAt = new DateTime(2026, 1, 18)
    },

    new Application
    {
        ApplicationId = 7,
        TenantId = 9,
        UnitId = 12,
        ApplicationDate = new DateTime(2026, 1, 20),
        StatusId = 1,
        CreatedAt = new DateTime(2026, 1, 20)
    },

    new Application
    {
        ApplicationId = 8,
        TenantId = 10,
        UnitId = 13,
        ApplicationDate = new DateTime(2026, 1, 22),
        StatusId = 2,
        CreatedAt = new DateTime(2026, 1, 22)
    },

    new Application
    {
        ApplicationId = 9,
        TenantId = 11,
        UnitId = 14,
        ApplicationDate = new DateTime(2026, 1, 25),
        StatusId = 1,
        CreatedAt = new DateTime(2026, 1, 25)
    },

    new Application
    {
        ApplicationId = 10,
        TenantId = 12,
        UnitId = 15,
        ApplicationDate = new DateTime(2026, 1, 28),
        StatusId = 3,
        CreatedAt = new DateTime(2026, 1, 28)
    }
);
        modelBuilder.Entity<Unit>()
    .HasMany(u => u.Amenities)
    .WithMany(a => a.Units)
    .UsingEntity(j => j.HasData(

        new { UnitId = 1, AmenityId = 1 },
        new { UnitId = 1, AmenityId = 4 },

        new { UnitId = 2, AmenityId = 2 },
        new { UnitId = 2, AmenityId = 4 },

        new { UnitId = 3, AmenityId = 1 },
        new { UnitId = 3, AmenityId = 3 },

        new { UnitId = 4, AmenityId = 2 },
        new { UnitId = 4, AmenityId = 3 },
        new { UnitId = 4, AmenityId = 4 },

        new { UnitId = 5, AmenityId = 1 },

        new { UnitId = 6, AmenityId = 4 },

        new { UnitId = 7, AmenityId = 1 },
        new { UnitId = 7, AmenityId = 2 },

        new { UnitId = 8, AmenityId = 3 },

        new { UnitId = 9, AmenityId = 1 },
        new { UnitId = 9, AmenityId = 4 },

        new { UnitId = 10, AmenityId = 2 },
        new { UnitId = 10, AmenityId = 3 }

    ));
     
        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
