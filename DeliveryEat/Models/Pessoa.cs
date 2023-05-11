using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{

    /// <summary>
    /// Descrição dos utilizadores 
    /// </summary>
    public class Pessoa
    {
        /*
         * Modelo de BD no seguinte link
         * https://app.quickdatabasediagrams.com/#/d/yQ7Nly
         */

        public Pessoa() {
            ListaPedido = new HashSet<Pedido>();
        } 

        /// <summary>
        /// PK 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador 
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// email do utilizador
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password do utilizador
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Numero de telefone do utilizador
        /// </summary>
        public string Telefone { get; set; }


        /// <summary>
        /// Dados referentes à morada
        /// </summary>
        //Nome da rua
        public string Rua { get; set; }
        //Código Postal
        public string CP { get; set;}
        //Nome da Localidade
        public string Localidade { get; set; }

        //******************

        /// <summary>
        /// FK Lista de Pedidos associados a um utilizador
        /// </summary>
        public ICollection<Pedido> ListaPedido { get; set; }


    }
}
