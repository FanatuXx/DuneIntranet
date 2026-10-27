using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewConsumptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FréquenceConsommation",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "ProduitConsommé",
                table: "Patient");

            migrationBuilder.AlterColumn<double>(
                name: "IMC",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "FLOAT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Consommation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Produit = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Fréquence = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consommation", x => x.Id);
                    table.CheckConstraint("CK_Consommation_Fréquence", "LEN(TRIM(Fréquence)) > 0");
                    table.CheckConstraint("CK_Consommation_Produit", "LEN(TRIM(Produit)) > 0");
                    table.ForeignKey(
                        name: "FK_Consommation_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consommation_PatientId",
                table: "Consommation",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consommation");

            migrationBuilder.AddColumn<string>(
                name: "FréquenceConsommation",
                table: "Patient",
                type: "NVARCHAR(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProduitConsommé",
                table: "Patient",
                type: "NVARCHAR(50)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "IMC",
                table: "ParamètresVitaux",
                type: "FLOAT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "FLOAT");
        }
    }
}
