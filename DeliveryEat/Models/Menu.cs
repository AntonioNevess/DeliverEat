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
        /// Preço do m enu
        /// </summary>
        public int Preco { get; set; }  

        
       

    }
}
