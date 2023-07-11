using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryEat.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pratos_Pedidos_PedidoId",
                table: "Pratos");

            migrationBuilder.DropIndex(
                name: "IX_Pratos_PedidoId",
                table: "Pratos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Pratos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Pratos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_PedidoId",
                table: "Pratos",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pratos_Pedidos_PedidoId",
                table: "Pratos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
