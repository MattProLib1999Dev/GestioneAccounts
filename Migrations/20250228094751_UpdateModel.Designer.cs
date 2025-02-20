﻿// <auto-generated />
using GestioneAccounts.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestioneAccounts.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250228094751_UpdateModel")]
    partial class UpdateModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestioneAccounts.DataAccess.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("GestioneAccounts.Domain.Models.Valori.Valori", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Valori");
                });

            modelBuilder.Entity("GestioneAccounts.Domain.Models.Valori.Valori", b =>
                {
                    b.HasOne("GestioneAccounts.DataAccess.Account", "Account")
                        .WithMany("Valori")
                        .HasForeignKey("Id");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("GestioneAccounts.DataAccess.Account", b =>
                {
                    b.Navigation("Valori");
                });
#pragma warning restore 612, 618
        }
    }
}
