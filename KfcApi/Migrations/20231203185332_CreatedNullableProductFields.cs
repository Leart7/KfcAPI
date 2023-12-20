using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KfcApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNullableProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_HomeCategories_HomeCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "HomeCategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_HomeCategories_HomeCategoryId",
                table: "Products",
                column: "HomeCategoryId",
                principalTable: "HomeCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_HomeCategories_HomeCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "HomeCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_HomeCategories_HomeCategoryId",
                table: "Products",
                column: "HomeCategoryId",
                principalTable: "HomeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
