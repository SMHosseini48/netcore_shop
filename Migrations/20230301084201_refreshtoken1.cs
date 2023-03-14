using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ncorep.Migrations
{
    public partial class refreshtoken1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7b19d5b9-c344-4b18-b132-d4a60cc36918");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "710712f4-a656-4b7b-b04e-ed5478f8db84");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00952cf5-d612-491f-9cf6-37a7c482a263", "AQAAAAEAACcQAAAAEPVS4ZcbcfXfKL4Wn8bW2B+uEV5s8qHpWC7cIfudqqNlPUv0+CGa7zHJ94SJThsSAA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d40e4547-a404-4906-b894-e92d3a7c95a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "175f81ee-10ef-4250-ad30-cfe2eb566ba9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "25781ad8-852f-4ef4-9026-7a06c730772d", "AQAAAAEAACcQAAAAELLTZQ6Jq2ko+lFwWj/fl++mt7CDvDPH1VxGXez9Hf1O+vjfwGLo51MHtmTzAcRLpA==" });
        }
    }
}
