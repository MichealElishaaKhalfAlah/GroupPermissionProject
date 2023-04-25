using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupPermissionsProject.Infrastructure.Migrations
{
    public partial class UserGroupPermissionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedByNameId = table.Column<string>(nullable: true),
                    ModifiedByNameId = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: false),
                    EnglishName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserGroups_AspNetUsers_CreatedByNameId",
                        column: x => x.CreatedByNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroups_AspNetUsers_ModifiedByNameId",
                        column: x => x.ModifiedByNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedByNameId = table.Column<string>(nullable: true),
                    ModifiedByNameId = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    userGroupID = table.Column<int>(nullable: true),
                    PageId = table.Column<int>(nullable: false),
                    CanView = table.Column<bool>(nullable: true),
                    CanAdd = table.Column<bool>(nullable: true),
                    CanEdit = table.Column<bool>(nullable: true),
                    CanDelete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetUsers_CreatedByNameId",
                        column: x => x.CreatedByNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetUsers_ModifiedByNameId",
                        column: x => x.ModifiedByNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_UserGroups_userGroupID",
                        column: x => x.userGroupID,
                        principalTable: "UserGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", null, "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedByNameId",
                table: "Permissions",
                column: "CreatedByNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ModifiedByNameId",
                table: "Permissions",
                column: "ModifiedByNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_userGroupID",
                table: "Permissions",
                column: "userGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_CreatedByNameId",
                table: "UserGroups",
                column: "CreatedByNameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_ModifiedByNameId",
                table: "UserGroups",
                column: "ModifiedByNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
