using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class add_creator_warranty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Devices_DeviceId",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_DeviceId",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Warranties");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Warranties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Warranties");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Warranties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_DeviceId",
                table: "Warranties",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Devices_DeviceId",
                table: "Warranties",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
