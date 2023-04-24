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

        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Quantidade do item do menu
        /// </summary>
        public int Quantidade { get; set; }

    }
}
