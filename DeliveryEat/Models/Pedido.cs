using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    /// <summary>
    /// Descrição do pedido
    /// </summary>
    public class Pedido
    {
        /*
         * Modelo de BD no seguinte link
         * https://app.quickdatabasediagrams.com/#/d/yQ7Nly
         */

        public Pedido() { 
            ListaPrato = new HashSet<Prato>();    
        } 
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lista de Items do menu no carrinho de um utilizador especifico
        /// </summary>
        public ICollection<Prato> ListaPrato { get; set; }

        //*********************
        //Falta as FK

        /// <summary>
        /// FK para a tabela Pessoa
        /// </summary>
        [ForeignKey(nameof(Pessoas))]
        public int PessoaFK { get; set;}
        public Pessoa Pessoas { get; set; }
        
        /// <summary>
        /// Fk para a tabela DetalhesPedido
        /// </summary>
        [ForeignKey(nameof(DetalhesPedidos))]
        public int DetalhesPedidoFK { get; set; }
        public DetalhesPedido DetalhesPedidos { get; set; }
        
    }
}
