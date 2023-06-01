using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    public class DetalhesPedido
    {

        public DetalhesPedido()
        {
            ListaPedidos = new HashSet<Pedido>();
            ListaPratos = new HashSet<Prato>();
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do prato
        /// </summary>
        public String NomePrato { get; set; }

        /// <summary>
        /// Quantidade do prato
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Preço do prato.
        /// </summary>
        public Double Preco { get; set; }



        //*************************

        /// <summary>
        /// Lista de de pedido 
        /// </summary>
        public ICollection<Pedido> ListaPedidos { get; }

        /// <summary>
        /// Lista de pratos 
        /// </summary>
        public ICollection<Prato> ListaPratos { get; set; }

    }
}
