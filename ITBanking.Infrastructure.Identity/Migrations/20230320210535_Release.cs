using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITBanking.Infrastructure.Identity.Migrations
{
    public partial class Release : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Idt",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Idt",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
