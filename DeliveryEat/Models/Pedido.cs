using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    /// <summary>
    /// Descrição do pedido
    /// </summary>
    public class Pedido
    {
        public Pedido()
        {
            ListaDetalhesPedido = new HashSet<DetalhesPedido>();
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        // Flag to indicate if the cart is confirmed
        public bool Confirmed { get; set; }
        public ICollection<DetalhesPedido> ListaDetalhesPedido { get; set; }

        //*********************

        /// <summary>
        /// FK para a tabela Pessoa
        /// </summary>
        [ForeignKey(nameof(Pessoas))]
        public int PessoaFK { get; set; }
        public Pessoa Pessoas { get; set; }

        /// <summary>
        /// 
        /// Fk para a tabela DetalhesPedido
        /// </summary>
        //[ForeignKey(nameof(DetalhesPedidos))]
        //public int DetalhesPedidoFK { get; set; }
        //public DetalhesPedido DetalhesPedidos { get; set; }

    }
}
