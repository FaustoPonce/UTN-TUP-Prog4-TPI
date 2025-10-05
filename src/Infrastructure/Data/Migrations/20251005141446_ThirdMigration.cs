using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WorkoutClasses_WorkoutClassId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkoutClassId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkoutClassId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "MemberWorkoutClass",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    WorkoutClassesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberWorkoutClass", x => new { x.MembersId, x.WorkoutClassesId });
                    table.ForeignKey(
                        name: "FK_MemberWorkoutClass_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberWorkoutClass_WorkoutClasses_WorkoutClassesId",
                        column: x => x.WorkoutClassesId,
                        principalTable: "WorkoutClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MemberWorkoutClass_WorkoutClassesId",
                table: "MemberWorkoutClass",
                column: "WorkoutClassesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberWorkoutClass");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutClassId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkoutClassId",
                table: "Users",
                column: "WorkoutClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WorkoutClasses_WorkoutClassId",
                table: "Users",
                column: "WorkoutClassId",
                principalTable: "WorkoutClasses",
                principalColumn: "Id");
        }
    }
}
