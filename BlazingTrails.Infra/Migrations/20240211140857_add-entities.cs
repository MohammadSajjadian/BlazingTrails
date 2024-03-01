using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingTrails.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "waypoints",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_waypoints", x => x.id);
                    table.ForeignKey(
                        name: "FK_waypoints_trails_trailId",
                        column: x => x.trailId,
                        principalTable: "trails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_waypoints_trailId",
                table: "waypoints",
                column: "trailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "waypoints");

            migrationBuilder.DropTable(
                name: "trails");
        }
    }
}
