using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug.Data.Migrations
{
    public partial class ver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Provider_ProviderId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ProviderId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_ProviderId",
                table: "Account",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Provider_ProviderId",
                table: "Account",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
