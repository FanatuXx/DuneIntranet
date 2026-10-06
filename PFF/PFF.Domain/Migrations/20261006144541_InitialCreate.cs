using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFF.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdressePatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rue = table.Column<string>(type: "NVARCHAR(256)", nullable: true),
                    Numéro = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    CodePostal = table.Column<int>(type: "INT", nullable: false),
                    Ville = table.Column<string>(type: "NVARCHAR(128)", nullable: true),
                    Pays = table.Column<string>(type: "NVARCHAR(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdressePatient", x => x.Id);
                    table.CheckConstraint("CK_Adresse_CodePostal", "CodePostal BETWEEN 1000 AND 9999");
                });

            migrationBuilder.CreateTable(
                name: "Médicament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    MéthodePrise = table.Column<string>(type: "NVARCHAR(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Médicament", x => x.Id);
                    table.CheckConstraint("CK_Médicament_MethodePrise", "LEN(TRIM(Nom)) > 0");
                    table.CheckConstraint("CK_Médicament_Nom", "LEN(TRIM(Nom)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "Pathologie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "NVARCHAR(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pathologie", x => x.Id);
                    table.CheckConstraint("CK_Pathologie_Nom", "LEN(TRIM(Nom)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "Travailleur",
                columns: table => new
                {
                    SSIN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prénom = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Nom = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Genre = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    NuméroTéléphone = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Fonction = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    Rue = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Numéro = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    CodePostal = table.Column<int>(type: "INT", nullable: false),
                    Ville = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    Pays = table.Column<string>(type: "NVARCHAR(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travailleur", x => x.SSIN);
                    table.CheckConstraint("CK_Travailleur_Email", "Email LIKE '%_@__%.__%'");
                    table.CheckConstraint("CK_Travailleur_Noms", "LEN(TRIM(Prénom)) > 0 AND LEN(TRIM(Nom)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NISS = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    NuméroID = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Prénom = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Nom = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Surnom = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Genre = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    DateNaissance = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    NuméroTéléphone = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    Allergies = table.Column<string>(type: "NVARCHAR(256)", nullable: true),
                    Assuré = table.Column<bool>(type: "BIT", nullable: true),
                    Mutuelle = table.Column<string>(type: "NVARCHAR(128)", nullable: true),
                    CarteMédicale = table.Column<bool>(type: "BIT", nullable: true),
                    ExpirationCarteMédicale = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Fedasil = table.Column<bool>(type: "BIT", nullable: true),
                    Revenus = table.Column<int>(type: "INT", nullable: true),
                    Statut = table.Column<string>(type: "NVARCHAR(128)", nullable: true),
                    Travail = table.Column<bool>(type: "BIT", nullable: true),
                    ProduitConsommé = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    FréquenceConsommation = table.Column<string>(type: "NVARCHAR(128)", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    DateDernièreVisite = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    PatientAddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.CheckConstraint("CK_Patient_DernièreVisite", "DateDernièreVisite >= DateInscription AND DateDernièreVisite <= GETDATE()");
                    table.CheckConstraint("CK_Patient_Identification", "LEN(TRIM(Prénom)) > 0 OR LEN(TRIM(Nom)) > 0 OR LEN(TRIM(Surnom)) > 0");
                    table.ForeignKey(
                        name: "FK_Patient_AdressePatient_PatientAddressId",
                        column: x => x.PatientAddressId,
                        principalTable: "AdressePatient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Consultation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motif = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Subjectif = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Objectif = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Évaluation = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Plan = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PathologyId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultation", x => x.Id);
                    table.CheckConstraint("CK_Consultation_Date", "Date <= GETDATE()");
                    table.ForeignKey(
                        name: "FK_Consultation_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TTChronique",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Posologie = table.Column<string>(type: "NVARCHAR(256)", nullable: false),
                    PathologyId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTChronique", x => x.Id);
                    table.CheckConstraint("CK_TTChronique_Posologie", "LEN(Posologie) > 0");
                    table.ForeignKey(
                        name: "FK_TTChronique_Pathologie_PathologyId",
                        column: x => x.PathologyId,
                        principalTable: "Pathologie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TTChronique_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultations_Pathologies_JoinTable",
                columns: table => new
                {
                    ConsultationsId = table.Column<int>(type: "int", nullable: false),
                    PathologiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations_Pathologies_JoinTable", x => new { x.ConsultationsId, x.PathologiesId });
                    table.ForeignKey(
                        name: "FK_Consultations_Pathologies_JoinTable_Consultation_ConsultationsId",
                        column: x => x.ConsultationsId,
                        principalTable: "Consultation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_Pathologies_JoinTable_Pathologie_PathologiesId",
                        column: x => x.PathologiesId,
                        principalTable: "Pathologie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultations_Travailleurs_JoinTable",
                columns: table => new
                {
                    ConsultationsId = table.Column<int>(type: "int", nullable: false),
                    WorkersSSIN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations_Travailleurs_JoinTable", x => new { x.ConsultationsId, x.WorkersSSIN });
                    table.ForeignKey(
                        name: "FK_Consultations_Travailleurs_JoinTable_Consultation_ConsultationsId",
                        column: x => x.ConsultationsId,
                        principalTable: "Consultation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_Travailleurs_JoinTable_Travailleur_WorkersSSIN",
                        column: x => x.WorkersSSIN,
                        principalTable: "Travailleur",
                        principalColumn: "SSIN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamètresVitaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FréquenceCardiaque = table.Column<int>(type: "INT", nullable: false),
                    PressionArtérielle = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Température = table.Column<decimal>(type: "DECIMAL(3,1)", nullable: false),
                    Saturation = table.Column<int>(type: "INT", nullable: false),
                    Taille = table.Column<double>(type: "FLOAT", nullable: false),
                    Poids = table.Column<int>(type: "INT", nullable: false),
                    IMC = table.Column<double>(type: "FLOAT", nullable: false),
                    Glycémie = table.Column<double>(type: "FLOAT", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamètresVitaux", x => x.Id);
                    table.CheckConstraint("CK_Paramètres_FréquenceCardiaque", "FréquenceCardiaque <= 220");
                    table.CheckConstraint("CK_Paramètres_Saturation", "Saturation >= 1 AND Saturation <= 100");
                    table.CheckConstraint("CK_Paramètres_Température", "Température >= 30 AND Température <= 45");
                    table.ForeignKey(
                        name: "FK_ParamètresVitaux_Consultation_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDébut = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DateFin = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FréquencePrise = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    WorkerSSIN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                    table.CheckConstraint("CK_Prescription_Dates", "DateDébut < DateFin");
                    table.ForeignKey(
                        name: "FK_Prescription_Consultation_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescription_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescription_Travailleur_WorkerSSIN",
                        column: x => x.WorkerSSIN,
                        principalTable: "Travailleur",
                        principalColumn: "SSIN");
                });

            migrationBuilder.CreateTable(
                name: "TTChronique_Médicaments_JoinTable",
                columns: table => new
                {
                    ChronicTreatmentsId = table.Column<int>(type: "int", nullable: false),
                    MedicinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTChronique_Médicaments_JoinTable", x => new { x.ChronicTreatmentsId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_TTChronique_Médicaments_JoinTable_Médicament_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Médicament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TTChronique_Médicaments_JoinTable_TTChronique_ChronicTreatmentsId",
                        column: x => x.ChronicTreatmentsId,
                        principalTable: "TTChronique",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions_Médicaments_JoinTable",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions_Médicaments_JoinTable", x => new { x.MedicinesId, x.PrescriptionsId });
                    table.ForeignKey(
                        name: "FK_Prescriptions_Médicaments_JoinTable_Médicament_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Médicament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Médicaments_JoinTable_Prescription_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_PatientId",
                table: "Consultation",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_Pathologies_JoinTable_PathologiesId",
                table: "Consultations_Pathologies_JoinTable",
                column: "PathologiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_Travailleurs_JoinTable_WorkersSSIN",
                table: "Consultations_Travailleurs_JoinTable",
                column: "WorkersSSIN");

            migrationBuilder.CreateIndex(
                name: "IX_Médicament_Nom",
                table: "Médicament",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamètresVitaux_ConsultationId",
                table: "ParamètresVitaux",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pathologie_Nom",
                table: "Pathologie",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_NISS",
                table: "Patient",
                column: "NISS",
                unique: true,
                filter: "[NISS] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PatientAddressId",
                table: "Patient",
                column: "PatientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_ConsultationId",
                table: "Prescription",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_WorkerSSIN",
                table: "Prescription",
                column: "WorkerSSIN");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Médicaments_JoinTable_PrescriptionsId",
                table: "Prescriptions_Médicaments_JoinTable",
                column: "PrescriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_TTChronique_PathologyId",
                table: "TTChronique",
                column: "PathologyId");

            migrationBuilder.CreateIndex(
                name: "IX_TTChronique_PatientId",
                table: "TTChronique",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TTChronique_Médicaments_JoinTable_MedicinesId",
                table: "TTChronique_Médicaments_JoinTable",
                column: "MedicinesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultations_Pathologies_JoinTable");

            migrationBuilder.DropTable(
                name: "Consultations_Travailleurs_JoinTable");

            migrationBuilder.DropTable(
                name: "ParamètresVitaux");

            migrationBuilder.DropTable(
                name: "Prescriptions_Médicaments_JoinTable");

            migrationBuilder.DropTable(
                name: "TTChronique_Médicaments_JoinTable");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Médicament");

            migrationBuilder.DropTable(
                name: "TTChronique");

            migrationBuilder.DropTable(
                name: "Consultation");

            migrationBuilder.DropTable(
                name: "Travailleur");

            migrationBuilder.DropTable(
                name: "Pathologie");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "AdressePatient");
        }
    }
}
