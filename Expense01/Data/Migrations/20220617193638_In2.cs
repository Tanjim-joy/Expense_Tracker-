using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expense01.Data.Migrations
{
    public partial class In2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.CreateTable(
                name: "dailyExpense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    EDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailyExpense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_dailyExpense_categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "categories",
                        principalColumn: "CategoriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dailyExpense_CategoriesId",
                table: "dailyExpense",
                column: "CategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailyExpense");

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    EDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_expenses_categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "categories",
                        principalColumn: "CategoriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expenses_CategoriesId",
                table: "expenses",
                column: "CategoriesId");
        }
    }
}
