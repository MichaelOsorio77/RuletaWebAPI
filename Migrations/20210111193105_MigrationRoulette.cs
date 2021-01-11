using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RuletaWebAPI.Migrations
{
    public partial class MigrationRoulette : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayedValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StakeValue = table.Column<double>(type: "float", nullable: false),
                    BetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdRoulette = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouletteItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetItems");

            migrationBuilder.DropTable(
                name: "RouletteItems");
        }
    }
}
