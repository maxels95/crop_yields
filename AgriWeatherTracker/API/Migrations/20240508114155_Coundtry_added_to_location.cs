﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class Coundtryaddedtolocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Locations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Locations");
        }
    }
}
