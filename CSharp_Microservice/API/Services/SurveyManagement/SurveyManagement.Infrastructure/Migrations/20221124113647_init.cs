using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyManagement.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentSurveys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentSurveyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserSurveyId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CompetencyGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CompetencyId = table.Column<long>(type: "bigint", nullable: false),
                    BenchMarkId = table.Column<long>(type: "bigint", nullable: false),
                    ActualLevelId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSurveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assessmenttype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    FromPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSurveys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AssessmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSurveyAssessments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSurveyId = table.Column<long>(type: "bigint", nullable: true),
                    AssessorId = table.Column<long>(type: "bigint", nullable: false),
                    AssessorRoleId = table.Column<long>(type: "bigint", nullable: false),
                    AssessmentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveyAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSurveyAssessments_AssessmentTypes_AssessmentTypeId",
                        column: x => x.AssessmentTypeId,
                        principalTable: "AssessmentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSurveyAssessments_UserSurveys_UserSurveyId",
                        column: x => x.UserSurveyId,
                        principalTable: "UserSurveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveyAssessments_AssessmentTypeId",
                table: "UserSurveyAssessments",
                column: "AssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveyAssessments_UserSurveyId",
                table: "UserSurveyAssessments",
                column: "UserSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSurveys");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "UserSurveyAssessments");

            migrationBuilder.DropTable(
                name: "AssessmentTypes");

            migrationBuilder.DropTable(
                name: "UserSurveys");
        }
    }
}
