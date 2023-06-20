﻿// <auto-generated />
using System;
using GeekShop.OrderAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShop.OrderAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20230620000741_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GeekShop.OrderAPI.Model.OrderDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count");

                    b.Property<long>("OrderHeaderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("product_name");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("order_detail");
                });

            modelBuilder.Entity("GeekShop.OrderAPI.Model.OrderHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cvv");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("card_number");

                    b.Property<int>("CartTotalItens")
                        .HasColumnType("int")
                        .HasColumnName("total_itens");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("coupon_code");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("purchase_date");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("discount_amount");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("ExpiryMothYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("expiry_month_year");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("purchase_time");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("bit")
                        .HasColumnName("payment_status");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<decimal>("PurchaseAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("purchase_amount");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("order_header");
                });

            modelBuilder.Entity("GeekShop.OrderAPI.Model.OrderDetail", b =>
                {
                    b.HasOne("GeekShop.OrderAPI.Model.OrderHeader", "OrderHeader")
                        .WithMany("CartDetails")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("GeekShop.OrderAPI.Model.OrderHeader", b =>
                {
                    b.Navigation("CartDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
