using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Expenses.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Expense_PaymentMethodId",
                table: "Expense",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_PaymentMethod_PaymentMethodId",
                table: "Expense",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_PaymentMethod_PaymentMethodId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_PaymentMethodId",
                table: "Expense");
        }
    }
}
