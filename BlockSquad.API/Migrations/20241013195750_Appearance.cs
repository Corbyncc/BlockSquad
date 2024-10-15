using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlockSquad.API.Migrations
{
    /// <inheritdoc />
    public partial class Appearance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codename",
                table: "User",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appearance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FaceId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    HairId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    BeardId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    SkinColor = table.Column<string>(type: "longtext", nullable: true),
                    HairColor = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appearance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appearance_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Appearance_UserId",
                table: "Appearance",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appearance");

            migrationBuilder.DropColumn(
                name: "Codename",
                table: "User");
        }
    }
}
