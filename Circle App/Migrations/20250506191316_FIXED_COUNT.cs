using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Circle_App.Migrations
{
    /// <inheritdoc />
    public partial class FIXED_COUNT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coutn",
                table: "Hashtag",
                newName: "Count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Hashtag",
                newName: "Coutn");
        }
    }
}
