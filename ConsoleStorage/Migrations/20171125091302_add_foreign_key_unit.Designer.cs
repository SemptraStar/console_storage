﻿// <auto-generated />
using ConsoleStorage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ConsoleStorage.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20171125091302_add_foreign_key_unit")]
    partial class add_foreign_key_unit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConsoleStorage.Models.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsDelivery");

                    b.HasKey("Id");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("ConsoleStorage.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("UnitId");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("UnitId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ConsoleStorage.Models.ProductBatch", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("BatchId");

                    b.Property<double>("Quantity");

                    b.HasKey("ProductId", "BatchId");

                    b.HasIndex("BatchId");

                    b.ToTable("ProductBatches");
                });

            modelBuilder.Entity("ConsoleStorage.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UnitName");

                    b.HasKey("Id");

                    b.HasIndex("UnitName")
                        .IsUnique()
                        .HasFilter("[UnitName] IS NOT NULL");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("ConsoleStorage.Models.Product", b =>
                {
                    b.HasOne("ConsoleStorage.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ConsoleStorage.Models.ProductBatch", b =>
                {
                    b.HasOne("ConsoleStorage.Models.Batch", "Batch")
                        .WithMany()
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ConsoleStorage.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
