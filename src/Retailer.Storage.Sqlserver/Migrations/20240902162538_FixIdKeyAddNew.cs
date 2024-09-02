using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retailer.Storage.Sqlserver.Migrations
{
    /// <inheritdoc />
    public partial class FixIdKeyAddNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Retails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Retails",
                table: "Retails",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Retails",
                table: "Retails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Retails");
        }
    }
}
