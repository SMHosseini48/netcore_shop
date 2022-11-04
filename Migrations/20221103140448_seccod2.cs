using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ncorep.Migrations
{
    public partial class seccod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0266550d-b346-400b-ae70-e77d43d3452e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "41b0f71c-efdb-464f-a8e7-02b0e90b1b52");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa8bbda4-aa6d-48ad-9460-7ec3ef3d3bc1", "AQAAAAEAACcQAAAAEJwqzZr77exJ4D2G6AtTlnVGw57hJLDbW/7TPvNavU9ewaB2IydZKpmDa0F4znojqw==" });
        }
    }
}
