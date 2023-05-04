using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    /// <summary>
    /// Descrição do menu
    /// </summary>
    public class Menu
    {
        /*
         * Modelo de BD no seguinte link
         * https://app.quickdatabasediagrams.com/#/d/yQ7Nly
         */

        public Menu() { 
            ListaPedido = new HashSet<Pedido>();
            ListaMenu  =new HashSet<Menu>();    
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Nome do Menu
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
        public ICollection<Pedido> ListaPedido { get; set; }

        /// <summary>
        /// Lista de Menus de um restaurante
        /// </summary>
        public ICollection<Menu> ListaMenu { get; set; }


        /// <summary>
        /// FK para o pedido 
        /// </summary>
        [ForeignKey(nameof(Pedido))]
        public int PedidoFK { get; set; }
        public Pedido Pedido { get; set; }

        /// <summary>
        /// FK para o restaurante
        /// </summary>
        [ForeignKey(nameof(Restaurante))]
        public int RestauranteFK { get; set;}
        public Restaurante Restaurante { get; set; }
       

    }
}
