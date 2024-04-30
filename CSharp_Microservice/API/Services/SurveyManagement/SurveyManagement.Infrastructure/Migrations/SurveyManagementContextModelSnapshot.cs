﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyManagement.Infrastructure.Persistence;

#nullable disable

namespace SurveyManagement.Infrastructure.Migrations
{
    [DbContext(typeof(SurveyManagementContext))]
    partial class SurveyManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SurveyManagement.Domain.Entities.AssessmentSurvey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("ActualLevelId")
                        .HasColumnType("bigint");

                    b.Property<string>("AssessmentSurveyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BenchMarkId")
                        .HasColumnType("bigint");

                    b.Property<long>("CompetencyGroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("CompetencyId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserSurveyAssessmentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("AssessmentSurveys");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.AssessmentType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Assessmenttype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AssessmentTypes");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.Survey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FromPeriod")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("SurveyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToPeriod")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.UserSurvey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("AssessmentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssessmentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("SurveyId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("UserSurveys");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.UserSurveyAssessment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("AssessmentTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("AssessorId")
                        .HasColumnType("bigint");

                    b.Property<long>("AssessorRoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserSurveyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentTypeId");

                    b.HasIndex("UserSurveyId");

                    b.ToTable("UserSurveyAssessments");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.UserSurveyAssessment", b =>
                {
                    b.HasOne("SurveyManagement.Domain.Entities.AssessmentType", "AssessmentType")
                        .WithMany()
                        .HasForeignKey("AssessmentTypeId");

                    b.HasOne("SurveyManagement.Domain.Entities.UserSurvey", "UserSurvey")
                        .WithMany("UserSurveyAssessments")
                        .HasForeignKey("UserSurveyId");

                    b.Navigation("AssessmentType");

                    b.Navigation("UserSurvey");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.UserSurvey", b =>
                {
                    b.Navigation("UserSurveyAssessments");
                });
#pragma warning restore 612, 618
        }
    }
}
