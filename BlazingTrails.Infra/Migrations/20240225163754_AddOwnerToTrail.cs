using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingTrails.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerToTrail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "trails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "trails");
        }
    }
}
