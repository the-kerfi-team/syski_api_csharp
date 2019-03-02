using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class DefaultMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "MemoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatingSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
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
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Tokens_PreviousTokenId",
                        column: x => x.PreviousTokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ManufacturerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoryModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MemoryTypeId = table.Column<Guid>(nullable: false),
                    MemoryBytes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoryModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoryModels_MemoryTypes_MemoryTypeId",
                        column: x => x.MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherboardModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherboardModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessorModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArchitectureId = table.Column<Guid>(nullable: false),
                    ClockSpeed = table.Column<int>(nullable: false),
                    CoreCount = table.Column<int>(nullable: false),
                    ThreadCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessorModels_Architectures_ArchitectureId",
                        column: x => x.ArchitectureId,
                        principalTable: "Architectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessorModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RAMModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAMModels_MemoryModels_Id",
                        column: x => x.Id,
                        principalTable: "MemoryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageModels_MemoryModels_Id",
                        column: x => x.Id,
                        principalTable: "MemoryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Secret = table.Column<string>(nullable: true),
                    HostName = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ModelId = table.Column<Guid>(nullable: true),
                    MotherboardId = table.Column<Guid>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Systems_MotherboardModels_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "MotherboardModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GPUModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUModels_ProcessorModels_Id",
                        column: x => x.Id,
                        principalTable: "ProcessorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GPUModels_RAMModels_Id",
                        column: x => x.Id,
                        principalTable: "RAMModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSystems",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSystems", x => new { x.UserId, x.SystemId });
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
                name: "SystemCPUs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    CPUModelID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCPUs", x => new { x.CPUModelID, x.SystemId });
                    table.ForeignKey(
                        name: "FK_SystemCPUs_ProcessorModels_CPUModelID",
                        column: x => x.CPUModelID,
                        principalTable: "ProcessorModels",
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
                name: "SystemModelTypes",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemModelTypes", x => new { x.SystemId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_SystemModelTypes_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemModelTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemOSs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    OSId = table.Column<Guid>(nullable: false),
                    ArchitectureId = table.Column<Guid>(nullable: false),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOSs", x => new { x.SystemId, x.OSId });
                    table.ForeignKey(
                        name: "FK_SystemOSs_Architectures_ArchitectureId",
                        column: x => x.ArchitectureId,
                        principalTable: "Architectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemOSs_OperatingSystems_OSId",
                        column: x => x.OSId,
                        principalTable: "OperatingSystems",
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
                name: "SystemRAMs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    RAMModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRAMs", x => new { x.SystemId, x.RAMModelId });
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
                name: "SystemStorages",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    StorageModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStorages", x => new { x.SystemId, x.StorageModelId });
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
                name: "SystemGPUs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    GPUModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGPUs", x => new { x.SystemId, x.GPUModelId });
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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSystems_SystemId",
                table: "ApplicationUserSystems",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryModels_MemoryTypeId",
                table: "MemoryModels",
                column: "MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ManufacturerId",
                table: "Models",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorModels_ArchitectureId",
                table: "ProcessorModels",
                column: "ArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCPUs_SystemId",
                table: "SystemCPUs",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGPUs_GPUModelId",
                table: "SystemGPUs",
                column: "GPUModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemModelTypes_TypeId",
                table: "SystemModelTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOSs_ArchitectureId",
                table: "SystemOSs",
                column: "ArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOSs_OSId",
                table: "SystemOSs",
                column: "OSId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRAMs_RAMModelId",
                table: "SystemRAMs",
                column: "RAMModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ModelId",
                table: "Systems",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_MotherboardId",
                table: "Systems",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStorages_StorageModelId",
                table: "SystemStorages",
                column: "StorageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_PreviousTokenId",
                table: "Tokens",
                column: "PreviousTokenId",
                unique: true,
                filter: "[PreviousTokenId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSystems");

            migrationBuilder.DropTable(
                name: "SystemCPUs");

            migrationBuilder.DropTable(
                name: "SystemGPUs");

            migrationBuilder.DropTable(
                name: "SystemModelTypes");

            migrationBuilder.DropTable(
                name: "SystemOSs");

            migrationBuilder.DropTable(
                name: "SystemRAMs");

            migrationBuilder.DropTable(
                name: "SystemStorages");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "GPUModels");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "OperatingSystems");

            migrationBuilder.DropTable(
                name: "StorageModels");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "ProcessorModels");

            migrationBuilder.DropTable(
                name: "RAMModels");

            migrationBuilder.DropTable(
                name: "MotherboardModels");

            migrationBuilder.DropTable(
                name: "Architectures");

            migrationBuilder.DropTable(
                name: "MemoryModels");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "MemoryTypes");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
