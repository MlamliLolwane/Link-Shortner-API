﻿// <auto-generated />
using System;
using LinkShortnerAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LinkShortnerAPI.Migrations
{
    [DbContext(typeof(LinkShortnerContext))]
    [Migration("20250623002051_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LinkShortnerAPI.Models.Click", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LinkId")
                        .HasColumnType("int");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ShortenedUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("LinkId");

                    b.ToTable("Clicks");
                });

            modelBuilder.Entity("LinkShortnerAPI.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Referrer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("LinkShortnerAPI.Models.Click", b =>
                {
                    b.HasOne("LinkShortnerAPI.Models.Link", "Link")
                        .WithMany("Click")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Link");
                });

            modelBuilder.Entity("LinkShortnerAPI.Models.Link", b =>
                {
                    b.Navigation("Click");
                });
#pragma warning restore 612, 618
        }
    }
}
