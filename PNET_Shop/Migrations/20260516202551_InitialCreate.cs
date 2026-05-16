using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PNET_Shop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    CHECK_NO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHECK_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TOTAL_SUM = table.Column<double>(type: "float", nullable: false),
                    CASHIER_NAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.CHECK_NO);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DEPT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    INFO = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DEPT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SUPPLIER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SUPPLIER_ID);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    GOOD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PRICE = table.Column<double>(type: "float", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRODUCER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DEPT_ID = table.Column<int>(type: "int", nullable: false),
                    SUPPLIER_ID = table.Column<int>(type: "int", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.GOOD_ID);
                    table.ForeignKey(
                        name: "FK_Goods_Departments_DEPT_ID",
                        column: x => x.DEPT_ID,
                        principalTable: "Departments",
                        principalColumn: "DEPT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goods_Suppliers_SUPPLIER_ID",
                        column: x => x.SUPPLIER_ID,
                        principalTable: "Suppliers",
                        principalColumn: "SUPPLIER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SALE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHECK_NO = table.Column<int>(type: "int", nullable: false),
                    GOOD_ID = table.Column<int>(type: "int", nullable: false),
                    DATE_SALE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SALE_ID);
                    table.ForeignKey(
                        name: "FK_Sales_Checks_CHECK_NO",
                        column: x => x.CHECK_NO,
                        principalTable: "Checks",
                        principalColumn: "CHECK_NO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Goods_GOOD_ID",
                        column: x => x.GOOD_ID,
                        principalTable: "Goods",
                        principalColumn: "GOOD_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_DEPT_ID",
                table: "Goods",
                column: "DEPT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_SUPPLIER_ID",
                table: "Goods",
                column: "SUPPLIER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CHECK_NO",
                table: "Sales",
                column: "CHECK_NO");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_GOOD_ID",
                table: "Sales",
                column: "GOOD_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
