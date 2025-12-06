using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFromBedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beds_Admissions_CurrentAdmissionId",
                table: "Beds");

            migrationBuilder.DropIndex(
                name: "IX_Beds_CurrentAdmissionId",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "CurrentAdmissionId",
                table: "Beds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentAdmissionId",
                table: "Beds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beds_CurrentAdmissionId",
                table: "Beds",
                column: "CurrentAdmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beds_Admissions_CurrentAdmissionId",
                table: "Beds",
                column: "CurrentAdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
