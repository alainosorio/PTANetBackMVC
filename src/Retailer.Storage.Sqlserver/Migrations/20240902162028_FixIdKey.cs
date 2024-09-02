using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retailer.Storage.Sqlserver.Migrations
{
    /// <inheritdoc />
    public partial class FixIdKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Retails",
                table: "Retails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Retails");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Retails");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Retails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Retails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Retails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Retails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Retails",
                table: "Retails",
                column: "Code");
        }
    }
}
