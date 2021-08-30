using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class AddLikesRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Items_LikedItemId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LikedItemId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LikedItemId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ItemUser",
                columns: table => new
                {
                    LikedItemsId = table.Column<int>(type: "int", nullable: false),
                    UsersLikedId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUser", x => new { x.LikedItemsId, x.UsersLikedId });
                    table.ForeignKey(
                        name: "FK_ItemUser_AspNetUsers_UsersLikedId",
                        column: x => x.UsersLikedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemUser_Items_LikedItemsId",
                        column: x => x.LikedItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemUser_UsersLikedId",
                table: "ItemUser",
                column: "UsersLikedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemUser");

            migrationBuilder.AddColumn<int>(
                name: "LikedItemId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LikedItemId",
                table: "AspNetUsers",
                column: "LikedItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Items_LikedItemId",
                table: "AspNetUsers",
                column: "LikedItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
