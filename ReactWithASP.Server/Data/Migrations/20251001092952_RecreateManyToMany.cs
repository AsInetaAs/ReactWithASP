using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactWithASP.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecreateManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyProgrammeSubject",
                columns: table => new
                {
                    StudyProgrammesId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyProgrammeSubject", x => new { x.StudyProgrammesId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_StudyProgrammeSubject_StudyProgrammes_StudyProgrammesId",
                        column: x => x.StudyProgrammesId,
                        principalTable: "StudyProgrammes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyProgrammeSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProgrammeSubject_SubjectsId",
                table: "StudyProgrammeSubject",
                column: "SubjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyProgrammeSubject");
        }
    }
}
