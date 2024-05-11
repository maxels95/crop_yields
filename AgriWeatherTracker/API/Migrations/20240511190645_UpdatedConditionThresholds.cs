using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedConditionThresholds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResilienceDuration",
                table: "ConditionThresholds",
                newName: "SevereResilienceDuration");

            migrationBuilder.RenameColumn(
                name: "MinWindSpeed",
                table: "ConditionThresholds",
                newName: "SevereMinTemp");

            migrationBuilder.AddColumn<double>(
                name: "ExtremeMaxTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ExtremeMinTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ExtremeResilienceDuration",
                table: "ConditionThresholds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MildMaxTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MildMinTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MildResilienceDuration",
                table: "ConditionThresholds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ModerateMaxTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ModerateMinTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ModerateResilienceDuration",
                table: "ConditionThresholds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SevereMaxTemp",
                table: "ConditionThresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtremeMaxTemp",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "ExtremeMinTemp",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "ExtremeResilienceDuration",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "MildMaxTemp",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "MildMinTemp",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "MildResilienceDuration",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "ModerateMaxTemp",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "ModerateMinTemp",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "ModerateResilienceDuration",
                table: "ConditionThresholds");

            migrationBuilder.DropColumn(
                name: "SevereMaxTemp",
                table: "ConditionThresholds");

            migrationBuilder.RenameColumn(
                name: "SevereResilienceDuration",
                table: "ConditionThresholds",
                newName: "ResilienceDuration");

            migrationBuilder.RenameColumn(
                name: "SevereMinTemp",
                table: "ConditionThresholds",
                newName: "MinWindSpeed");
        }
    }
}
