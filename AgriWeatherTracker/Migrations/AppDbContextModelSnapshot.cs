﻿// <auto-generated />
using System;
using AgriWeatherTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AgriWeatherTracker.Models.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<double>("Rainfall")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Weathers");
                });

            modelBuilder.Entity("ConditionThreshold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("MaxHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("MaxRainfall")
                        .HasColumnType("double precision");

                    b.Property<double>("MaxTemperature")
                        .HasColumnType("double precision");

                    b.Property<double>("MaxWindSpeed")
                        .HasColumnType("double precision");

                    b.Property<double>("MinHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("MinRainfall")
                        .HasColumnType("double precision");

                    b.Property<double>("MinTemperature")
                        .HasColumnType("double precision");

                    b.Property<double>("MinWindSpeed")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("OptimalHumidity")
                        .HasColumnType("double precision");

                    b.Property<int>("ResilienceDuration")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ConditionThresholds");
                });

            modelBuilder.Entity("Crop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Crops");
                });

            modelBuilder.Entity("GrowthCycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CropId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CropId");

                    b.ToTable("GrowthCycles");
                });

            modelBuilder.Entity("GrowthStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdverseConditionsId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("GrowthCycleId")
                        .HasColumnType("integer");

                    b.Property<double>("HistoricalAdverseImpactScore")
                        .HasColumnType("double precision");

                    b.Property<int>("OptimalConditionsId")
                        .HasColumnType("integer");

                    b.Property<int>("ResilienceDurationInDays")
                        .HasColumnType("integer");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdverseConditionsId");

                    b.HasIndex("GrowthCycleId");

                    b.HasIndex("OptimalConditionsId");

                    b.ToTable("GrowthStages");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CropId")
                        .HasColumnType("integer");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CropId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("AgriWeatherTracker.Models.Weather", b =>
                {
                    b.HasOne("Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("GrowthCycle", b =>
                {
                    b.HasOne("Crop", null)
                        .WithMany("GrowthCycles")
                        .HasForeignKey("CropId");
                });

            modelBuilder.Entity("GrowthStage", b =>
                {
                    b.HasOne("ConditionThreshold", "AdverseConditions")
                        .WithMany()
                        .HasForeignKey("AdverseConditionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GrowthCycle", null)
                        .WithMany("Stages")
                        .HasForeignKey("GrowthCycleId");

                    b.HasOne("ConditionThreshold", "OptimalConditions")
                        .WithMany()
                        .HasForeignKey("OptimalConditionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdverseConditions");

                    b.Navigation("OptimalConditions");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.HasOne("Crop", "Crop")
                        .WithMany("Locations")
                        .HasForeignKey("CropId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crop");
                });

            modelBuilder.Entity("Crop", b =>
                {
                    b.Navigation("GrowthCycles");

                    b.Navigation("Locations");
                });

            modelBuilder.Entity("GrowthCycle", b =>
                {
                    b.Navigation("Stages");
                });
#pragma warning restore 612, 618
        }
    }
}
