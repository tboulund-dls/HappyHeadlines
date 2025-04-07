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
    [Migration("20250401091613_InitialCreate")]
    partial class InitialCreate
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

            modelBuilder.Entity("SubscriberService.Models.SubscriberType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SubscriberTypes");
                });

            modelBuilder.Entity("SubscriberService.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SubscriberId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubsriberId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("TypeId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("SubscriberService.Models.Subscription", b =>
                {
                    b.HasOne("SubscriberService.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId");

                    b.HasOne("SubscriberService.Models.SubscriberType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscriber");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
