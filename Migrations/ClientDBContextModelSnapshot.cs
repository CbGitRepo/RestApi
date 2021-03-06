﻿// <auto-generated />
using System;
using AngularNetCoreSample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AngularNetCoreSample.Migrations
{
    [DbContext(typeof(ClientDBContext))]
    partial class ClientDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AngularNetCoreSample.Data.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<int>("CommandeCount");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AngularNetCoreSample.Data.Entities.Commande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int?>("clientId");

                    b.HasKey("Id");

                    b.HasIndex("clientId");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("AngularNetCoreSample.Data.Entities.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("commandeId");

                    b.Property<string>("productCategory");

                    b.Property<string>("productDescription");

                    b.Property<string>("productName");

                    b.Property<int>("productPrice");

                    b.Property<int>("productWeight");

                    b.HasKey("ID");

                    b.HasIndex("commandeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AngularNetCoreSample.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token");

                    b.Property<string>("email");

                    b.Property<string>("firstName");

                    b.Property<string>("lastName");

                    b.Property<string>("password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AngularNetCoreSample.Data.Entities.Commande", b =>
                {
                    b.HasOne("AngularNetCoreSample.Data.Entities.Client", "client")
                        .WithMany("Commandes")
                        .HasForeignKey("clientId");
                });

            modelBuilder.Entity("AngularNetCoreSample.Data.Entities.Product", b =>
                {
                    b.HasOne("AngularNetCoreSample.Data.Entities.Commande", "commande")
                        .WithMany("Products")
                        .HasForeignKey("commandeId");
                });
#pragma warning restore 612, 618
        }
    }
}
