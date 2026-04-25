using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LeaseBridge.API.Models;

public partial class ApplicationDbContext : DbContext
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

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LeaseBridgeDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
