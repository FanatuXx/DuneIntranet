using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_AddressePatient_PatientAddressId",
                table: "Patient");

            migrationBuilder.AlterColumn<bool>(
                name: "Travail",
                table: "Patient",
                type: "BIT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<int>(
                name: "Revenus",
                table: "Patient",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<string>(
                name: "ProduitConsommé",
                table: "Patient",
                type: "NVARCHAR(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(50)");

            migrationBuilder.AlterColumn<int>(
                name: "PatientAddressId",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FréquenceConsommation",
                table: "Patient",
                type: "NVARCHAR(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)");

            migrationBuilder.AlterColumn<bool>(
                name: "Fedasil",
                table: "Patient",
                type: "BIT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<bool>(
                name: "CarteMédicale",
                table: "Patient",
                type: "BIT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<bool>(
                name: "Assuré",
                table: "Patient",
                type: "BIT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_AddressePatient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId",
                principalTable: "AddressePatient",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_AddressePatient_PatientAddressId",
                table: "Patient");

            migrationBuilder.AlterColumn<bool>(
                name: "Travail",
                table: "Patient",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Revenus",
                table: "Patient",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProduitConsommé",
                table: "Patient",
                type: "NVARCHAR(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientAddressId",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FréquenceConsommation",
                table: "Patient",
                type: "NVARCHAR(128)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Fedasil",
                table: "Patient",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CarteMédicale",
                table: "Patient",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Assuré",
                table: "Patient",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_AddressePatient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId",
                principalTable: "AddressePatient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
