using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyManagement.Infrastructure.Migrations
{
    public partial class chanprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserSurveyId",
                table: "AssessmentSurveys",
                newName: "UserSurveyAssessmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserSurveyAssessmentId",
                table: "AssessmentSurveys",
                newName: "UserSurveyId");
        }
    }
}
