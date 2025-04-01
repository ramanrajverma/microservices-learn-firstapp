using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservices.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class removingcoupontodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupouns",
                table: "Coupouns");

            migrationBuilder.RenameTable(
                name: "Coupouns",
                newName: "Coupons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons",
                column: "CouponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons");

            migrationBuilder.RenameTable(
                name: "Coupons",
                newName: "Coupouns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupouns",
                table: "Coupouns",
                column: "CouponId");
        }
    }
}
