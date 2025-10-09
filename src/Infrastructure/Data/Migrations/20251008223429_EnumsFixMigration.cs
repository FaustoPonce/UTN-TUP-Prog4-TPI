using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnumsFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutClasses_Users_employeeId",
                table: "WorkoutClasses");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "WorkoutClasses",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutClasses_employeeId",
                table: "WorkoutClasses",
                newName: "IX_WorkoutClasses_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutClasses_Users_EmployeeId",
                table: "WorkoutClasses",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutClasses_Users_EmployeeId",
                table: "WorkoutClasses");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "WorkoutClasses",
                newName: "employeeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutClasses_EmployeeId",
                table: "WorkoutClasses",
                newName: "IX_WorkoutClasses_employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutClasses_Users_employeeId",
                table: "WorkoutClasses",
                column: "employeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
