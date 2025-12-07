using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAdmisson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Beds_BedId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Rooms_RoomId",
                table: "Admissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Admissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BedId",
                table: "Admissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Beds_BedId",
                table: "Admissions",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Rooms_RoomId",
                table: "Admissions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Beds_BedId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Rooms_RoomId",
                table: "Admissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Admissions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BedId",
                table: "Admissions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Beds_BedId",
                table: "Admissions",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Rooms_RoomId",
                table: "Admissions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
