using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaseBridge.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Amenitie__842AF50BC6F1CC26", x => x.AmenityId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__C8EE206385352328", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppUsers__1788CC4C4ADB3655", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "LeaseStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LeaseSta__C8EE20637552008F", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__19093A0BCD2F05BF", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__C8EE20636D4895EC", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    MethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__FC681851F4CF137C", x => x.MethodId);
                });

            migrationBuilder.CreateTable(
                name: "PriorityTypes",
                columns: table => new
                {
                    PriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Priority__D0A3D0BEAE1C0E68", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skills__DFA0918772BE8E6B", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "UnitStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UnitStat__C8EE2063A09CE406", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UnitType__516F03B52D43BEAD", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Properti__70C9A735812141E8", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Manager",
                        column: x => x.ManagerId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffSkills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffSkills", x => new { x.StaffId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_StaffSkills_Category",
                        column: x => x.CategoryId,
                        principalTable: "MaintenanceCategories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_StaffSkills_Skill",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId");
                    table.ForeignKey(
                        name: "FK_StaffSkills_Staff",
                        column: x => x.StaffId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Units__44F5ECB524C2F27E", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK__Units__PropertyI__4E88ABD4",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId");
                    table.ForeignKey(
                        name: "FK__Units__StatusId__5070F446",
                        column: x => x.StatusId,
                        principalTable: "UnitStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK__Units__TypeId__4F7CD00D",
                        column: x => x.TypeId,
                        principalTable: "UnitTypes",
                        principalColumn: "TypeId");
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__C93A4C99F7AA0BFA", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK__Applicati__Statu__534D60F1",
                        column: x => x.StatusId,
                        principalTable: "ApplicationStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK__Applicati__Tenan__5165187F",
                        column: x => x.TenantId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Applicati__UnitI__52593CB8",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.CreateTable(
                name: "Leases",
                columns: table => new
                {
                    LeaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leases__21FA58C114FB33A2", x => x.LeaseId);
                    table.ForeignKey(
                        name: "FK__Leases__StatusId__5629CD9C",
                        column: x => x.StatusId,
                        principalTable: "LeaseStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK__Leases__TenantId__5441852A",
                        column: x => x.TenantId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Leases__UnitId__5535A963",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TicketNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__33A8517A6EB8F73D", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK__Maintenan__Categ__59063A47",
                        column: x => x.CategoryId,
                        principalTable: "MaintenanceCategories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK__Maintenan__Prior__59FA5E80",
                        column: x => x.PriorityId,
                        principalTable: "PriorityTypes",
                        principalColumn: "PriorityId");
                    table.ForeignKey(
                        name: "FK__Maintenan__Statu__5AEE82B9",
                        column: x => x.StatusId,
                        principalTable: "MaintenanceStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK__Maintenan__Tenan__571DF1D5",
                        column: x => x.TenantId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Maintenan__UnitI__5812160E",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.CreateTable(
                name: "UnitAmenities",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UnitAmen__ECB743E5CEF7A8AA", x => new { x.UnitId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK__UnitAmeni__Ameni__5F7E2DAC",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "AmenityId");
                    table.ForeignKey(
                        name: "FK__UnitAmeni__UnitI__5E8A0973",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.CreateTable(
                name: "UnitImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UnitImag__7516F70C8FA81AA8", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK__UnitImage__UnitI__55F4C372",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaseId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Status",
                        column: x => x.StatusId,
                        principalTable: "InvoiceStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_Invoices_Leases_LeaseId",
                        column: x => x.LeaseId,
                        principalTable: "Leases",
                        principalColumn: "LeaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDD6B9779A51", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_Request",
                        column: x => x.RequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK__Feedback__Tenant__6754599E",
                        column: x => x.TenantId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__32499E7757CAB96A", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK__Maintenan__Reque__5DCAEF64",
                        column: x => x.RequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK__Maintenan__Staff__5EBF139D",
                        column: x => x.StaffId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__442C64BEB509A6F4", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK__Maintenan__Reque__58D1301D",
                        column: x => x.RequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceUpdates",
                columns: table => new
                {
                    UpdateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    OldStatusId = table.Column<int>(type: "int", nullable: true),
                    NewStatusId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__7A0CF3C55F5D63B3", x => x.UpdateId);
                    table.ForeignKey(
                        name: "FK_MU_NewStatus",
                        column: x => x.NewStatusId,
                        principalTable: "MaintenanceStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_MU_OldStatus",
                        column: x => x.OldStatusId,
                        principalTable: "MaintenanceStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_MU_User",
                        column: x => x.UpdatedBy,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Maintenan__Reque__5BE2A6F2",
                        column: x => x.RequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceRequestId = table.Column<int>(type: "int", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    NotificationType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValue: "InApp"),
                    TargetUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E1255D49916", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK__Notificat__Appli__66603565",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId");
                    table.ForeignKey(
                        name: "FK__Notificat__Maint__656C112C",
                        column: x => x.MaintenanceRequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__6477ECF3",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    MethodId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransactionReference = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Method",
                        column: x => x.MethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "MethodId");
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "AmenityId", "Name" },
                values: new object[,]
                {
                    { 1, "Parking" },
                    { 2, "Gym" },
                    { 3, "Pool" },
                    { 4, "WiFi" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserId", "Email", "FirstName", "IdentityUserId", "IsAvailable", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "manager@test.com", "Manager", null, true, "User", "33330001" },
                    { 2, "staff@test.com", "Staff", null, true, "User", "33330002" },
                    { 3, "tenant@test.com", "Tenant", null, true, "User", "33330003" },
                    { 4, "noor.ali@gmail.com", "Noor", null, true, "Ali", "33330004" },
                    { 5, "khalid.hasan@gmail.com", "Khalid", null, true, "Hasan", "33330005" },
                    { 6, "fatima.mahmood@gmail.com", "Fatima", null, true, "Mahmood", "33330006" },
                    { 7, "ahmed.yousif@gmail.com", "Ahmed", null, true, "Yousif", "33330007" },
                    { 8, "layla.ibrahim@gmail.com", "Layla", null, true, "Ibrahim", "33330008" },
                    { 9, "yousef.saleh@gmail.com", "Yousef", null, true, "Saleh", "33330009" },
                    { 10, "mariam.adel@gmail.com", "Mariam", null, true, "Adel", "33330010" },
                    { 11, "hassan.nasser@gmail.com", "Hassan", null, true, "Nasser", "33330011" },
                    { 12, "zainab.kareem@gmail.com", "Zainab", null, true, "Kareem", "33330012" },
                    { 13, "omar.rahman@gmail.com", "Omar", null, true, "Rahman", "33330013" },
                    { 14, "salman.jaber@gmail.com", "Salman", null, true, "Jaber", "33330014" },
                    { 15, "huda.faisal@gmail.com", "Huda", null, true, "Faisal", "33330015" },
                    { 16, "mahmood.karim@gmail.com", "Mahmood", null, true, "Karim", "33330016" },
                    { 17, "reem.nasser@gmail.com", "Reem", null, true, "Nasser", "33330017" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Submitted" },
                    { 2, "Screening" },
                    { 3, "Approved" },
                    { 4, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Paid" },
                    { 3, "Overdue" }
                });

            migrationBuilder.InsertData(
                table: "LeaseStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Active" },
                    { 3, "Expired" },
                    { 4, "Renewal" },
                    { 5, "Terminated" }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceCategories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Plumbing" },
                    { 2, "Electrical" },
                    { 3, "HVAC" },
                    { 4, "General Maintenance" }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Submitted" },
                    { 2, "Assigned" },
                    { 3, "In Progress" },
                    { 4, "Resolved" },
                    { 5, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "MethodId", "Name" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Card" },
                    { 3, "Bank Transfer" },
                    { 4, "BenefitPay" }
                });

            migrationBuilder.InsertData(
                table: "PriorityTypes",
                columns: new[] { "PriorityId", "Name" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Name" },
                values: new object[,]
                {
                    { 1, "Plumbing" },
                    { 2, "Electrical" },
                    { 3, "HVAC" },
                    { 4, "Carpentry" },
                    { 5, "Painting" }
                });

            migrationBuilder.InsertData(
                table: "UnitStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Available" },
                    { 2, "Reserved" },
                    { 3, "Occupied" },
                    { 4, "UnderMaintenance" },
                    { 5, "Pending Inspection" }
                });

            migrationBuilder.InsertData(
                table: "UnitTypes",
                columns: new[] { "TypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Apartment" },
                    { 2, "Studio" },
                    { 3, "Villa" },
                    { 4, "Office" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Description", "Location", "ManagerId", "Name" },
                values: new object[,]
                {
                    { 1, "Luxury residential apartments", "Manama", 1, "Palm Heights" },
                    { 2, "Modern high-rise residential building", "Seef", 1, "Seef Towers" },
                    { 3, "Waterfront luxury residences", "Amwaj Islands", 1, "Marina Residences" },
                    { 4, "Premium office spaces", "Diplomatic Area", 1, "Business Bay Offices" },
                    { 5, "Family-friendly villa compound", "Riffa", 1, "Green Gardens" },
                    { 6, "Affordable city apartments", "Juffair", 1, "City View Apartments" },
                    { 7, "Residential apartments near airport", "Muharraq", 1, "Pearl Residency" },
                    { 8, "Mixed-use commercial property", "Seef", 1, "Skyline Plaza" },
                    { 9, "Luxury beachfront villas", "Durrat Al Bahrain", 1, "Lagoon Villas" },
                    { 10, "Student accommodation complex", "Isa Town", 1, "University Residences" },
                    { 11, "High-end residential tower", "Manama", 1, "Al Naseem Tower" },
                    { 12, "Corporate office building", "Bahrain Bay", 1, "Harbor Offices" },
                    { 13, "Private residential compound", "Saar", 1, "Sunset Compound" },
                    { 14, "Luxury serviced apartments", "Juffair", 1, "Royal Suites" },
                    { 15, "Technology and startup offices", "Hidd", 1, "Tech Park Offices" }
                });

            migrationBuilder.InsertData(
                table: "StaffSkills",
                columns: new[] { "SkillId", "StaffId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 13, 1 },
                    { 2, 14, 2 },
                    { 3, 15, 3 },
                    { 4, 16, 4 },
                    { 5, 17, 4 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "PropertyId", "RentAmount", "Size", "StatusId", "TypeId", "UnitNumber" },
                values: new object[,]
                {
                    { 1, 1, 450m, 120m, 1, 1, "A101" },
                    { 2, 1, 470m, 125m, 2, 1, "A102" },
                    { 3, 2, 350m, 90m, 1, 2, "B201" },
                    { 4, 2, 360m, 92m, 3, 2, "B202" },
                    { 5, 3, 1200m, 350m, 1, 3, "C301" },
                    { 6, 3, 1250m, 360m, 2, 3, "C302" },
                    { 7, 4, 800m, 200m, 1, 4, "OFF-1" },
                    { 8, 4, 850m, 220m, 4, 4, "OFF-2" },
                    { 9, 5, 1500m, 400m, 1, 3, "V101" },
                    { 10, 6, 500m, 130m, 2, 1, "D401" },
                    { 11, 7, 320m, 85m, 1, 2, "E501" },
                    { 12, 8, 950m, 250m, 3, 4, "COM-1" },
                    { 13, 9, 1800m, 500m, 1, 3, "L101" },
                    { 14, 10, 280m, 70m, 1, 2, "STU-1" },
                    { 15, 11, 650m, 150m, 2, 1, "F601" }
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "ApplicationDate", "CreatedAt", "StatusId", "TenantId", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 11, null },
                    { 2, new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 12, null },
                    { 3, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 13, null },
                    { 4, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, 14, null },
                    { 5, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 15, null },
                    { 6, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, 11, null },
                    { 7, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, 12, null },
                    { 8, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, 13, null },
                    { 9, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11, 14, null },
                    { 10, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 12, 15, null }
                });

            migrationBuilder.InsertData(
                table: "Leases",
                columns: new[] { "LeaseId", "EndDate", "IsActive", "StartDate", "StatusId", "TenantId", "UnitId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 1 },
                    { 2, new DateTime(2027, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 2 },
                    { 3, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 3 },
                    { 4, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, 4 },
                    { 5, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 7, 5 },
                    { 6, new DateTime(2027, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, 6 },
                    { 7, new DateTime(2027, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, 7 },
                    { 8, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10, 8 },
                    { 9, new DateTime(2027, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 11, 9 },
                    { 10, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12, 10 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceRequests",
                columns: new[] { "RequestId", "CategoryId", "CompletedAt", "CreatedAt", "Description", "PriorityId", "StatusId", "TenantId", "TicketNumber", "Title", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water leaking under the sink cabinet.", 2, 1, 3, "MR-1001", "Leaking kitchen sink", 1, null },
                    { 2, 2, null, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bedroom outlets are not working.", 3, 2, 4, "MR-1002", "Power outage in bedroom", 2, null },
                    { 3, 3, null, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "AC leaking water continuously.", 2, 1, 5, "MR-1003", "Air conditioner leaking", 3, null },
                    { 4, 1, null, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drain water backing up.", 3, 2, 6, "MR-1004", "Bathroom pipe blockage", 4, null },
                    { 5, 4, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Front door lock jammed.", 2, 3, 7, "MR-1005", "Broken door lock", 5, null },
                    { 6, 2, null, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Living room lights flickering.", 1, 1, 8, "MR-1006", "Flickering lights", 6, null },
                    { 7, 4, null, new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kitchen cabinet hinge is loose.", 1, 2, 9, "MR-1007", "Loose cabinet door", 7, null },
                    { 8, 3, null, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cooling system stopped working.", 3, 1, 10, "MR-1008", "AC not cooling", 8, null },
                    { 9, 4, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bedroom wall paint peeling.", 1, 3, 11, "MR-1009", "Wall repaint request", 9, null },
                    { 10, 1, null, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water leaking around toilet base.", 2, 1, 12, "MR-1010", "Toilet leaking", 10, null }
                });

            migrationBuilder.InsertData(
                table: "UnitAmenities",
                columns: new[] { "AmenityId", "UnitId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 2, 2 },
                    { 4, 2 },
                    { 1, 3 },
                    { 3, 3 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 },
                    { 1, 5 },
                    { 4, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 8 },
                    { 1, 9 },
                    { 4, 9 },
                    { 2, 10 },
                    { 3, 10 }
                });

            migrationBuilder.InsertData(
                table: "UnitImages",
                columns: new[] { "ImageId", "ImageUrl", "UnitId" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85", 1 },
                    { 2, "https://images.unsplash.com/photo-1494526585095-c41746248156", 2 },
                    { 3, "https://images.unsplash.com/photo-1484154218962-a197022b5858", 3 },
                    { 4, "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688", 4 }
                });

            migrationBuilder.InsertData(
                table: "Feedback",
                columns: new[] { "FeedbackId", "CreatedAt", "Message", "Rating", "RequestId", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintenance team was quick and professional.", 5, 5, 7 },
                    { 2, new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Painting quality was very good.", 4, 9, 11 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "Amount", "DueDate", "InvoiceNumber", "IssuedDate", "LeaseId", "StatusId" },
                values: new object[,]
                {
                    { 1, 450.00m, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 2, 600.00m, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1002", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, 700.00m, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1003", new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 4, 360.00m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1004", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2 },
                    { 5, 1200.00m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1005", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3 },
                    { 6, 1250.00m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1006", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2 },
                    { 7, 800.00m, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1007", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1 },
                    { 8, 850.00m, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1008", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2 },
                    { 9, 1500.00m, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1009", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1 },
                    { 10, 500.00m, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-1010", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceAssignments",
                columns: new[] { "AssignmentId", "AssignedDate", "RequestId", "StaffId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 },
                    { 2, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 14 },
                    { 3, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 15 },
                    { 4, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 16 },
                    { 5, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 17 },
                    { 6, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 13 },
                    { 7, new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 14 },
                    { 8, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 15 },
                    { 9, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 16 },
                    { 10, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 17 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceAttachments",
                columns: new[] { "AttachmentId", "FileUrl", "RequestId" },
                values: new object[,]
                {
                    { 1, "https://example.com/leak-photo.jpg", 1 },
                    { 2, "https://example.com/door-lock.jpg", 5 },
                    { 3, "https://example.com/wall-paint.jpg", 9 }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceUpdates",
                columns: new[] { "UpdateId", "CreatedAt", "NewStatusId", "Notes", "OldStatusId", "RequestId", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Assigned to plumbing staff.", 1, 1, 13 },
                    { 2, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Door lock repaired successfully.", 2, 5, 17 },
                    { 3, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Wall repaint completed.", 3, 9, 16 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "CreatedAt", "InvoiceId", "MethodId", "PaymentDate", "TransactionReference", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 450m, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1001", null },
                    { 2, 470m, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1002", null },
                    { 3, 350m, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1003", null },
                    { 4, 360m, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1004", null },
                    { 5, 1200m, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, new DateTime(2026, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1005", null },
                    { 6, 1250m, new DateTime(2026, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, new DateTime(2026, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1006", null },
                    { 7, 800m, new DateTime(2026, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1007", null },
                    { 8, 850m, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1008", null },
                    { 9, 1500m, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1009", null },
                    { 10, 500m, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TXN-1010", null }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Amenitie__737584F62D4066A9",
                table: "Amenities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StatusId",
                table: "Applications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_TenantId",
                table: "Applications",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UnitId",
                table: "Applications",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "UQ_AppUsers_IdentityUserId",
                table: "AppUsers",
                column: "IdentityUserId",
                unique: true,
                filter: "[IdentityUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_RequestId",
                table: "Feedback",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_TenantId",
                table: "Feedback",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_LeaseId",
                table: "Invoices",
                column: "LeaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StatusId",
                table: "Invoices",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_StatusId",
                table: "Leases",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantId",
                table: "Leases",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_UnitId",
                table: "Leases",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "UQ_LeaseStatus_Name",
                table: "LeaseStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceAssignments_StaffId",
                table: "MaintenanceAssignments",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "UQ_Request_Staff",
                table: "MaintenanceAssignments",
                columns: new[] { "RequestId", "StaffId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceAttachments_RequestId",
                table: "MaintenanceAttachments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "UQ_MaintenanceCategories_Name",
                table: "MaintenanceCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_CategoryId",
                table: "MaintenanceRequests",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_PriorityId",
                table: "MaintenanceRequests",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_StatusId",
                table: "MaintenanceRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_TenantId",
                table: "MaintenanceRequests",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_UnitId",
                table: "MaintenanceRequests",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "UQ__Maintena__CBED06DAB3AAE172",
                table: "MaintenanceRequests",
                column: "TicketNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Ticket",
                table: "MaintenanceRequests",
                column: "TicketNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_MaintenanceStatus_Name",
                table: "MaintenanceStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceUpdates_NewStatusId",
                table: "MaintenanceUpdates",
                column: "NewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceUpdates_OldStatusId",
                table: "MaintenanceUpdates",
                column: "OldStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceUpdates_RequestId",
                table: "MaintenanceUpdates",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceUpdates_UpdatedBy",
                table: "MaintenanceUpdates",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ApplicationId",
                table: "Notifications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MaintenanceRequestId",
                table: "Notifications",
                column: "MaintenanceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_PaymentMethods_Name",
                table: "PaymentMethods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MethodId",
                table: "Payments",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionReference",
                table: "Payments",
                column: "TransactionReference",
                unique: true,
                filter: "[TransactionReference] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_PriorityTypes_Name",
                table: "PriorityTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ManagerId",
                table: "Properties",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "UQ__Skills__737584F651195CDD",
                table: "Skills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkills_CategoryId",
                table: "StaffSkills",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffSkills_SkillId",
                table: "StaffSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitAmenities_AmenityId",
                table: "UnitAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitImages_UnitId",
                table: "UnitImages",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropertyId",
                table: "Units",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_StatusId",
                table: "Units",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_TypeId",
                table: "Units",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_UnitStatus_Name",
                table: "UnitStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UnitTypes_Name",
                table: "UnitTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "MaintenanceAssignments");

            migrationBuilder.DropTable(
                name: "MaintenanceAttachments");

            migrationBuilder.DropTable(
                name: "MaintenanceUpdates");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "StaffSkills");

            migrationBuilder.DropTable(
                name: "UnitAmenities");

            migrationBuilder.DropTable(
                name: "UnitImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "MaintenanceRequests");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "ApplicationStatus");

            migrationBuilder.DropTable(
                name: "MaintenanceCategories");

            migrationBuilder.DropTable(
                name: "PriorityTypes");

            migrationBuilder.DropTable(
                name: "MaintenanceStatus");

            migrationBuilder.DropTable(
                name: "InvoiceStatus");

            migrationBuilder.DropTable(
                name: "Leases");

            migrationBuilder.DropTable(
                name: "LeaseStatus");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "UnitStatus");

            migrationBuilder.DropTable(
                name: "UnitTypes");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
