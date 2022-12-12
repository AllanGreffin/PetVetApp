using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetVetApp.Migrations
{
    public partial class ReferenciasNullabas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlertType",
                table: "Alert",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertType",
                table: "Alert");
        }
    }
}
