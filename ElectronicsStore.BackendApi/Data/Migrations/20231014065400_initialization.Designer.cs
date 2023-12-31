﻿// <auto-generated />
using System;
using ElectronicsStore.BackendApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElectronicsStore.BackendApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231014065400_initialization")]
    partial class initialization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaNhanHieu");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenNhanHieu");

                    b.HasKey("Id");

                    b.ToTable("NHANHIEU");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaLoaiHang");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenLoai");

                    b.HasKey("Id");

                    b.ToTable("LOAIHANG");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaAnh");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDefaul")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("MacDinh");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DuongDan");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ANH");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.ImportBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaDonNhap");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 10, 14, 13, 54, 0, 210, DateTimeKind.Local).AddTicks(558))
                        .HasColumnName("NgayNhap");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int>("TotalValue")
                        .HasColumnType("int")
                        .HasColumnName("TongGiaTri");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(900)");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("HOADONNHAPHANG");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.ImportBillDetail", b =>
                {
                    b.Property<int>("ImportBillId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ImportPrice")
                        .HasColumnType("int")
                        .HasColumnName("GiaNhap");

                    b.Property<int>("Inventory")
                        .HasColumnType("int")
                        .HasColumnName("HangTon");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("SoLuong");

                    b.HasKey("ImportBillId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CHITIETHOADONNHAP");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaSanPham");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 10, 14, 13, 54, 0, 212, DateTimeKind.Local).AddTicks(4381))
                        .HasColumnName("NgayTao");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MoTa");

                    b.Property<string>("Introduce")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("GioiThieu");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenSanPham");

                    b.Property<int>("OfferRate")
                        .HasColumnType("int")
                        .HasColumnName("TyLeGiam");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NguonGoc");

                    b.Property<int>("PurchaseCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("SoLuongMua");

                    b.Property<int>("SalePrice")
                        .HasColumnType("int")
                        .HasColumnName("GiaBan");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SANPHAM");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .IsUnicode(false)
                        .HasColumnType("varchar(900)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.SaleBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaDonBan");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DiaChiNhan");

                    b.Property<DateTime>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 10, 14, 13, 54, 0, 213, DateTimeKind.Local).AddTicks(9254))
                        .HasColumnName("NgayLap");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SoDienThoai");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenNguoiNhan");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("ToTalPayment")
                        .HasColumnType("int")
                        .HasColumnName("TongThanhToan");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(900)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("HOADONBANHANG");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.SaleBillDetail", b =>
                {
                    b.Property<int>("SaleBillId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ImportBillId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("SoLuong");

                    b.Property<int>("SalePrice")
                        .HasColumnType("int")
                        .HasColumnName("GiaBan");

                    b.HasKey("SaleBillId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CHITIETHOADONBAN");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaTrangThai");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenTrangThai");

                    b.HasKey("Id");

                    b.ToTable("TRANGTHAI");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaNhaCungCap");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Diachi");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenNganHang");

                    b.Property<string>("BankNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SoTaiKhoan");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TenNhaCungCap");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DienThoai");

                    b.HasKey("Id");

                    b.ToTable("NHACUNGCAP");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .IsUnicode(false)
                        .HasColumnType("varchar(900)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 10, 14, 13, 54, 0, 209, DateTimeKind.Local).AddTicks(4441));

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(900)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(900)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(900)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(900)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(900)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(900)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Image", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.ImportBill", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Supplier", "Supplier")
                        .WithMany("ImportBills")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.User", "User")
                        .WithMany("ImportBills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.ImportBillDetail", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.ImportBill", "ImportBill")
                        .WithMany("ImportBillDetails")
                        .HasForeignKey("ImportBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Product", "Product")
                        .WithMany("ImportBillDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportBill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Product", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.SaleBill", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Status", "Status")
                        .WithMany("SaleBills")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.User", "User")
                        .WithMany("SaleBills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.SaleBillDetail", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Product", "Product")
                        .WithMany("SaleBillDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.SaleBill", "SaleBill")
                        .WithMany("SaleBillDetails")
                        .HasForeignKey("SaleBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SaleBill");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ElectronicsStore.BackendApi.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.ImportBill", b =>
                {
                    b.Navigation("ImportBillDetails");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ImportBillDetails");

                    b.Navigation("SaleBillDetails");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.SaleBill", b =>
                {
                    b.Navigation("SaleBillDetails");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Status", b =>
                {
                    b.Navigation("SaleBills");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.Supplier", b =>
                {
                    b.Navigation("ImportBills");
                });

            modelBuilder.Entity("ElectronicsStore.BackendApi.Data.Entities.User", b =>
                {
                    b.Navigation("ImportBills");

                    b.Navigation("SaleBills");
                });
#pragma warning restore 612, 618
        }
    }
}
