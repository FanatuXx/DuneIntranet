using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ModifOnConsultationAndWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathologyId",
                table: "Consultation");

            migrationBuilder.AlterColumn<string>(
                name: "Évaluation",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)");

            migrationBuilder.AlterColumn<string>(
                name: "Subjectif",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)");

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)");

            migrationBuilder.AlterColumn<string>(
                name: "Objectif",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Évaluation",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subjectif",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Objectif",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Consultation",
                type: "NVARCHAR(256)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PathologyId",
                table: "Consultation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
