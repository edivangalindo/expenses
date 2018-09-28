using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expenses.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Revenue",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Revenue",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Expense",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Expense",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Revenue",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Revenue");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Revenue",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Revenue",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Expense",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Expense",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bill = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.Id);
                });
        }
    }
}
