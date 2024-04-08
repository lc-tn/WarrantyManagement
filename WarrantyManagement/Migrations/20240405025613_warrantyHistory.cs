using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarrantyManagement.Migrations
{
    /// <inheritdoc />
    public partial class warrantyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "WarrantyHistories",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "WarrantyHistories");
        }
    }
}
