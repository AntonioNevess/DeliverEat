using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(50)]
        public string Nome { get; set; }

        /// <summary>
        /// email do utilizador
        /// </summary>
        [EmailAddress(ErrorMessage = "O {0} não está corretamente escrito")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("[a-z._0-9]+@gmail.com", ErrorMessage = "O {0} tem de ser do GMail")]
        [StringLength(40)]
        public string Email { get; set; }

        /// <summary>
        /// Password do utilizador
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Numero de telefone do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Telemóvel")]
        [StringLength(9, MinimumLength = 9,
              ErrorMessage = "O {0} deve ter {1} dígitos")]
        [RegularExpression("9[1236][0-9]{7}",
              ErrorMessage = "O número de {0} deve começar por 91, 92, 93 ou 96, e ter 9 dígitos")]
        public string Telefone { get; set; }


        /// <summary>
        /// Dados referentes à morada
        /// </summary>
        //Nome da rua
        public string Rua { get; set; }
        //Código Postal
        [DisplayName("Código Postal")]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-ZÇÁÉÍÓÚÊÂÎÔÛÀÃÕ ]+",
                   ErrorMessage = "O {0} tem de ser da forma XXXX-XXX NOME DA TERRA")]
        [StringLength(25)]
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
