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
        public string Nome { get; set; }

        /// <summary>
        /// Contacto do estabilecimento
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Descrição do restaurante
        /// </summary>
        public string Descricao { get; set; }


        /// <summary>
        /// Dados referentes à morada
        /// </summary>
        //Nome da rua
        public string Rua { get; set; }
        //Código Postal
        public string CP { get; set; }
        //Nome da Localidade
        public string Localidade { get; set; }

        //******************************************************

        /// <summary>
        /// Lista de items do Prato associados a um restaurante
        /// </summary>
        public ICollection<Prato> ListaPratos { get; set; }



    }
}
