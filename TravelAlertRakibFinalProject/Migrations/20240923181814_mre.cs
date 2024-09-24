using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAlertRakibFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class mre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityId",
                table: "RoomFacilities");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "RoomFacilities",
                newName: "FacilityID");

            migrationBuilder.RenameIndex(
                name: "IX_RoomFacilities_FacilityId",
                table: "RoomFacilities",
                newName: "IX_RoomFacilities_FacilityID");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "RoomImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityID",
                table: "RoomFacilities",
                column: "FacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityID",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "RoomImages");

            migrationBuilder.RenameColumn(
                name: "FacilityID",
                table: "RoomFacilities",
                newName: "FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomFacilities_FacilityID",
                table: "RoomFacilities",
                newName: "IX_RoomFacilities_FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityId",
                table: "RoomFacilities",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
