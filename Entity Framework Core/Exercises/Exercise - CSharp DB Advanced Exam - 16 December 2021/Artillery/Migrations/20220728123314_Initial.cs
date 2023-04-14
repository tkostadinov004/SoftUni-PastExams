using Microsoft.EntityFrameworkCore.Migrations;

namespace Artillery.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(maxLength: 60, nullable: false),
                    ArmySize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(maxLength: 40, nullable: false),
                    Founded = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shell",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShellWeight = table.Column<double>(nullable: false),
                    Caliber = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shell", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gun",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerId = table.Column<int>(nullable: false),
                    GunWeight = table.Column<int>(nullable: false),
                    BarrelLength = table.Column<double>(nullable: false),
                    NumberBuild = table.Column<int>(nullable: true),
                    Range = table.Column<int>(nullable: false),
                    GunType = table.Column<int>(nullable: false),
                    ShellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gun_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gun_Shell_ShellId",
                        column: x => x.ShellId,
                        principalTable: "Shell",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryGun",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false),
                    GunId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryGun", x => new { x.CountryId, x.GunId });
                    table.ForeignKey(
                        name: "FK_CountryGun_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryGun_Gun_GunId",
                        column: x => x.GunId,
                        principalTable: "Gun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryGun_GunId",
                table: "CountryGun",
                column: "GunId");

            migrationBuilder.CreateIndex(
                name: "IX_Gun_ManufacturerId",
                table: "Gun",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Gun_ShellId",
                table: "Gun",
                column: "ShellId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_ManufacturerName",
                table: "Manufacturer",
                column: "ManufacturerName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryGun");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Gun");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "Shell");
        }
    }
}
