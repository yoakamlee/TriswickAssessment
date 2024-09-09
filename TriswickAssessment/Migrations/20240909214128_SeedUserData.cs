using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriswickAssessment.Migrations
{
    public partial class SeedUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2024, 8, 30, 23, 41, 27, 696, DateTimeKind.Local).AddTicks(4574), new DateTime(2024, 8, 30, 23, 41, 27, 696, DateTimeKind.Local).AddTicks(4588) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2024, 9, 4, 23, 41, 27, 696, DateTimeKind.Local).AddTicks(4590), new DateTime(2024, 9, 4, 23, 41, 27, 696, DateTimeKind.Local).AddTicks(4591) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2024, 9, 7, 23, 41, 27, 696, DateTimeKind.Local).AddTicks(4592), new DateTime(2024, 9, 7, 23, 41, 27, 696, DateTimeKind.Local).AddTicks(4593) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserRole", "Username" },
                values: new object[,]
                {
                    { "user1", "Password123", "Regular", "regUser" },
                    { "user2", "ModPassword123", "Moderator", "modUser" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "user2");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2024, 8, 30, 23, 20, 15, 826, DateTimeKind.Local).AddTicks(7957), new DateTime(2024, 8, 30, 23, 20, 15, 826, DateTimeKind.Local).AddTicks(7969) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2024, 9, 4, 23, 20, 15, 826, DateTimeKind.Local).AddTicks(7971), new DateTime(2024, 9, 4, 23, 20, 15, 826, DateTimeKind.Local).AddTicks(7971) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2024, 9, 7, 23, 20, 15, 826, DateTimeKind.Local).AddTicks(7972), new DateTime(2024, 9, 7, 23, 20, 15, 826, DateTimeKind.Local).AddTicks(7973) });
        }
    }
}
