using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class modify_warrantyDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarrantyDevices",
                table: "WarrantyDevices");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WarrantyDevices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarrantyDevices",
                table: "WarrantyDevices",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarrantyDevices",
                table: "WarrantyDevices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WarrantyDevices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarrantyDevices",
                table: "WarrantyDevices",
                columns: new[] { "WarrantyId", "DeviceId" });
        }
    }
}
