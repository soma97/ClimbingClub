using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingClub.Migrations
{
    public partial class Finalv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountLoaned",
                table: "GearLoanings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountAvailable",
                table: "GearItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountLoaned",
                table: "GearLoanings");

            migrationBuilder.DropColumn(
                name: "CountAvailable",
                table: "GearItems");
        }
    }
}
