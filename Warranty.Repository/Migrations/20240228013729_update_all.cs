using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class update_all : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_CustomerId",
                table: "WarrantyHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_Devices_DeviceId",
                table: "WarrantyHistories");

            migrationBuilder.DropIndex(
                name: "IX_WarrantyHistories_CustomerId",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "WarrantyHistories");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "WarrantyHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WarrantyHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_UserId",
                table: "WarrantyHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_UserId",
                table: "WarrantyHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_Devices_DeviceId",
                table: "WarrantyHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_AspNetUsers_UserId",
                table: "WarrantyHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_Devices_DeviceId",
                table: "WarrantyHistories");

            migrationBuilder.DropIndex(
                name: "IX_WarrantyHistories_UserId",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WarrantyHistories");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "WarrantyHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "WarrantyHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "WarrantyHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "WarrantyHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "WarrantyHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_Devices_DeviceId",
                table: "WarrantyHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
