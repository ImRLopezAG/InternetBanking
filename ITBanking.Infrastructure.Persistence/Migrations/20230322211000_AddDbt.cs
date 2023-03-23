using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITBanking.Infrastructure.Persistence.Migrations
{
    public partial class AddDbt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Dbt",
                table: "Products",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dbt",
                table: "Products");
        }
    }
}
