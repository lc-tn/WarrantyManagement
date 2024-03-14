using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class add_relationship_wanrrantyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistory_AspNetUsers_CustomerId",
                table: "WarrantyHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistory_Devices_DeviceId",
                table: "WarrantyHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarrantyHistory",
                table: "WarrantyHistory");

            migrationBuilder.RenameTable(
                name: "WarrantyHistory",
                newName: "WarrantyHistories");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyHistory_DeviceId",
                table: "WarrantyHistories",
                newName: "IX_WarrantyHistories_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyHistory_CustomerId",
                table: "WarrantyHistories",
                newName: "IX_WarrantyHistories_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WarrantyHistories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarrantyHistories",
                table: "WarrantyHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_WarrantyId",
                table: "WarrantyHistories",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId",
                table: "WarrantyHistories",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_Devices_DeviceId",
                table: "WarrantyHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_Warranties_WarrantyId",
                table: "WarrantyHistories",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId",
                table: "WarrantyHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_Devices_DeviceId",
                table: "WarrantyHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_Warranties_WarrantyId",
                table: "WarrantyHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarrantyHistories",
                table: "WarrantyHistories");

            migrationBuilder.DropIndex(
                name: "IX_WarrantyHistories_WarrantyId",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WarrantyHistories");

            migrationBuilder.RenameTable(
                name: "WarrantyHistories",
                newName: "WarrantyHistory");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyHistories_DeviceId",
                table: "WarrantyHistory",
                newName: "IX_WarrantyHistory_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyHistories_CustomerId",
                table: "WarrantyHistory",
                newName: "IX_WarrantyHistory_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarrantyHistory",
                table: "WarrantyHistory",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistory_AspNetUsers_CustomerId",
                table: "WarrantyHistory",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistory_Devices_DeviceId",
                table: "WarrantyHistory",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
