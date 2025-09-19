using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace questions_Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enonce = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Reponse1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Reponse2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Reponse3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Reponse4 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BonneReponse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CategorieId = table.Column<int>(type: "int", nullable: true),
                    QuestionnaireId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Questions_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Libelle" },
                values: new object[,]
                {
                    { 1, "#FF5733", "Mathématiques" },
                    { 2, "#33FF57", "Histoire" },
                    { 3, "#3357FF", "Science" }
                });

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "Id", "Titre" },
                values: new object[,]
                {
                    { 1, "Quiz Général" },
                    { 2, "Quiz Avancé" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "BonneReponse", "CategorieId", "Enonce", "QuestionnaireId", "Reponse1", "Reponse2", "Reponse3", "Reponse4" },
                values: new object[,]
                {
                    { 1, "Paris", 2, "Quelle est la capitale de la France ?", 1, "Paris", "Londres", "Berlin", "Madrid" },
                    { 2, "4", 1, "Combien font 2 + 2 ?", 1, "3", "4", "5", "6" },
                    { 3, "Oxygène", 3, "Quel est l'élément chimique avec le symbole 'O' ?", 2, "Or", "Oxygène", "Argent", "Hydrogène" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategorieId",
                table: "Questions",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Questionnaires");
        }
    }
}
