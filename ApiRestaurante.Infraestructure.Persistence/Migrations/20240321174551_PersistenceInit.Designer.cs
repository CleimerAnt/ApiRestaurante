﻿// <auto-generated />
using System;
using ApiRestaurante.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiRestaurante.Infraestructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240321174551_PersistenceInit")]
    partial class PersistenceInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Dishes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DishCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LasModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPerson")
                        .HasColumnType("int");

                    b.Property<int?>("OrdersId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrdersId");

                    b.ToTable("Dishes", (string)null);
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.DishesIngredients", b =>
                {
                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("DishesId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId", "DishesId");

                    b.HasIndex("DishesId");

                    b.ToTable("DishesIngredients");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.DishesOrders", b =>
                {
                    b.Property<int>("OrdersId")
                        .HasColumnType("int");

                    b.Property<int>("DishesID")
                        .HasColumnType("int");

                    b.HasKey("OrdersId", "DishesID");

                    b.HasIndex("DishesID");

                    b.ToTable("DishesOrders", (string)null);
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Ingredients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LasModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LasModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SubTotal")
                        .HasColumnType("float");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Tables", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LasModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPeoplePerTable")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tables", (string)null);
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Dishes", b =>
                {
                    b.HasOne("ApiRestaurante.Core.Domain.Entities.Orders", null)
                        .WithMany("Dishes")
                        .HasForeignKey("OrdersId");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.DishesIngredients", b =>
                {
                    b.HasOne("ApiRestaurante.Core.Domain.Entities.Dishes", "Dishes")
                        .WithMany("DishesIngredients")
                        .HasForeignKey("DishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiRestaurante.Core.Domain.Entities.Ingredients", "Ingredients")
                        .WithMany("DishesIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dishes");

                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.DishesOrders", b =>
                {
                    b.HasOne("ApiRestaurante.Core.Domain.Entities.Dishes", "Dishes")
                        .WithMany("DishesOrders")
                        .HasForeignKey("DishesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiRestaurante.Core.Domain.Entities.Orders", "Orders")
                        .WithMany("DishesOrders")
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dishes");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Orders", b =>
                {
                    b.HasOne("ApiRestaurante.Core.Domain.Entities.Tables", "Tables")
                        .WithMany("Orders")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Dishes", b =>
                {
                    b.Navigation("DishesIngredients");

                    b.Navigation("DishesOrders");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Ingredients", b =>
                {
                    b.Navigation("DishesIngredients");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Orders", b =>
                {
                    b.Navigation("Dishes");

                    b.Navigation("DishesOrders");
                });

            modelBuilder.Entity("ApiRestaurante.Core.Domain.Entities.Tables", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}