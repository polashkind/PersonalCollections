using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalCollections.Migrations
{
    /// <inheritdoc />
    public partial class addYearColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Items",
                newName: "MovieYear");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BookYear",
                table: "Items",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CarYear",
                table: "Items",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookYear",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CarYear",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "MovieYear",
                table: "Items",
                newName: "Year");
        }
    }
}
