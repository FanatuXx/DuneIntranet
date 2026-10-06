using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_AddressePatient_PatientAddressId",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressePatient",
                table: "AddressePatient");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Adresse_CodePostal",
                table: "AddressePatient");

            migrationBuilder.RenameTable(
                name: "AddressePatient",
                newName: "AdressePatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdressePatient",
                table: "AdressePatient",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Adresse_CodePostal",
                table: "AdressePatient",
                sql: "CodePostal BETWEEN 1000 AND 9999");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_AdressePatient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId",
                principalTable: "AdressePatient",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_AdressePatient_PatientAddressId",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdressePatient",
                table: "AdressePatient");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Adresse_CodePostal",
                table: "AdressePatient");

            migrationBuilder.RenameTable(
                name: "AdressePatient",
                newName: "AddressePatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressePatient",
                table: "AddressePatient",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Adresse_CodePostal",
                table: "AddressePatient",
                sql: "CodePostal >= 4");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_AddressePatient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId",
                principalTable: "AddressePatient",
                principalColumn: "Id");
        }
    }
}
