﻿// <auto-generated />
using System;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConcertTicketBookingSystemAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Action", b =>
                {
                    b.Property<int>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActionId");

                    b.HasIndex("UserId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Concert", b =>
                {
                    b.Property<int>("ConcertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ConcertDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<bool>("IsActiveFlag")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<int>("LeftCount")
                        .HasColumnType("int");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Performer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PreImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PreImageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConcertId");

                    b.HasIndex("UserId");

                    b.ToTable("Concert");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Image", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Source")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("ConcertId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.PromoCode", b =>
                {
                    b.Property<Guid>("PromoCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("smallmoney");

                    b.Property<bool>("IsActiveFlag")
                        .HasColumnType("bit");

                    b.Property<int>("LeftCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalCount")
                        .HasColumnType("int");

                    b.HasKey("PromoCodeId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("PromoCodes");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Ticket", b =>
                {
                    b.Property<Guid>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("IsMarkedFlag")
                        .HasColumnType("bit");

                    b.Property<Guid>("PromoCodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketId");

                    b.HasIndex("PromoCodeId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CookieConfirmationFlag")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PromoCodeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("PromoCodeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.ClassicConcert", b =>
                {
                    b.HasBaseType("ConcertTicketBookingSystemAPI.Models.Concert");

                    b.Property<string>("Compositor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcertName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ClassicConcerts");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.OpenAirConcert", b =>
                {
                    b.HasBaseType("ConcertTicketBookingSystemAPI.Models.Concert");

                    b.Property<string>("HeadLiner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("OpenAirConcerts");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.PartyConcert", b =>
                {
                    b.HasBaseType("ConcertTicketBookingSystemAPI.Models.Concert");

                    b.Property<int>("Censure")
                        .HasColumnType("int");

                    b.ToTable("PartyConcerts");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.FacebookUser", b =>
                {
                    b.HasBaseType("ConcertTicketBookingSystemAPI.Models.User");

                    b.Property<int>("FacebookId")
                        .HasColumnType("int");

                    b.HasIndex("FacebookId")
                        .IsUnique()
                        .HasFilter("[FacebookId] IS NOT NULL");

                    b.ToTable("FacebookUsers");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.GoogleUser", b =>
                {
                    b.HasBaseType("ConcertTicketBookingSystemAPI.Models.User");

                    b.Property<int>("GoogleId")
                        .HasColumnType("int");

                    b.HasIndex("GoogleId")
                        .IsUnique()
                        .HasFilter("[GoogleId] IS NOT NULL");

                    b.ToTable("GoogleUsers");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.MicrosoftUser", b =>
                {
                    b.HasBaseType("ConcertTicketBookingSystemAPI.Models.User");

                    b.Property<int>("MicrosoftId")
                        .HasColumnType("int");

                    b.HasIndex("MicrosoftId")
                        .IsUnique()
                        .HasFilter("[MicrosoftId] IS NOT NULL");

                    b.ToTable("MicrosoftUsers");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Action", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.User", "User")
                        .WithMany("Actions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Concert", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Image", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.Concert", null)
                        .WithMany("Images")
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Ticket", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.PromoCode", "PromoCode")
                        .WithMany()
                        .HasForeignKey("PromoCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcertTicketBookingSystemAPI.Models.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PromoCode");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.User", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.PromoCode", "PromoCode")
                        .WithMany()
                        .HasForeignKey("PromoCodeId");

                    b.Navigation("PromoCode");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.ClassicConcert", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.Concert", null)
                        .WithOne()
                        .HasForeignKey("ConcertTicketBookingSystemAPI.Models.ClassicConcert", "ConcertId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.OpenAirConcert", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.Concert", null)
                        .WithOne()
                        .HasForeignKey("ConcertTicketBookingSystemAPI.Models.OpenAirConcert", "ConcertId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.PartyConcert", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.Concert", null)
                        .WithOne()
                        .HasForeignKey("ConcertTicketBookingSystemAPI.Models.PartyConcert", "ConcertId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.FacebookUser", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.User", null)
                        .WithOne()
                        .HasForeignKey("ConcertTicketBookingSystemAPI.Models.FacebookUser", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.GoogleUser", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.User", null)
                        .WithOne()
                        .HasForeignKey("ConcertTicketBookingSystemAPI.Models.GoogleUser", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.MicrosoftUser", b =>
                {
                    b.HasOne("ConcertTicketBookingSystemAPI.Models.User", null)
                        .WithOne()
                        .HasForeignKey("ConcertTicketBookingSystemAPI.Models.MicrosoftUser", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.Concert", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("ConcertTicketBookingSystemAPI.Models.User", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
