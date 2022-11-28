using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWallet.Core.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountTypes_AccountTypeId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals");

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisible",
                table: "Subcategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Journals",
                type: "BLOB",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisible",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncome",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTypeId",
                table: "Accounts",
                type: "BLOB",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTypes_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId",
                principalTable: "AccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountTypes_AccountTypeId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals");

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisible",
                table: "Subcategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Journals",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "BLOB");

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisible",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncome",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTypeId",
                table: "Accounts",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "BLOB");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTypes_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId",
                principalTable: "AccountTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
