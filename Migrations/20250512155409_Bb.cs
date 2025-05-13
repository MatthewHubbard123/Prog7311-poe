using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prog7311_POE.Migrations
{
    /// <inheritdoc />
    public partial class Bb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmerNameId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmerNameId",
                table: "Products",
                column: "FarmerNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_FarmerNameId",
                table: "Products",
                column: "FarmerNameId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_FarmerNameId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FarmerNameId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FarmerNameId",
                table: "Products");
        }
    }
}
