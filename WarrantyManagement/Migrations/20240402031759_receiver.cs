using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class receiver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "WarrantyHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Warranties",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Warranties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Warranties");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Warranties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
