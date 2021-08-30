using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class AddImageNavigationKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Collections_CollectionId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Collections_CollectionId",
                table: "Images",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Collections_CollectionId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Collections_CollectionId",
                table: "Images",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
