using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryEat.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalhesPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesPedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaFK = table.Column<int>(type: "int", nullable: false),
                    DetalhesPedidoFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_DetalhesPedidos_DetalhesPedidoFK",
                        column: x => x.DetalhesPedidoFK,
                        principalTable: "DetalhesPedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Pessoas_PessoaFK",
                        column: x => x.PessoaFK,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pratos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id");
                });

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
                name: "IX_DetalhesPedidoPrato_ListaPratosId",
                table: "DetalhesPedidoPrato",
                column: "ListaPratosId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DetalhesPedidoFK",
                table: "Pedidos",
                column: "DetalhesPedidoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_PessoaFK",
                table: "Pedidos",
                column: "PessoaFK");

            migrationBuilder.CreateIndex(
                name: "IX_PratoRestaurante_ListaRestaurantesId",
                table: "PratoRestaurante",
                column: "ListaRestaurantesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pratos_PedidoId",
                table: "Pratos",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalhesPedidoPrato");

            migrationBuilder.DropTable(
                name: "PratoRestaurante");

            migrationBuilder.DropTable(
                name: "Pratos");

            migrationBuilder.DropTable(
                name: "Restaurantes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "DetalhesPedidos");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
