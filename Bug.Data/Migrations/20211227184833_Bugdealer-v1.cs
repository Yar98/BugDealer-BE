using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug.Data.Migrations
{
    public partial class Bugdealerv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Timezone_TimezoneId",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timezone",
                table: "Timezone");

            migrationBuilder.DropIndex(
                name: "IX_Account_TimezoneId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "TimezoneId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Worklog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Timezone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Timezone",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timezone",
                table: "Timezone",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Timezone",
                table: "Timezone");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Timezone");

            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Timezone",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TimezoneId",
                table: "Account",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timezone",
                table: "Timezone",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Account_TimezoneId",
                table: "Account",
                column: "TimezoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Timezone_TimezoneId",
                table: "Account",
                column: "TimezoneId",
                principalTable: "Timezone",
                principalColumn: "CountryCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
