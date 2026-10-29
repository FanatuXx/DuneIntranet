using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class convertOptionalEntitiesParametersToNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                table: "ParamètresVitaux");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_AdressePatient_PatientAddressId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "ParamètresVitaux");

            migrationBuilder.AlterColumn<int>(
                name: "PatientAddressId",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsultationId",
                table: "ParamètresVitaux",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                table: "ParamètresVitaux",
                column: "ConsultationId",
                principalTable: "Consultation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_AdressePatient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId",
                principalTable: "AdressePatient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                table: "ParamètresVitaux");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_AdressePatient_PatientAddressId",
                table: "Patient");

            migrationBuilder.AlterColumn<int>(
                name: "PatientAddressId",
                table: "Patient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultationId",
                table: "ParamètresVitaux",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "ParamètresVitaux",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                table: "ParamètresVitaux",
                column: "ConsultationId",
                principalTable: "Consultation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_AdressePatient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId",
                principalTable: "AdressePatient",
                principalColumn: "Id");
        }
    }
}
