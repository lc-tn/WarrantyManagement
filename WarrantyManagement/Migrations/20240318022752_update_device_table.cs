using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class update_device_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplacementDevice",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplacementDevice",
                table: "Devices",
                type: "int",
                nullable: true);
        }
    }
}
