using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Hp = table.Column<double>(type: "REAL", nullable: false),
                    HpGain = table.Column<double>(type: "REAL", nullable: false),
                    Mana = table.Column<double>(type: "REAL", nullable: false),
                    ManaGain = table.Column<double>(type: "REAL", nullable: false),
                    Ad = table.Column<double>(type: "REAL", nullable: false),
                    As = table.Column<double>(type: "REAL", nullable: false),
                    Armor = table.Column<double>(type: "REAL", nullable: false),
                    ArmorGain = table.Column<double>(type: "REAL", nullable: false),
                    Mr = table.Column<double>(type: "REAL", nullable: false),
                    MS = table.Column<double>(type: "REAL", nullable: false),
                    Range = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Champions");
        }
    }
}
