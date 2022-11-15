using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWallet.Core.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Journals",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_AccountId",
                table: "Journals",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_AccountId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Journals");
        }
    }
}
