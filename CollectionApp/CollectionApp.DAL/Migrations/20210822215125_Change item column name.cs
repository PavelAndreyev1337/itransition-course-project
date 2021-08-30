using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class Changeitemcolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThirdtText",
                table: "Items",
                newName: "ThirdText");

            migrationBuilder.RenameColumn(
                name: "ThirdtDate",
                table: "Items",
                newName: "ThirdDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThirdText",
                table: "Items",
                newName: "ThirdtText");

            migrationBuilder.RenameColumn(
                name: "ThirdDate",
                table: "Items",
                newName: "ThirdtDate");
        }
    }
}
