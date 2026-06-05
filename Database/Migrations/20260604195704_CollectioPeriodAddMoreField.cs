using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class CollectioPeriodAddMoreField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceDeductions");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "CollectionPeriods",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "CollectionPeriods");

            migrationBuilder.CreateTable(
                name: "AdvanceDeductions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdvanceId = table.Column<long>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: false),
                    AmountDeducted = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RecordStatus = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvanceDeductions_Advances_AdvanceId",
                        column: x => x.AdvanceId,
                        principalTable: "Advances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvanceDeductions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDeductions_AdvanceId",
                table: "AdvanceDeductions",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDeductions_PaymentId",
                table: "AdvanceDeductions",
                column: "PaymentId");
        }
    }
}
