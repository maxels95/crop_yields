using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthScoreupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores");

            migrationBuilder.DropIndex(
                name: "IX_HealthScores_CropId",
                table: "HealthScores");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HealthScores",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores",
                column: "CropId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HealthScores",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthScores",
                table: "HealthScores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HealthScores_CropId",
                table: "HealthScores",
                column: "CropId",
                unique: true);
        }
    }
}
