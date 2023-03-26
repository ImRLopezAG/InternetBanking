﻿// <auto-generated />
using System;
using ITBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITBanking.Infrastructure.Persistence.Migrations {
  [DbContext(typeof(ITBankingContext))]
  [Migration("20230322211000_AddDbt")]
  partial class AddDbt {
    protected override void BuildTargetModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "6.0.14")
          .HasAnnotation("Relational:MaxIdentifierLength", 128);

      SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Beneficiary", b => {
        b.Property<int>("Id")
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

        b.Property<DateTime>("CreatedAt")
            .HasColumnType("datetime2");

        b.Property<DateTime>("LastModifiedAt")
            .HasColumnType("datetime2");

        b.Property<int>("ProductId")
            .HasColumnType("int");

        b.Property<string>("Receptor")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.Property<string>("Sender")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.HasKey("Id");

        b.HasIndex("ProductId");

        b.ToTable("Beneficiaries", ( string )null);
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Payment", b => {
        b.Property<int>("Id")
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

        b.Property<double>("Amount")
            .HasColumnType("float");

        b.Property<DateTime>("CreatedAt")
            .HasColumnType("datetime2");

        b.Property<DateTime>("LastModifiedAt")
            .HasColumnType("datetime2");

        b.Property<int>("RProductId")
            .HasColumnType("int");

        b.Property<string>("Receptor")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.Property<int>("SProductId")
            .HasColumnType("int");

        b.Property<string>("Sender")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.HasKey("Id");

        b.HasIndex("RProductId");

        b.HasIndex("SProductId");

        b.ToTable("Payments", ( string )null);
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Product", b => {
        b.Property<int>("Id")
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

        b.Property<string>("AccountNumber")
            .IsRequired()
            .HasColumnType("nvarchar(450)");

        b.Property<double>("Amount")
            .HasColumnType("float");

        b.Property<DateTime>("CreatedAt")
            .HasColumnType("datetime2");

        b.Property<double?>("Dbt")
            .HasColumnType("float");

        b.Property<bool?>("HasLimit")
            .HasColumnType("bit");

        b.Property<bool>("IsPrincipal")
            .HasColumnType("bit");

        b.Property<DateTime>("LastModifiedAt")
            .HasColumnType("datetime2");

        b.Property<double?>("Limit")
            .HasColumnType("float");

        b.Property<int>("TyAccountId")
            .HasColumnType("int");

        b.Property<string>("UserId")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.HasKey("Id");

        b.HasIndex("AccountNumber")
            .IsUnique();

        b.ToTable("Products", ( string )null);
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Transfer", b => {
        b.Property<int>("Id")
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

        b.Property<double>("Amount")
            .HasColumnType("float");

        b.Property<DateTime>("CreatedAt")
            .HasColumnType("datetime2");

        b.Property<DateTime>("LastModifiedAt")
            .HasColumnType("datetime2");

        b.Property<int>("RProductId")
            .HasColumnType("int");

        b.Property<string>("Receptor")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.Property<int>("SProductId")
            .HasColumnType("int");

        b.Property<string>("Sender")
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        b.HasKey("Id");

        b.HasIndex("RProductId");

        b.HasIndex("SProductId");

        b.ToTable("Transfers", ( string )null);
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Beneficiary", b => {
        b.HasOne("ITBanking.Core.Domain.Entities.Product", "Product")
            .WithMany("Beneficiaries")
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        b.Navigation("Product");
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Payment", b => {
        b.HasOne("ITBanking.Core.Domain.Entities.Product", "RProduct")
            .WithMany("RPayments")
            .HasForeignKey("RProductId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        b.HasOne("ITBanking.Core.Domain.Entities.Product", "SProduct")
            .WithMany("SPayments")
            .HasForeignKey("SProductId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        b.Navigation("RProduct");

        b.Navigation("SProduct");
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Transfer", b => {
        b.HasOne("ITBanking.Core.Domain.Entities.Product", "RProduct")
            .WithMany("RTransfers")
            .HasForeignKey("RProductId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        b.HasOne("ITBanking.Core.Domain.Entities.Product", "SProduct")
            .WithMany("STransfers")
            .HasForeignKey("SProductId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        b.Navigation("RProduct");

        b.Navigation("SProduct");
      });

      modelBuilder.Entity("ITBanking.Core.Domain.Entities.Product", b => {
        b.Navigation("Beneficiaries");

        b.Navigation("RPayments");

        b.Navigation("RTransfers");

        b.Navigation("SPayments");

        b.Navigation("STransfers");
      });
#pragma warning restore 612, 618
    }
  }
}
