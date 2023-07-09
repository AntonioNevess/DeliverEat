using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryEat.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_DetalhesPedidos_DetalhesPedidoFK",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "DetalhesPedidoPrato");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_DetalhesPedidoFK",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DetalhesPedidoFK",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "PedidosFK",
                table: "DetalhesPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PratoFK",
                table: "DetalhesPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesPedidos_PedidosFK",
                table: "DetalhesPedidos",
                column: "PedidosFK");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesPedidos_PratoFK",
                table: "DetalhesPedidos",
                column: "PratoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalhesPedidos_Pedidos_PedidosFK",
                table: "DetalhesPedidos",
                column: "PedidosFK",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalhesPedidos_Pratos_PratoFK",
                table: "DetalhesPedidos",
                column: "PratoFK",
                principalTable: "Pratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalhesPedidos_Pedidos_PedidosFK",
                table: "DetalhesPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalhesPedidos_Pratos_PratoFK",
                table: "DetalhesPedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetalhesPedidos_PedidosFK",
                table: "DetalhesPedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetalhesPedidos_PratoFK",
                table: "DetalhesPedidos");

            migrationBuilder.DropColumn(
                name: "PedidosFK",
                table: "DetalhesPedidos");

            migrationBuilder.DropColumn(
                name: "PratoFK",
                table: "DetalhesPedidos");

            migrationBuilder.AddColumn<int>(
                name: "DetalhesPedidoFK",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DetalhesPedidoPrato",
                columns: table => new
                {
                    ListaDetalhePedidosId = table.Column<int>(type: "int", nullable: false),
                    ListaPratosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesPedidoPrato", x => new { x.ListaDetalhePedidosId, x.ListaPratosId });
                    table.ForeignKey(
                        name: "FK_DetalhesPedidoPrato_DetalhesPedidos_ListaDetalhePedidosId",
                        column: x => x.ListaDetalhePedidosId,
                        principalTable: "DetalhesPedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalhesPedidoPrato_Pratos_ListaPratosId",
                        column: x => x.ListaPratosId,
                        principalTable: "Pratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DetalhesPedidoFK",
                table: "Pedidos",
                column: "DetalhesPedidoFK");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesPedidoPrato_ListaPratosId",
                table: "DetalhesPedidoPrato",
                column: "ListaPratosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_DetalhesPedidos_DetalhesPedidoFK",
                table: "Pedidos",
                column: "DetalhesPedidoFK",
                principalTable: "DetalhesPedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
