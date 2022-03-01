using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorsID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizesID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ColorsID",
                table: "Carts",
                column: "ColorsID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_SizesID",
                table: "Carts",
                column: "SizesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Colors_ColorsID",
                table: "Carts",
                column: "ColorsID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Sizes_SizesID",
                table: "Carts",
                column: "SizesID",
                principalTable: "Sizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Colors_ColorsID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Sizes_SizesID",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ColorsID",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_SizesID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ColorsID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "SizesID",
                table: "Carts");
        }
    }
}
