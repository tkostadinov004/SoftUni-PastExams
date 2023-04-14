using Microsoft.EntityFrameworkCore.Migrations;

namespace Footballers.Migrations
{
    public partial class Initiala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Footballer_Coach_CoachId",
                table: "Footballer");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamFootballer_Footballer_FootballerId",
                table: "TeamFootballer");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamFootballer_Team_TeamId",
                table: "TeamFootballer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamFootballer",
                table: "TeamFootballer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Footballer",
                table: "Footballer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coach",
                table: "Coach");

            migrationBuilder.RenameTable(
                name: "TeamFootballer",
                newName: "TeamsFootballers");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.RenameTable(
                name: "Footballer",
                newName: "Footballers");

            migrationBuilder.RenameTable(
                name: "Coach",
                newName: "Coaches");

            migrationBuilder.RenameIndex(
                name: "IX_TeamFootballer_TeamId",
                table: "TeamsFootballers",
                newName: "IX_TeamsFootballers_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballer_CoachId",
                table: "Footballers",
                newName: "IX_Footballers_CoachId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamsFootballers",
                table: "TeamsFootballers",
                columns: new[] { "FootballerId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Footballers",
                table: "Footballers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coaches",
                table: "Coaches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Footballers_Coaches_CoachId",
                table: "Footballers",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsFootballers_Footballers_FootballerId",
                table: "TeamsFootballers",
                column: "FootballerId",
                principalTable: "Footballers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamsFootballers_Teams_TeamId",
                table: "TeamsFootballers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Footballers_Coaches_CoachId",
                table: "Footballers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsFootballers_Footballers_FootballerId",
                table: "TeamsFootballers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamsFootballers_Teams_TeamId",
                table: "TeamsFootballers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamsFootballers",
                table: "TeamsFootballers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Footballers",
                table: "Footballers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coaches",
                table: "Coaches");

            migrationBuilder.RenameTable(
                name: "TeamsFootballers",
                newName: "TeamFootballer");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.RenameTable(
                name: "Footballers",
                newName: "Footballer");

            migrationBuilder.RenameTable(
                name: "Coaches",
                newName: "Coach");

            migrationBuilder.RenameIndex(
                name: "IX_TeamsFootballers_TeamId",
                table: "TeamFootballer",
                newName: "IX_TeamFootballer_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Footballers_CoachId",
                table: "Footballer",
                newName: "IX_Footballer_CoachId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamFootballer",
                table: "TeamFootballer",
                columns: new[] { "FootballerId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Footballer",
                table: "Footballer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coach",
                table: "Coach",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Footballer_Coach_CoachId",
                table: "Footballer",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamFootballer_Footballer_FootballerId",
                table: "TeamFootballer",
                column: "FootballerId",
                principalTable: "Footballer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamFootballer_Team_TeamId",
                table: "TeamFootballer",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
