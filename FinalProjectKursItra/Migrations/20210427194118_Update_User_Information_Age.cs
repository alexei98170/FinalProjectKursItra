using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectKursItra.Migrations
{
    public partial class Update_User_Information_Age : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "UserInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "UserInformations");
        }
    }
}
