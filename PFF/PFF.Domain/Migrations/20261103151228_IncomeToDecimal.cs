using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class IncomeToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Revenus",
                table: "Patient",
                type: "DECIMAL(7,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Revenus",
                table: "Patient",
                type: "INT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,2)",
                oldNullable: true);
        }
    }
}
