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
            ListaMenu = new HashSet<Menu>();    
            ListaUtilizador = new HashSet<Utilizador>();
        } 
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quantidade do item do menu
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Lista de um Utilizador
        /// </summary>
        public ICollection<Utilizador> ListaUtilizador { get; }

        /// <summary>
        /// M-N
        /// Lista de Items do menu no carrinho de um utilizador especifico
        /// </summary>
        public ICollection<Menu> ListaMenu { get; set; }
    }
}
