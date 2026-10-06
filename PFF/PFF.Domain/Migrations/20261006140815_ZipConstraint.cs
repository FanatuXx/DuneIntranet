using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ZipConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Adresse_CodePostal",
                table: "AddressePatient",
                sql: "CodePostal >= 4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Adresse_CodePostal",
                table: "AddressePatient");
        }
    }
}
