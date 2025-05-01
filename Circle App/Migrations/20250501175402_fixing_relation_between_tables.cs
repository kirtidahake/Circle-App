using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Circle_App.Migrations
{
    /// <inheritdoc />
    public partial class fixing_relation_between_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_UserId",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
