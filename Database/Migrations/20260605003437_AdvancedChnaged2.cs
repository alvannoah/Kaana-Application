using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AdvancedChnaged2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSettled",
                table: "Advances");

            migrationBuilder.AddColumn<decimal>(
                name: "RepaidAmount",
                table: "Advances",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepaidAmount",
                table: "Advances");

            migrationBuilder.AddColumn<bool>(
                name: "IsSettled",
                table: "Advances",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
