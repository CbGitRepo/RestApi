using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularNetCoreSample.Migrations
{
    public partial class ee5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Commandes",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Commandes",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
