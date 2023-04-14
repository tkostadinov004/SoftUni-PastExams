using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Footballers.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    Trophies = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footballer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ContractStartDate = table.Column<DateTime>(nullable: false),
                    ContractEndDate = table.Column<DateTime>(nullable: false),
                    PositionType = table.Column<int>(nullable: false),
                    BestSkillType = table.Column<int>(nullable: false),
                    CoachId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footballer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Footballer_Coach_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coach",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamFootballer",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    FootballerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamFootballer", x => new { x.FootballerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamFootballer_Footballer_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamFootballer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Footballer_CoachId",
                table: "Footballer",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamFootballer_TeamId",
                table: "TeamFootballer",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamFootballer");

            migrationBuilder.DropTable(
                name: "Footballer");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Coach");
        }
    }
}
