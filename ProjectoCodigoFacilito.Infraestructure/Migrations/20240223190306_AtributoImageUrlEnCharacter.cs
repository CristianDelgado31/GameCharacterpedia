using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectoCodigoFacilito.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AtributoImageUrlEnCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Characters");
        }
    }
}
