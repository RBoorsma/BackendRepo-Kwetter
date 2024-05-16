﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserProfileService.DAL.Context;

#nullable disable

namespace UserProfileService.DAL.Migrations
{
    [DbContext(typeof(UserProfileDbContext))]
    [Migration("20240508120637_UserProfileDBNew")]
    partial class UserProfileDBNew
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("UserProfileService.DAL.Model.UserProfile", b =>
                {
                    b.Property<Guid>("ProfileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bio")
                        .HasMaxLength(160)
                        .HasColumnType("varchar(160)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ProfileID");

                    b.ToTable("UserProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
