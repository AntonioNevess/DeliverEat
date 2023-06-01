using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    /// <summary>
    /// Descrição do Prato
    /// </summary>
    public class Prato
    {
        /*
         * Modelo de BD no seguinte link
         * https://app.quickdatabasediagrams.com/#/d/yQ7Nly
         */

        public Prato() { 
            ListaDetalhePedidos = new HashSet<DetalhesPedido>();
            ListaRestaurantes = new HashSet<Restaurante>();
        
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Nome do Prato
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do menu
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Preço do menu
        /// </summary>
        public int Preco { get; set; }  

        
        //**********************************************************

        /// <summary>
        /// Lista de Pedidos de um restaurante
        /// </summary>
        public ICollection<DetalhesPedido> ListaDetalhePedidos { get; set; }

        public ICollection<Restaurante> ListaRestaurantes { get; set; }



        /*
        /// <summary>
        /// FK para a yabela detalhe_pedido 
        /// </summary>
        [ForeignKey(nameof(DetalhePedidos))]
        public int DetalhePedidoFK { get; set; }
        public DetalhesPedido DetalhePedidos { get; set; }

        /// <summary>
        /// FK para o restaurante
        /// </summary>
        [ForeignKey(nameof(Restaurantes))]
        public int RestauranteFK { get; set;}
        public Restaurante Restaurantes { get; set; }*/
       

    }
}
