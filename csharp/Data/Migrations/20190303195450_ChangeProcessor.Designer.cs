﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using csharp.Data;

namespace csharp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190303195450_ChangeProcessor")]
    partial class ChangeProcessor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("csharp.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("csharp.Data.ApplicationUserSystems", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<Guid>("SystemId");

                    b.HasKey("UserId", "SystemId");

                    b.HasIndex("SystemId");

                    b.ToTable("ApplicationUserSystems");
                });

            modelBuilder.Entity("csharp.Data.Architecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Architectures");
                });

            modelBuilder.Entity("csharp.Data.GPUModel", b =>
                {
                    b.Property<Guid>("Id");

                    b.HasKey("Id");

                    b.ToTable("GPUModels");
                });

            modelBuilder.Entity("csharp.Data.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("csharp.Data.MemoryModel", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<int>("MemoryBytes");

                    b.Property<Guid>("MemoryTypeId");

                    b.HasKey("Id");

                    b.HasIndex("MemoryTypeId");

                    b.ToTable("MemoryModels");
                });

            modelBuilder.Entity("csharp.Data.MemoryType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MemoryTypes");
                });

            modelBuilder.Entity("csharp.Data.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ManufacturerId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("csharp.Data.MotherboardModel", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("SerialNumber");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("MotherboardModels");
                });

            modelBuilder.Entity("csharp.Data.OperatingSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("OperatingSystems");
                });

            modelBuilder.Entity("csharp.Data.ProcessorModel", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("ArchitectureId");

                    b.Property<Guid?>("GPUModelId");

                    b.HasKey("Id");

                    b.HasIndex("ArchitectureId");

                    b.HasIndex("GPUModelId");

                    b.ToTable("ProcessorModels");
                });

            modelBuilder.Entity("csharp.Data.RAMModel", b =>
                {
                    b.Property<Guid>("Id");

                    b.HasKey("Id");

                    b.ToTable("RAMModels");
                });

            modelBuilder.Entity("csharp.Data.StorageModel", b =>
                {
                    b.Property<Guid>("Id");

                    b.HasKey("Id");

                    b.ToTable("StorageModels");
                });

            modelBuilder.Entity("csharp.Data.System", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HostName");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<Guid?>("ModelId");

                    b.Property<Guid?>("MotherboardId");

                    b.Property<string>("Secret");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.HasIndex("MotherboardId");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("csharp.Data.SystemCPU", b =>
                {
                    b.Property<Guid>("CPUModelID");

                    b.Property<Guid>("SystemId");

                    b.Property<int>("ClockSpeed");

                    b.Property<int>("CoreCount");

                    b.Property<int>("ThreadCount");

                    b.HasKey("CPUModelID", "SystemId");

                    b.HasIndex("SystemId");

                    b.ToTable("SystemCPUs");
                });

            modelBuilder.Entity("csharp.Data.SystemGPU", b =>
                {
                    b.Property<Guid>("SystemId");

                    b.Property<Guid>("GPUModelId");

                    b.HasKey("SystemId", "GPUModelId");

                    b.HasIndex("GPUModelId");

                    b.ToTable("SystemGPUs");
                });

            modelBuilder.Entity("csharp.Data.SystemModelType", b =>
                {
                    b.Property<Guid>("SystemId");

                    b.Property<Guid>("TypeId");

                    b.HasKey("SystemId", "TypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("SystemModelTypes");
                });

            modelBuilder.Entity("csharp.Data.SystemOS", b =>
                {
                    b.Property<Guid>("SystemId");

                    b.Property<Guid>("OSId");

                    b.Property<Guid>("ArchitectureId");

                    b.Property<string>("Version");

                    b.HasKey("SystemId", "OSId");

                    b.HasIndex("ArchitectureId");

                    b.HasIndex("OSId");

                    b.ToTable("SystemOSs");
                });

            modelBuilder.Entity("csharp.Data.SystemRAM", b =>
                {
                    b.Property<Guid>("SystemId");

                    b.Property<Guid>("RAMModelId");

                    b.HasKey("SystemId", "RAMModelId");

                    b.HasIndex("RAMModelId");

                    b.ToTable("SystemRAMs");
                });

            modelBuilder.Entity("csharp.Data.SystemStorage", b =>
                {
                    b.Property<Guid>("SystemId");

                    b.Property<Guid>("StorageModelId");

                    b.HasKey("SystemId", "StorageModelId");

                    b.HasIndex("StorageModelId");

                    b.ToTable("SystemStorages");
                });

            modelBuilder.Entity("csharp.Data.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Audience");

                    b.Property<DateTime>("Expires");

                    b.Property<string>("Issuer");

                    b.Property<Guid?>("NextTokenId");

                    b.Property<DateTime>("NotBefore");

                    b.Property<Guid?>("PreviousTokenId");

                    b.Property<string>("RefreshToken");

                    b.Property<string>("Subject");

                    b.Property<string>("TokenType");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PreviousTokenId")
                        .IsUnique()
                        .HasFilter("[PreviousTokenId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("csharp.Data.Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("csharp.Data.ApplicationUserSystems", b =>
                {
                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("Users")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.ApplicationUser", "User")
                        .WithMany("Systems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.GPUModel", b =>
                {
                    b.HasOne("csharp.Data.RAMModel", "RAMModel")
                        .WithOne("GPUModel")
                        .HasForeignKey("csharp.Data.GPUModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("csharp.Data.MemoryModel", b =>
                {
                    b.HasOne("csharp.Data.Model", "Model")
                        .WithOne("MemoryModel")
                        .HasForeignKey("csharp.Data.MemoryModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("csharp.Data.MemoryType", "MemoryType")
                        .WithMany("MemoryModels")
                        .HasForeignKey("MemoryTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("csharp.Data.Model", b =>
                {
                    b.HasOne("csharp.Data.Manufacturer", "Manufacturer")
                        .WithMany("Models")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("csharp.Data.MotherboardModel", b =>
                {
                    b.HasOne("csharp.Data.Model", "Model")
                        .WithOne("MotherboardModel")
                        .HasForeignKey("csharp.Data.MotherboardModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("csharp.Data.ProcessorModel", b =>
                {
                    b.HasOne("csharp.Data.Architecture", "Architecture")
                        .WithMany("ProcessorModels")
                        .HasForeignKey("ArchitectureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.GPUModel", "GPUModel")
                        .WithMany()
                        .HasForeignKey("GPUModelId");

                    b.HasOne("csharp.Data.Model", "Model")
                        .WithOne("ProcessorModel")
                        .HasForeignKey("csharp.Data.ProcessorModel", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.RAMModel", b =>
                {
                    b.HasOne("csharp.Data.MemoryModel", "MemoryModel")
                        .WithOne("RAMModel")
                        .HasForeignKey("csharp.Data.RAMModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("csharp.Data.StorageModel", b =>
                {
                    b.HasOne("csharp.Data.MemoryModel", "MemoryModel")
                        .WithOne("StorageModel")
                        .HasForeignKey("csharp.Data.StorageModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("csharp.Data.System", b =>
                {
                    b.HasOne("csharp.Data.Model", "Model")
                        .WithMany("Systems")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.MotherboardModel", "MotherboardModel")
                        .WithMany("Systems")
                        .HasForeignKey("MotherboardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.SystemCPU", b =>
                {
                    b.HasOne("csharp.Data.ProcessorModel", "ProcessorModel")
                        .WithMany("SystemCPUs")
                        .HasForeignKey("CPUModelID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("SystemCPUs")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.SystemGPU", b =>
                {
                    b.HasOne("csharp.Data.GPUModel", "GPUModel")
                        .WithMany("SystemGPUs")
                        .HasForeignKey("GPUModelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("SystemGPUs")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.SystemModelType", b =>
                {
                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("SystemTypes")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.Type", "Type")
                        .WithMany("SystemModelTypes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.SystemOS", b =>
                {
                    b.HasOne("csharp.Data.Architecture", "Architecture")
                        .WithMany("SystemOSs")
                        .HasForeignKey("ArchitectureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.OperatingSystem", "OperatingSystem")
                        .WithMany("SystemOSs")
                        .HasForeignKey("OSId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("SystemOSs")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.SystemRAM", b =>
                {
                    b.HasOne("csharp.Data.RAMModel", "RAMModel")
                        .WithMany("SystemRAMs")
                        .HasForeignKey("RAMModelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("SystemRAMs")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.SystemStorage", b =>
                {
                    b.HasOne("csharp.Data.StorageModel", "StorageModel")
                        .WithMany("SystemStorages")
                        .HasForeignKey("StorageModelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("csharp.Data.System", "System")
                        .WithMany("SystemStorages")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("csharp.Data.Token", b =>
                {
                    b.HasOne("csharp.Data.Token", "PreviousToken")
                        .WithOne("NextToken")
                        .HasForeignKey("csharp.Data.Token", "PreviousTokenId");

                    b.HasOne("csharp.Data.ApplicationUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("csharp.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("csharp.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("csharp.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("csharp.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
