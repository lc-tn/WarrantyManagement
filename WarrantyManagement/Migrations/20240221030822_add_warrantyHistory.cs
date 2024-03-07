using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class add_warrantyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_AspNetUsers_CustomerId",
                table: "Warranties");

            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Devices_DeviceId",
                table: "Warranties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warranties",
                table: "Warranties");

            migrationBuilder.RenameTable(
                name: "Warranties",
                newName: "Warranty");

            migrationBuilder.RenameIndex(
                name: "IX_Warranties_DeviceId",
                table: "Warranty",
                newName: "IX_Warranty_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Warranties_CustomerId",
                table: "Warranty",
                newName: "IX_Warranty_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warranty",
                table: "Warranty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranty_AspNetUsers_CustomerId",
                table: "Warranty",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warranty_Devices_DeviceId",
                table: "Warranty",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranty_AspNetUsers_CustomerId",
                table: "Warranty");

            migrationBuilder.DropForeignKey(
                name: "FK_Warranty_Devices_DeviceId",
                table: "Warranty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warranty",
                table: "Warranty");

            migrationBuilder.RenameTable(
                name: "Warranty",
                newName: "Warranties");

            migrationBuilder.RenameIndex(
                name: "IX_Warranty_DeviceId",
                table: "Warranties",
                newName: "IX_Warranties_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Warranty_CustomerId",
                table: "Warranties",
                newName: "IX_Warranties_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warranties",
                table: "Warranties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_AspNetUsers_CustomerId",
                table: "Warranties",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
