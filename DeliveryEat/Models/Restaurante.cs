using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    /// <summary>
    /// descrição do restaurante 
    /// </summary>
    public class Restaurante
    {
        public Restaurante(){
            ListaPratos = new HashSet<Prato>();
        }
        /*
         * Modelo de BD no seguinte link
         * https://app.quickdatabasediagrams.com/#/d/yQ7Nly
         */

        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do restaurante
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Nome do estabelecimento")]
        [StringLength(50)]
        public string Nome { get; set; }

        /// <summary>
        /// Contacto do estabilecimento
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Telemóvel")]
        [StringLength(9, MinimumLength = 9,
                    ErrorMessage = "O {0} deve ter {1} dígitos")]
        [RegularExpression("9[1236][0-9]{7}",
                    ErrorMessage = "O número de {0} deve começar por 91, 92, 93 ou 96, e ter 9 dígitos")]
        //            ((+|00)[0-9]{2,5})?[0-9]{5,9
        public string Telefone { get; set; }


        /// <summary>
        /// Descrição do restaurante
        /// </summary>
        /// 
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength(250)]
        public string Descricao { get; set; }

        /// <summary>
        /// email do estabelecimento
        /// </summary>
        /// 
        [EmailAddress(ErrorMessage = "O {0} não está corretamente escrito")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("[a-z._0-9]+@gmail.com", ErrorMessage = "O {0} tem de ser do GMail")]
        [StringLength(40)]
        public string Email { get; set; }   

        /// <summary>
        ///Nome do ficheiro com a fotografia do Restaurante
        /// </summary>
        public string NomeFotografia { get; set; }


        /// <summary>
        /// Dados referentes à morada
        /// </summary>
        //Nome da rua
        [StringLength(100)]
        [DisplayName("Rua")]
        public string Rua { get; set; }
        //Código Postal
        [DisplayName("Código Postal")]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-ZÇÁÉÍÓÚÊÂÎÔÛÀÃÕ ]+",
                        ErrorMessage = "O {0} tem de ser da forma XXXX-XXX NOME DA TERRA")]
        [StringLength(25)]
        public string CP { get; set; }
        //Nome da Localidade
        [StringLength(30)]
        [DisplayName("Localidade")]
        public string Localidade { get; set; }

        //******************************************************

        /// <summary>
        /// Lista de items do Prato associados a um restaurante
        /// </summary>
        public ICollection<Prato> ListaPratos { get; set; }



    }
}
