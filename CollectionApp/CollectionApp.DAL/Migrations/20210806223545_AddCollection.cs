using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class AddCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstFieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondFieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThirdFieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstIntegerFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    SecondIntegerFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    ThirdIntegerFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    FirstStringFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    SecondStringFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    ThirdStringFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    FirstTextFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    SecondTextFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    ThirdTextFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    FirstDateFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    SecondDateFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    ThirdDateFieldVisible = table.Column<bool>(type: "bit", nullable: false),
                    FirstBoolVisible = table.Column<bool>(type: "bit", nullable: false),
                    SecondBoolVisible = table.Column<bool>(type: "bit", nullable: false),
                    ThirdBoolVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CollectionId",
                table: "AspNetUsers",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Collections_CollectionId",
                table: "AspNetUsers",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Collections_CollectionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CollectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "AspNetUsers");
        }
    }
}
