using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddingCollectionPeriodsToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDeductions");

            migrationBuilder.DropTable(
                name: "PeriodBalances");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Payments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CollectionPeriodId",
                table: "MilkCollections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CollectionPeriodId",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MilkCollections_CollectionPeriodId",
                table: "MilkCollections",
                column: "CollectionPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CollectionPeriodId",
                table: "Expenses",
                column: "CollectionPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CollectionPeriods_CollectionPeriodId",
                table: "Expenses",
                column: "CollectionPeriodId",
                principalTable: "CollectionPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilkCollections_CollectionPeriods_CollectionPeriodId",
                table: "MilkCollections",
                column: "CollectionPeriodId",
                principalTable: "CollectionPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CollectionPeriods_CollectionPeriodId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkCollections_CollectionPeriods_CollectionPeriodId",
                table: "MilkCollections");

            migrationBuilder.DropIndex(
                name: "IX_MilkCollections_CollectionPeriodId",
                table: "MilkCollections");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CollectionPeriodId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CollectionPeriodId",
                table: "MilkCollections");

            migrationBuilder.DropColumn(
                name: "CollectionPeriodId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentDeductions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    RecordStatus = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDeductions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodBalances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CollectionPeriodId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FarmerId = table.Column<long>(type: "INTEGER", nullable: false),
                    RecordStatus = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalLitres = table.Column<decimal>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodBalances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDeductions_PaymentId",
                table: "PaymentDeductions",
                column: "PaymentId");
        }
    }
}
