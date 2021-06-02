﻿// <auto-generated />
using System;
using BlueLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlueLibrary.Migrations
{
    [DbContext(typeof(BlueLibraryContext))]
    [Migration("20210602175726_Version5")]
    partial class Version5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlueLibrary.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BookImageId")
                        .HasColumnType("int");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookImageId")
                        .IsUnique();

                    b.HasIndex("PublisherId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("BlueLibrary.Models.BookImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookImage");
                });

            modelBuilder.Entity("BlueLibrary.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("BlueLibrary.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("BlueLibrary.Models.Book", b =>
                {
                    b.HasOne("BlueLibrary.Models.BookImage", "BookImage")
                        .WithOne("Book")
                        .HasForeignKey("BlueLibrary.Models.Book", "BookImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlueLibrary.Models.Publisher", "BookPublisher")
                        .WithMany("BooksPublisher")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookImage");

                    b.Navigation("BookPublisher");
                });

            modelBuilder.Entity("BlueLibrary.Models.Genre", b =>
                {
                    b.HasOne("BlueLibrary.Models.Book", null)
                        .WithMany("Genres")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("BlueLibrary.Models.Book", b =>
                {
                    b.Navigation("Genres");
                });

            modelBuilder.Entity("BlueLibrary.Models.BookImage", b =>
                {
                    b.Navigation("Book");
                });

            modelBuilder.Entity("BlueLibrary.Models.Publisher", b =>
                {
                    b.Navigation("BooksPublisher");
                });
#pragma warning restore 612, 618
        }
    }
}
