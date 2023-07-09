using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryEat.Migrations
{
    /// <inheritdoc />
    public partial class FixPratoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PratoRestaurante");

            migrationBuilder.AddColumn<int>(
                name: "RestauranteFK",
                table: "Pratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_RestauranteFK",
                table: "Pratos",
                column: "RestauranteFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Pratos_Restaurantes_RestauranteFK",
                table: "Pratos",
                column: "RestauranteFK",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pratos_Restaurantes_RestauranteFK",
                table: "Pratos");

            migrationBuilder.DropIndex(
                name: "IX_Pratos_RestauranteFK",
                table: "Pratos");

            migrationBuilder.DropColumn(
                name: "RestauranteFK",
                table: "Pratos");

            migrationBuilder.CreateTable(
                name: "PratoRestaurante",
                columns: table => new
                {
                    ListaPratosId = table.Column<int>(type: "int", nullable: false),
                    ListaRestaurantesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PratoRestaurante", x => new { x.ListaPratosId, x.ListaRestaurantesId });
                    table.ForeignKey(
                        name: "FK_PratoRestaurante_Pratos_ListaPratosId",
                        column: x => x.ListaPratosId,
                        principalTable: "Pratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PratoRestaurante_Restaurantes_ListaRestaurantesId",
                        column: x => x.ListaRestaurantesId,
                        principalTable: "Restaurantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PratoRestaurante_ListaRestaurantesId",
                table: "PratoRestaurante",
                column: "ListaRestaurantesId");
        }
    }
}
