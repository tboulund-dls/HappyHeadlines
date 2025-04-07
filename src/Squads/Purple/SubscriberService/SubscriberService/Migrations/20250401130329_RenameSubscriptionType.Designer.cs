﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriberService.Infrastructure.Data;

#nullable disable

namespace SubscriberService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250401130329_RenameSubscriptionType")]
    partial class RenameSubscriptionType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("SubscriberService.Models.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("SubscriberService.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionTypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("SubscriptionTypeId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("SubscriberService.Models.SubscriptionType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SubscriberTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3cbad589-81b4-49c8-a48a-1cee835ea267"),
                            Type = "DAILY"
                        },
                        new
                        {
                            Id = new Guid("9a24ad3e-4e3d-4f9b-953d-4c2b4f45abaa"),
                            Type = "NEWSSTREAM"
                        });
                });

            modelBuilder.Entity("SubscriberService.Models.Subscription", b =>
                {
                    b.HasOne("SubscriberService.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubscriberService.Models.SubscriptionType", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscriber");

                    b.Navigation("SubscriptionType");
                });
#pragma warning restore 612, 618
        }
    }
}
