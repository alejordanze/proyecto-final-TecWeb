using Microsoft.EntityFrameworkCore.Migrations;

namespace RadioTaxisAPI.Migrations
{
    public partial class adding_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ci",
                table: "Drivers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Expedido",
                table: "Drivers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "Drivers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ci",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Expedido",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Placa",
                table: "Drivers");
        }
    }
}
