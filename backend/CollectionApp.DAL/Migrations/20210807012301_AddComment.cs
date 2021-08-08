using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class AddComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentItem",
                columns: table => new
                {
                    CommentsId = table.Column<int>(type: "int", nullable: false),
                    ItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentItem", x => new { x.CommentsId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_CommentItem_Comment_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentItem_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentItem_ItemsId",
                table: "CommentItem",
                column: "ItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentItem");

            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
