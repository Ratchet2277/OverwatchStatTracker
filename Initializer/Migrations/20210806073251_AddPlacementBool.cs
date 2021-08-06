using Microsoft.EntityFrameworkCore.Migrations;

namespace Initializer.Migrations
{
    public partial class AddPlacementBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlacement",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlacement",
                table: "Games");
        }
    }
}
