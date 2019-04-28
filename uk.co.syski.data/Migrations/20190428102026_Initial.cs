using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Syski.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserSystemCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSystemCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Architectures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Architectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatingSystemModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystemModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageInterfaceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageInterfaceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemTypeNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTypeNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "AuthenticationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TokenType = table.Column<string>(nullable: true),
                    Issuer = table.Column<string>(nullable: true),
                    Audience = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    NotBefore = table.Column<DateTime>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    NextTokenId = table.Column<Guid>(nullable: true),
                    PreviousTokenId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthenticationTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BIOSModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManufacturerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIOSModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BIOSModels_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ManufacturerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CPUModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: true),
                    ArchitectureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUModels_Architectures_ArchitectureId",
                        column: x => x.ArchitectureId,
                        principalTable: "Architectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CPUModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GPUModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherboardModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RAMModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: true),
                    Size = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAMModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorageModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: true),
                    Size = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HostName = table.Column<string>(nullable: true),
                    ModelId = table.Column<Guid>(nullable: true),
                    Secret = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Systems_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSystems",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SystemId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSystems", x => new { x.UserId, x.SystemId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSystems_ApplicationUserSystemCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ApplicationUserSystemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSystems_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSystems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemBIOSs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    BIOSModelId = table.Column<Guid>(nullable: false),
                    Caption = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemBIOSs", x => x.SystemId);
                    table.ForeignKey(
                        name: "FK_SystemBIOSs_BIOSModels_BIOSModelId",
                        column: x => x.BIOSModelId,
                        principalTable: "BIOSModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemBIOSs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemCommands",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    QueuedTime = table.Column<DateTime>(nullable: false),
                    ExecutedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCommands", x => new { x.SystemId, x.QueuedTime });
                    table.ForeignKey(
                        name: "FK_SystemCommands_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemCPUs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    CPUModelID = table.Column<Guid>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    ClockSpeed = table.Column<int>(nullable: false),
                    CoreCount = table.Column<int>(nullable: false),
                    ThreadCount = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCPUs", x => new { x.SystemId, x.CPUModelID, x.Slot });
                    table.ForeignKey(
                        name: "FK_SystemCPUs_CPUModels_CPUModelID",
                        column: x => x.CPUModelID,
                        principalTable: "CPUModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemCPUs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemCPUsData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Load = table.Column<double>(nullable: false),
                    Processes = table.Column<int>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCPUsData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemCPUsData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemGPUs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    GPUModelId = table.Column<Guid>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGPUs", x => new { x.SystemId, x.GPUModelId, x.Slot });
                    table.ForeignKey(
                        name: "FK_SystemGPUs_GPUModels_GPUModelId",
                        column: x => x.GPUModelId,
                        principalTable: "GPUModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemGPUs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemMotherboards",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    MotherboardModelId = table.Column<Guid>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMotherboards", x => x.SystemId);
                    table.ForeignKey(
                        name: "FK_SystemMotherboards_MotherboardModels_MotherboardModelId",
                        column: x => x.MotherboardModelId,
                        principalTable: "MotherboardModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemMotherboards_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemOSs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    OperatingSystemId = table.Column<Guid>(nullable: false),
                    ArchitectureId = table.Column<Guid>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOSs", x => new { x.SystemId, x.OperatingSystemId });
                    table.ForeignKey(
                        name: "FK_SystemOSs_Architectures_ArchitectureId",
                        column: x => x.ArchitectureId,
                        principalTable: "Architectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemOSs_OperatingSystemModels_OperatingSystemId",
                        column: x => x.OperatingSystemId,
                        principalTable: "OperatingSystemModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemOSs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemPingData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    SendPingTime = table.Column<DateTime>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPingData", x => new { x.SystemId, x.SendPingTime });
                    table.ForeignKey(
                        name: "FK_SystemPingData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemRAMData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Free = table.Column<int>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRAMData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemRAMData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemRAMs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    RAMModelId = table.Column<Guid>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRAMs", x => new { x.SystemId, x.RAMModelId, x.Slot });
                    table.ForeignKey(
                        name: "FK_SystemRAMs_RAMModels_RAMModelId",
                        column: x => x.RAMModelId,
                        principalTable: "RAMModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemRAMs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemRunningProcesses",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MemSize = table.Column<long>(nullable: false),
                    KernelTime = table.Column<long>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Threads = table.Column<int>(nullable: false),
                    UpTime = table.Column<long>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRunningProcesses", x => new { x.SystemId, x.Id, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemRunningProcesses_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemStorageData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Time = table.Column<float>(nullable: false),
                    Transfers = table.Column<float>(nullable: false),
                    Reads = table.Column<float>(nullable: false),
                    Writes = table.Column<float>(nullable: false),
                    ByteReads = table.Column<float>(nullable: false),
                    ByteWrites = table.Column<float>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStorageData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemStorageData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemStorages",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    StorageModelId = table.Column<Guid>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    StorageInterfaceId = table.Column<Guid>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStorages", x => new { x.SystemId, x.StorageModelId, x.Slot });
                    table.ForeignKey(
                        name: "FK_SystemStorages_StorageInterfaceTypes_StorageInterfaceId",
                        column: x => x.StorageInterfaceId,
                        principalTable: "StorageInterfaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemStorages_StorageModels_StorageModelId",
                        column: x => x.StorageModelId,
                        principalTable: "StorageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemStorages_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemTypes",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTypes", x => new { x.SystemId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_SystemTypes_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemTypes_SystemTypeNames_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SystemTypeNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSystems_CategoryId",
                table: "ApplicationUserSystems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSystems_SystemId",
                table: "ApplicationUserSystems",
                column: "SystemId");

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
                name: "IX_AuthenticationTokens_UserId",
                table: "AuthenticationTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BIOSModels_ManufacturerId",
                table: "BIOSModels",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUModels_ArchitectureId",
                table: "CPUModels",
                column: "ArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUModels_ModelId",
                table: "CPUModels",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GPUModels_ModelId",
                table: "GPUModels",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ManufacturerId",
                table: "Models",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardModels_ModelId",
                table: "MotherboardModels",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RAMModels_ModelId",
                table: "RAMModels",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StorageModels_ModelId",
                table: "StorageModels",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SystemBIOSs_BIOSModelId",
                table: "SystemBIOSs",
                column: "BIOSModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCPUs_CPUModelID",
                table: "SystemCPUs",
                column: "CPUModelID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGPUs_GPUModelId",
                table: "SystemGPUs",
                column: "GPUModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemMotherboards_MotherboardModelId",
                table: "SystemMotherboards",
                column: "MotherboardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOSs_ArchitectureId",
                table: "SystemOSs",
                column: "ArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOSs_OperatingSystemId",
                table: "SystemOSs",
                column: "OperatingSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRAMs_RAMModelId",
                table: "SystemRAMs",
                column: "RAMModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ModelId",
                table: "Systems",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStorages_StorageInterfaceId",
                table: "SystemStorages",
                column: "StorageInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStorages_StorageModelId",
                table: "SystemStorages",
                column: "StorageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTypes_TypeId",
                table: "SystemTypes",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSystems");

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
                name: "AuthenticationTokens");

            migrationBuilder.DropTable(
                name: "SystemBIOSs");

            migrationBuilder.DropTable(
                name: "SystemCommands");

            migrationBuilder.DropTable(
                name: "SystemCPUs");

            migrationBuilder.DropTable(
                name: "SystemCPUsData");

            migrationBuilder.DropTable(
                name: "SystemGPUs");

            migrationBuilder.DropTable(
                name: "SystemMotherboards");

            migrationBuilder.DropTable(
                name: "SystemOSs");

            migrationBuilder.DropTable(
                name: "SystemPingData");

            migrationBuilder.DropTable(
                name: "SystemRAMData");

            migrationBuilder.DropTable(
                name: "SystemRAMs");

            migrationBuilder.DropTable(
                name: "SystemRunningProcesses");

            migrationBuilder.DropTable(
                name: "SystemStorageData");

            migrationBuilder.DropTable(
                name: "SystemStorages");

            migrationBuilder.DropTable(
                name: "SystemTypes");

            migrationBuilder.DropTable(
                name: "ApplicationUserSystemCategory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BIOSModels");

            migrationBuilder.DropTable(
                name: "CPUModels");

            migrationBuilder.DropTable(
                name: "GPUModels");

            migrationBuilder.DropTable(
                name: "MotherboardModels");

            migrationBuilder.DropTable(
                name: "OperatingSystemModels");

            migrationBuilder.DropTable(
                name: "RAMModels");

            migrationBuilder.DropTable(
                name: "StorageInterfaceTypes");

            migrationBuilder.DropTable(
                name: "StorageModels");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "SystemTypeNames");

            migrationBuilder.DropTable(
                name: "Architectures");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
