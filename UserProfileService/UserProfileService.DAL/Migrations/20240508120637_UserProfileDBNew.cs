using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfileService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileDBNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "UserProfiles",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "UserProfiles",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "UserProfiles",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "profileID",
                table: "UserProfiles",
                newName: "ProfileID");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "UserProfiles",
                type: "varchar(160)",
                maxLength: 160,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UserProfiles",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "UserProfiles",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "UserProfiles",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "UserProfiles",
                newName: "profileID");
        }
    }
}
