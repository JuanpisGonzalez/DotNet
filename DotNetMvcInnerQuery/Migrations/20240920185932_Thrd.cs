using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetMvcInnerQuery.Migrations
{
    /// <inheritdoc />
    public partial class Thrd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Sales");
        }
    }
}
