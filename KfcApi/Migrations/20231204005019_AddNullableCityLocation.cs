using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KfcApi.Migrations
{
    public partial class AddNullableCityLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter the Locations table, making the City column nullable
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                nullable: true, // Set to true to make it nullable
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: false); // Update according to your existing configuration
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
