using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCropLocationAndUpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CropLocations");

            migrationBuilder.AddColumn<int>(
                name: "CropId",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CropId",
                table: "Locations",
                column: "CropId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Crops_CropId",
                table: "Locations",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Crops_CropId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CropId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CropId",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "CropLocations",
                columns: table => new
                {
                    CropId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropLocations", x => new { x.CropId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_CropLocations_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CropLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CropLocations_LocationId",
                table: "CropLocations",
                column: "LocationId");
        }
    }
}
