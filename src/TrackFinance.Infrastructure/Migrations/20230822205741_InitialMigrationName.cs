using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackFinance.Infrastructure.Migrations;

public partial class InitialMigrationName : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Transactions",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
          TransactionDescriptionType = table.Column<int>(type: "int", nullable: false),
          ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
          UserId = table.Column<int>(type: "int", nullable: false),
          TransactionType = table.Column<int>(type: "int", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Transactions", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Projects",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(type: "nvarchar(max)", maxLength: 100, nullable: false),
          Priority = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Projects", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "ToDoItems",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
          IsDone = table.Column<bool>(type: "int", nullable: false),
          ProjectId = table.Column<int>(type: "int", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ToDoItems", x => x.Id);
          table.ForeignKey(
                    name: "FK_ToDoItems_Projects_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "Id");
        });

    migrationBuilder.CreateIndex(
        name: "IX_ToDoItems_ProjectId",
        table: "ToDoItems",
        column: "ProjectId");
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Transactions");

    migrationBuilder.DropTable(
        name: "ToDoItems");

    migrationBuilder.DropTable(
        name: "Projects");
  }
}
