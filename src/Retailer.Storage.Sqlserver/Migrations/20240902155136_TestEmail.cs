using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retailer.Storage.Sqlserver.Migrations
{
    /// <inheritdoc />
    public partial class TestEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Retails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Retails");
        }
    }
}
