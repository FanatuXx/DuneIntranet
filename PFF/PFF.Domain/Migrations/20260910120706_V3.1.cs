using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class V31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Patient_Mutuelle",
                table: "Patient");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Patient_Mutuelle",
                table: "Patient",
                sql: "Mutuelle IS NULL OR LEN(TRIM(Mutuelle)) > 0 ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Patient_Mutuelle",
                table: "Patient");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Patient_Mutuelle",
                table: "Patient",
                sql: "LEN(TRIM(Mutuelle)) > 0 ");
        }
    }
}
