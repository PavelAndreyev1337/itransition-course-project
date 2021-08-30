using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class AddItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LikedItemId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    likes = table.Column<int>(type: "int", nullable: false),
                    FirstInteger = table.Column<int>(type: "int", nullable: false),
                    SecondInteger = table.Column<int>(type: "int", nullable: false),
                    ThirdInteger = table.Column<int>(type: "int", nullable: false),
                    FirstString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdtText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstDate = table.Column<DateTime>(type: "date", nullable: false),
                    SecondDate = table.Column<DateTime>(type: "date", nullable: false),
                    ThirdtDate = table.Column<DateTime>(type: "date", nullable: false),
                    FirstBoolean = table.Column<bool>(type: "bit", nullable: false),
                    SecondBoolean = table.Column<bool>(type: "bit", nullable: false),
                    ThirdBoolean = table.Column<bool>(type: "bit", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LikedItemId",
                table: "AspNetUsers",
                column: "LikedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId",
                table: "Items",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Items_LikedItemId",
                table: "AspNetUsers",
                column: "LikedItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Items_LikedItemId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LikedItemId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LikedItemId",
                table: "AspNetUsers");
        }
    }
}
