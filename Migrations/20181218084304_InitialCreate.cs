using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularNetCoreSample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Clients_ClientId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Commandes_CommandeId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Commandes",
                newName: "clientId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_ClientId",
                table: "Commandes",
                newName: "IX_Commandes_clientId");

            migrationBuilder.RenameColumn(
                name: "CommandeId",
                table: "Products",
                newName: "commandeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CommandeId",
                table: "Products",
                newName: "IX_Products_commandeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Clients_clientId",
                table: "Commandes",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Commandes_commandeId",
                table: "Products",
                column: "commandeId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Clients_clientId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Commandes_commandeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "Commandes",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_clientId",
                table: "Commandes",
                newName: "IX_Commandes_ClientId");

            migrationBuilder.RenameColumn(
                name: "commandeId",
                table: "Product",
                newName: "CommandeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_commandeId",
                table: "Product",
                newName: "IX_Product_CommandeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Clients_ClientId",
                table: "Commandes",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Commandes_CommandeId",
                table: "Product",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
