using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddingDateToVitalSign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                table: "ParamètresVitaux");

            migrationBuilder.AlterColumn<decimal>(
                name: "Température",
                table: "ParamètresVitaux",
                type: "DECIMAL(3,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(3,1)");

            migrationBuilder.AlterColumn<double>(
                name: "Taille",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "FLOAT");

            migrationBuilder.AlterColumn<string>(
                name: "PressionArtérielle",
                table: "ParamètresVitaux",
                type: "NVARCHAR(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(50)");

            migrationBuilder.AlterColumn<int>(
                name: "Poids",
                table: "ParamètresVitaux",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<double>(
                name: "IMC",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "FLOAT");

            migrationBuilder.AlterColumn<double>(
                name: "Glycémie",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "FLOAT");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultationId",
                table: "ParamètresVitaux",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ParamètresVitaux",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                table: "ParamètresVitaux");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ParamètresVitaux");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "ParamètresVitaux");

            migrationBuilder.AlterColumn<decimal>(
                name: "Température",
                table: "ParamètresVitaux",
                type: "DECIMAL(3,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(3,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Taille",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "FLOAT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PressionArtérielle",
                table: "ParamètresVitaux",
                type: "NVARCHAR(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Poids",
                table: "ParamètresVitaux",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "IMC",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "FLOAT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Glycémie",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "FLOAT",
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
        }
    }
}
