using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupPermissionsProject.Infrastructure.Migrations
{
    public partial class AddColumGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_UserGroups_userGroupID",
                table: "Permissions");

            migrationBuilder.AlterColumn<int>(
                name: "userGroupID",
                table: "Permissions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_UserGroups_userGroupID",
                table: "Permissions",
                column: "userGroupID",
                principalTable: "UserGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_UserGroups_userGroupID",
                table: "Permissions");

            migrationBuilder.AlterColumn<int>(
                name: "userGroupID",
                table: "Permissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_UserGroups_userGroupID",
                table: "Permissions",
                column: "userGroupID",
                principalTable: "UserGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
