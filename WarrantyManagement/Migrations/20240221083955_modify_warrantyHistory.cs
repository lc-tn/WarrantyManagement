using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class modify_warrantyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId1",
                table: "WarrantyHistories");

            migrationBuilder.DropIndex(
                name: "IX_WarrantyHistories_CustomerId1",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "WarrantyHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "WarrantyHistories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_CustomerId",
                table: "WarrantyHistories",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId",
                table: "WarrantyHistories",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId",
                table: "WarrantyHistories");

            migrationBuilder.DropIndex(
                name: "IX_WarrantyHistories_CustomerId",
                table: "WarrantyHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "WarrantyHistories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "WarrantyHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_CustomerId1",
                table: "WarrantyHistories",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId1",
                table: "WarrantyHistories",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
