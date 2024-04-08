using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class replacementDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplacementDevice",
                table: "WarrantyDevices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReplacementDevice",
                table: "WarrantyDeviceHistories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplacementDevice",
                table: "WarrantyDevices");

            migrationBuilder.DropColumn(
                name: "ReplacementDevice",
                table: "WarrantyDeviceHistories");
        }
    }
}
