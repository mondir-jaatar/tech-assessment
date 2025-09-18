using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeChooz.TechAssessment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class change_MaxCapacity_to_MaxParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "Courses",
                newName: "MaxParticipants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "Courses",
                newName: "MaxCapacity");
        }
    }
}
