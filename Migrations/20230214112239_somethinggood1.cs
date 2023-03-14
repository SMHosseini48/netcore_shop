using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ncorep.Migrations
{
    public partial class somethinggood1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9a0a1cf5-c325-4ca5-b1fb-678528b33a18");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fd3a9336-5822-4ff7-bbae-25f398bd946c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "25b4310c-4ca2-4239-a8bd-85d4d8250cf5", "AQAAAAEAACcQAAAAEJ+Jr8lE138Z61IAEciAL43YosAVm9C7/bGZ7uxiR0DiktMcM5dRhmsiDmCcW+/nyg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a9739fe0-0db5-41a6-8ff9-312c92a27e57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0f38cbfc-0321-4b5f-bf6b-3fd78447b7e5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3e2e2bf-6090-48c1-8aa8-dd1345c0ff9b", "AQAAAAEAACcQAAAAEHuZHPTMHfE1uArZhFOvwSAG++LNxQ4fWbtNsoq3ZVSxSwfY3y/sxmzeV1SMpXXKrA==" });
        }
    }
}
