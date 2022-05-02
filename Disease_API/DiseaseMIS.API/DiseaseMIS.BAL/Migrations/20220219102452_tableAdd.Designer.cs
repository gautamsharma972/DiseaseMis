﻿// <auto-generated />
using System;
using DiseaseMIS.BAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiseaseMIS.BAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220219102452_tableAdd")]
    partial class tableAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Disease", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("DiseaseTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("ReportCasesId")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseTypeId");

                    b.HasIndex("ReportCasesId");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.DiseaseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DiseaseType");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Districts", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Incharges", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Designation")
                        .HasColumnType("text");

                    b.Property<byte[]>("InstituteId")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.ToTable("Incharges");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Institutes", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("DistrictId")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Laboratory", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("DistrictsId")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictsId");

                    b.ToTable("Laboratories");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.MIS.AnimalCategory", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("NoOfCases")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AnimalCategories");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.MIS.Animals", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("CategoryId")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ModuleOperations", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayOrder")
                        .HasColumnType("text");

                    b.Property<string>("IconName")
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("LinkType")
                        .HasColumnType("text");

                    b.Property<string>("ModulesId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ModulesId");

                    b.ToTable("ModulesOperations");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ModulePermission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("ModuleOperationsId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Permissions")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ModuleOperationsId");

                    b.ToTable("ModulePermissions");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Modules", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayOrder")
                        .HasColumnType("text");

                    b.Property<string>("IconName")
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("LinkType")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.RecentActions", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(36)")
                        .HasMaxLength(36);

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RecentActions");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ReportCases", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<int?>("DiseaseTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("ReportsId")
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseTypeId");

                    b.HasIndex("ReportsId");

                    b.ToTable("ReportCases");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Reports", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("CurrentProcessLink")
                        .HasColumnType("text");

                    b.Property<byte[]>("DistrictId")
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("InchargeId")
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("InstituteId")
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("LastApproved")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastEdited")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Remarks")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReportingDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("InchargeId");

                    b.HasIndex("InstituteId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.RolesPermissionMapper", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("PermissionsId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("RolesId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionsId");

                    b.HasIndex("RolesId");

                    b.ToTable("RolesPermissionMappers");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Symptoms", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("DiseaseId")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("SymptomsId")
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("SymptomsId");

                    b.ToTable("Symptoms");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.UserPermissionsMapper", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("AppUserId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("PermissionsId")
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("PermissionsId");

                    b.ToTable("UserPermissionMappers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Disease", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.DiseaseType", "DiseaseType")
                        .WithMany()
                        .HasForeignKey("DiseaseTypeId");

                    b.HasOne("DiseaseMIS.BAL.Core.ReportCases", null)
                        .WithMany("Disease")
                        .HasForeignKey("ReportCasesId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Incharges", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.Institutes", "Institute")
                        .WithMany("Incharges")
                        .HasForeignKey("InstituteId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Institutes", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.Districts", "District")
                        .WithMany("Institutes")
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Laboratory", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.Districts", "Districts")
                        .WithMany("Laboratories")
                        .HasForeignKey("DistrictsId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.MIS.Animals", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.MIS.AnimalCategory", "Category")
                        .WithMany("Animals")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ModuleOperations", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.Modules", "Modules")
                        .WithMany("ModuleOperations")
                        .HasForeignKey("ModulesId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ModulePermission", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.ModuleOperations", "ModuleOperations")
                        .WithMany("Permissions")
                        .HasForeignKey("ModuleOperationsId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.ReportCases", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.DiseaseType", "DiseaseType")
                        .WithMany()
                        .HasForeignKey("DiseaseTypeId");

                    b.HasOne("DiseaseMIS.BAL.Core.Reports", null)
                        .WithMany("Cases")
                        .HasForeignKey("ReportsId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Reports", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.Districts", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");

                    b.HasOne("DiseaseMIS.BAL.Core.Incharges", "Incharge")
                        .WithMany()
                        .HasForeignKey("InchargeId");

                    b.HasOne("DiseaseMIS.BAL.Core.Institutes", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.RolesPermissionMapper", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.ModulePermission", "Permissions")
                        .WithMany()
                        .HasForeignKey("PermissionsId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Roles")
                        .WithMany()
                        .HasForeignKey("RolesId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.Symptoms", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.Disease", "Disease")
                        .WithMany("Symptoms")
                        .HasForeignKey("DiseaseId");

                    b.HasOne("DiseaseMIS.BAL.Core.Symptoms", null)
                        .WithMany("SubSymptoms")
                        .HasForeignKey("SymptomsId");
                });

            modelBuilder.Entity("DiseaseMIS.BAL.Core.UserPermissionsMapper", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.ApplicationUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("DiseaseMIS.BAL.Core.ModulePermission", "Permissions")
                        .WithMany()
                        .HasForeignKey("PermissionsId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiseaseMIS.BAL.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DiseaseMIS.BAL.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
