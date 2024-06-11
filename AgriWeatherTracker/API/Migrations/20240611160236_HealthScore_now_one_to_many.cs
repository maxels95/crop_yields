using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class HealthScorenowonetomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "HealthScores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HealthScores_CropId",
                table: "HealthScores",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthScores_LocationId",
                table: "HealthScores",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthScores_Locations_LocationId",
                table: "HealthScores",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthScores_Locations_LocationId",
                table: "HealthScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores");

            migrationBuilder.DropIndex(
                name: "IX_HealthScores_CropId",
                table: "HealthScores");

            migrationBuilder.DropIndex(
                name: "IX_HealthScores_LocationId",
                table: "HealthScores");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "HealthScores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores",
                column: "CropId");
        }
    }
}
