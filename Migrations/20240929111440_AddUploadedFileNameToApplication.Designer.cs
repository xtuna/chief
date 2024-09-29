﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chief.DAL;

#nullable disable

namespace chief.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240929111440_AddUploadedFileNameToApplication")]
    partial class AddUploadedFileNameToApplication
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DTSNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DaysToEvaluate")
                        .HasColumnType("int");

                    b.Property<int>("Evaluators")
                        .HasColumnType("int");

                    b.Property<DateTime>("FiledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UploadedFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Applications", t =>
                        {
                            t.HasTrigger("trg_UpdateApplicationStatus");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });
#pragma warning restore 612, 618
        }
    }
}
