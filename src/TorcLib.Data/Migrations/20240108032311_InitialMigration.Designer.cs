﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashFlow.Data.Migrations
{
    [DbContext(typeof(CashFlowContext))]
    [Migration("20240108032311_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CashFlow.Domain.Aggregates.BillsToPay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expirationdate");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("paymentdate");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer")
                        .HasColumnName("supplierid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_billstopay");

                    b.HasIndex("SupplierId");

                    b.ToTable("billstopay", (string)null);
                });

            modelBuilder.Entity("CashFlow.Domain.Aggregates.BillsToReceive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customerid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expirationdate");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("paymentdate");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_billstoreceive");

                    b.HasIndex("CustomerId");

                    b.ToTable("billstoreceive", (string)null);
                });

            modelBuilder.Entity("CashFlow.Domain.Aggregates.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("CashFlow.Domain.Aggregates.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_supplier");

                    b.ToTable("supplier", (string)null);
                });

            modelBuilder.Entity("CashFlow.Domain.Aggregates.BillsToPay", b =>
                {
                    b.HasOne("CashFlow.Domain.Aggregates.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_billstopay_supplier_supplierid");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("CashFlow.Domain.Aggregates.BillsToReceive", b =>
                {
                    b.HasOne("CashFlow.Domain.Aggregates.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_billstoreceive_customer_customerid");

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
