using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITBanking.Infrastructure.Persistence.Migrations
{
    public partial class AddNewProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Receptor",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Receptor",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receptor",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Receptor",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Payments");
        }
    }
}
