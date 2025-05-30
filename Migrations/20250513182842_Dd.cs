﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prog7311_POE.Migrations
{
    /// <inheritdoc />
    public partial class Dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "Products");
        }
    }
}
