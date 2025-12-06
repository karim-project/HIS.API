using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFloorNumberInRoomModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Rooms");
        }
    }
}
