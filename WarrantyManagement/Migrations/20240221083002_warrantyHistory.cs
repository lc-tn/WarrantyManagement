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

            migrationBuilder.CreateTable(
                name: "WarrantyHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Technician = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Implementer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantyHistories_AspNetUsers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarrantyHistories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_CustomerId1",
                table: "WarrantyHistories",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_DeviceId",
                table: "WarrantyHistories",
                column: "DeviceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_AspNetUsers_CustomerId",
                table: "Warranties");

            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Devices_DeviceId",
                table: "Warranties");

            migrationBuilder.DropTable(
                name: "WarrantyHistories");

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
    }
}
