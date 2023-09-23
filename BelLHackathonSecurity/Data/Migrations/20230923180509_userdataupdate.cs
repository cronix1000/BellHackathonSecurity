using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelLHackathonSecurity.Data.Migrations
{
    public partial class userdataupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "userDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "userDatas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userDatas");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "userDatas");
        }
    }
}
