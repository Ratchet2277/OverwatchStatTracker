using Microsoft.EntityFrameworkCore.Migrations;

namespace Initializer.Migrations
{
    public partial class BindSquadMembersToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SquadMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainUserId",
                table: "SquadMembers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SquadMembers_MainUserId",
                table: "SquadMembers",
                column: "MainUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SquadMembers_AspNetUsers_MainUserId",
                table: "SquadMembers",
                column: "MainUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SquadMembers_AspNetUsers_MainUserId",
                table: "SquadMembers");

            migrationBuilder.DropIndex(
                name: "IX_SquadMembers_MainUserId",
                table: "SquadMembers");

            migrationBuilder.DropColumn(
                name: "MainUserId",
                table: "SquadMembers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SquadMembers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
