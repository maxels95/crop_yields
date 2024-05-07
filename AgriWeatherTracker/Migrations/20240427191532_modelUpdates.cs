using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class modelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WindSpeed",
                table: "Weathers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HistoricalAdverseImpactScore",
                table: "GrowthStages",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxHumidity",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxWindSpeed",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinHumidity",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinWindSpeed",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ResilienceDuration",
                table: "ConditionThresholds",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WindSpeed",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "HistoricalAdverseImpactScore",
                table: "GrowthStages");

            migrationBuilder.DropColumn(
                name: "MaxHumidity",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "MaxWindSpeed",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "MinHumidity",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "MinWindSpeed",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "ResilienceDuration",
                table: "ConditionThresholds");
        }
    }
}
