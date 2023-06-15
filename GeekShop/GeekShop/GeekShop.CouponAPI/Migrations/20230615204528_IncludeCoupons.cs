using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShop.CouponAPI.Migrations
{
    public partial class IncludeCoupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "id", "coupon_Code", "discount" },
                values: new object[] { 1L, "NICAO_10", 10m });

            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "id", "coupon_Code", "discount" },
                values: new object[] { 2L, "NICAO_20", 20m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
