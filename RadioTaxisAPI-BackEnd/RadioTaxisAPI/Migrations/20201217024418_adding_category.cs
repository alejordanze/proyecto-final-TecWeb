using Microsoft.EntityFrameworkCore.Migrations;

namespace RadioTaxisAPI.Migrations
{
    public partial class adding_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Drivers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Drivers");
        }
    }
}
