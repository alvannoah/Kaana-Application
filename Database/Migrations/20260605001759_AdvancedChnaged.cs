using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AdvancedChnaged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilkCollections_CollectionPeriods_CollectionPeriodId",
                table: "MilkCollections");

            migrationBuilder.AlterColumn<long>(
                name: "CollectionPeriodId",
                table: "MilkCollections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MilkCollections_CollectionPeriods_CollectionPeriodId",
                table: "MilkCollections",
                column: "CollectionPeriodId",
                principalTable: "CollectionPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilkCollections_CollectionPeriods_CollectionPeriodId",
                table: "MilkCollections");

            migrationBuilder.AlterColumn<long>(
                name: "CollectionPeriodId",
                table: "MilkCollections",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_MilkCollections_CollectionPeriods_CollectionPeriodId",
                table: "MilkCollections",
                column: "CollectionPeriodId",
                principalTable: "CollectionPeriods",
                principalColumn: "Id");
        }
    }
}
