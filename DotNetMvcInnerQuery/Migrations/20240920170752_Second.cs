using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetMvcInnerQuery.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Providers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
