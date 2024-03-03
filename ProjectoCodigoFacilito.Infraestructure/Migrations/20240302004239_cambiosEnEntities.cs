using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectoCodigoFacilito.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class cambiosEnEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Characters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
