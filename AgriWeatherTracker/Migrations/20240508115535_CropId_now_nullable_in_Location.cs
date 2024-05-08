using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class CropIdnownullableinLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Crops_CropId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "CropId",
                table: "Locations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Crops_CropId",
                table: "Locations",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Crops_CropId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "CropId",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Crops_CropId",
                table: "Locations",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
