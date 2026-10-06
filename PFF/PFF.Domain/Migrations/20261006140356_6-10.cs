using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class _610 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_PatientAddress_ZipCode",
                table: "AddressePatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_PatientAddress_ZipCode",
                table: "AddressePatient",
                sql: "ZipCode >= 4");
        }
    }
}
