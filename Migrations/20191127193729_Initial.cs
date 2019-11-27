using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingClub.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loanings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ExpectedReturnDate = table.Column<DateTime>(nullable: false),
                    LoanDate = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loanings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loanings_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembershipFees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    IsMonthly = table.Column<bool>(nullable: false),
                    MemberId = table.Column<int>(nullable: true),
                    Payment = table.Column<DateTime>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipFees_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    TrainingDate = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.TrainingDate);
                    table.ForeignKey(
                        name: "FK_Trainings_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    LoaningId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GearItems_Loanings_LoaningId",
                        column: x => x.LoaningId,
                        principalTable: "Loanings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GearItems_LoaningId",
                table: "GearItems",
                column: "LoaningId");

            migrationBuilder.CreateIndex(
                name: "IX_Loanings_MemberId",
                table: "Loanings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFees_MemberId",
                table: "MembershipFees",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_MemberId",
                table: "Trainings",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearItems");

            migrationBuilder.DropTable(
                name: "MembershipFees");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Loanings");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
