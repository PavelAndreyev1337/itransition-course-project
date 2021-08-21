using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectionApp.DAL.Migrations
{
    public partial class Changecollectiontypefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstBoolVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FirstDateFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FirstIntegerFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FirstStringFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FirstTextFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondBoolVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondDateFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondIntegerFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondStringFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondTextFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdBoolVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdDateFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdIntegerFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdStringFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdTextFieldVisible",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "FirstFieldType",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondFieldType",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThirdFieldType",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstFieldType",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondFieldType",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdFieldType",
                table: "Collections");

            migrationBuilder.AddColumn<bool>(
                name: "FirstBoolVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirstDateFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirstIntegerFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirstStringFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirstTextFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondBoolVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondDateFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondIntegerFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondStringFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondTextFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdBoolVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdDateFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdIntegerFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdStringFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdTextFieldVisible",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
