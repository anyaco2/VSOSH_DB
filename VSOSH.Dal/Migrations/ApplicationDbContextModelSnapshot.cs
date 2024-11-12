﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VSOSH.Dal;

#nullable disable

namespace VSOSH.Dal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VSOSH.Domain.Entities.SchoolOlympiadResultBase", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("CurrentGrade")
                        .HasColumnType("integer")
                        .HasColumnName("CurrentGrade");

                    b.Property<double>("FinalScore")
                        .HasColumnType("double precision");

                    b.Property<int?>("GradeCompeting")
                        .HasColumnType("integer")
                        .HasColumnName("GradeCompeting");

                    b.Property<string>("ParticipantCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ParticipantCode");

                    b.Property<double>("Percentage")
                        .HasColumnType("double precision")
                        .HasColumnName("Percentage");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("School");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.ComplexProperty<Dictionary<string, object>>("StudentName", "VSOSH.Domain.Entities.SchoolOlympiadResultBase.StudentName#StudentName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("LastName");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("MiddleName");
                        });

                    b.HasKey("Id");

                    b.ToTable("SchoolOlympiadResultBase", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.ArtResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("ArtResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.AstronomyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("AstronomyResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.BiologyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("BiologyResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.ChemistryResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("ChemistryResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.ChineseResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("ChineseResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.ComputerScienceResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("ComputerScienceResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.EcologyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("EcologyResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.EconomyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("EconomyResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.EnglishResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("EnglishResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.FrenchResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("FrenchResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.FundamentalsLifeSafetyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("FundamentalsLifeSafetyResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.GeographyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("GeographyResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.GermanResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("GermanResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.HistoryResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("HistoryResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.LawResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("LawResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.LiteratureResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("LiteratureResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.MathResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("MathResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.PhysicResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("PhysicResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.PhysicalEducationResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.Property<double>("FinalScoreInPractice")
                        .HasColumnType("double precision")
                        .HasColumnName("FinalScoreInPractice");

                    b.Property<double>("FinalScoreInTheory")
                        .HasColumnType("double precision")
                        .HasColumnName("FinalScoreInTheory");

                    b.Property<double>("PreliminaryScoreInPractice")
                        .HasColumnType("double precision")
                        .HasColumnName("PreliminaryScoreInPractice");

                    b.Property<double>("PreliminaryScoreInTheory")
                        .HasColumnType("double precision")
                        .HasColumnName("PreliminaryScoreInTheory");

                    b.ToTable("PhysicalEducationResult", (string)null);
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.RussianResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("RussianResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.SocialStudiesResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.ToTable("SocialStudiesResults");
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.TechnologyResult", b =>
                {
                    b.HasBaseType("VSOSH.Domain.Entities.SchoolOlympiadResultBase");

                    b.Property<string>("DirectionPractice")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DirectionPractice");

                    b.ToTable("TechnologyResult", (string)null);
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.PhysicalEducationResult", b =>
                {
                    b.HasOne("VSOSH.Domain.Entities.SchoolOlympiadResultBase", null)
                        .WithOne()
                        .HasForeignKey("VSOSH.Domain.Entities.PhysicalEducationResult", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.RussianResult", b =>
                {
                    b.HasOne("VSOSH.Domain.Entities.SchoolOlympiadResultBase", null)
                        .WithOne()
                        .HasForeignKey("VSOSH.Domain.Entities.RussianResult", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.SocialStudiesResult", b =>
                {
                    b.HasOne("VSOSH.Domain.Entities.SchoolOlympiadResultBase", null)
                        .WithOne()
                        .HasForeignKey("VSOSH.Domain.Entities.SocialStudiesResult", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VSOSH.Domain.Entities.TechnologyResult", b =>
                {
                    b.HasOne("VSOSH.Domain.Entities.SchoolOlympiadResultBase", null)
                        .WithOne()
                        .HasForeignKey("VSOSH.Domain.Entities.TechnologyResult", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
