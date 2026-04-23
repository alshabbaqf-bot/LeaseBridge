using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace LeaseBridge.API.Models;

public partial class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<LeaseStatus> LeaseStatuses { get; set; }

    public virtual DbSet<MaintenanceAssignment> MaintenanceAssignments { get; set; }

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

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StaffSkill> StaffSkills { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitStatus> UnitStatuses { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__AppUsers__1788CC4C4ADB3655");

            entity.HasIndex(e => e.IdentityUserId, "UQ_AppUsers_IdentityUserId").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AppUsers__RoleId__4D94879B");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4C99F7AA0BFA");

            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");

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

            entity.HasOne(d => d.Request).WithMany(p => p.MaintenanceUpdates)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Reque__5BE2A6F2");

            entity.HasOne(d => d.Status).WithMany(p => p.MaintenanceUpdates)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Statu__5CD6CB2B");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1255D49916");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(255);

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

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

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
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1ACDC799A5");

            entity.HasIndex(e => e.Name, "UQ_Roles_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<StaffSkill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__StaffSki__DFA09187A563FF8B");

            entity.HasIndex(e => new { e.StaffId, e.CategoryId }, "UQ_Staff_Skill").IsUnique();

            entity.HasOne(d => d.Category).WithMany(p => p.StaffSkills)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StaffSkil__Categ__60A75C0F");

            entity.HasOne(d => d.Staff).WithMany(p => p.StaffSkills)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StaffSkil__Staff__5FB337D6");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Units__44F5ECB524C2F27E");

            entity.Property(e => e.RentAmount).HasColumnType("decimal(10, 2)");
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
