using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionArchitecture.Persistance.Context.Migrations
{
    public partial class UpdatedPlantConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Plants_Name",
                table: "Plants",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plants_Name",
                table: "Plants");
        }
    }
}
