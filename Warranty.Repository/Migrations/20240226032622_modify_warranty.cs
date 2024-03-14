using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class modify_warranty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WarrantyDevices");

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Warranties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "Warranties",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Warranties");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "WarrantyDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
