using System.ComponentModel.DataAnnotations;
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
        
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do Prato
        /// </summary>
        ///
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Nome do Prato")]
        [StringLength(50)]
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do menu
        /// </summary>
        /// 
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength(250)]
        public string Descricao { get; set; }

        /// <summary>
        ///Nome do ficheiro com a fotografia do Prato
        /// </summary>
        public string NomeFotografia { get; set; }

        /// <summary>
        /// Preço do menu
        /// </summary>
        public decimal Preco { get; set; }

        [NotMapped] // esta anotação impede a EF de exportar este atributo para a BD
        [RegularExpression("[0-9]+(.|,)?[0-9]{0,2}", ErrorMessage = "Só pode escrever algarismos e, " + "se desejar, duas casas decimais no {0}")]
        [Display(Name = "Preço")]
        public string PrecoPratoAux { get; set; }

        //**********************************************************
        [ForeignKey(nameof(Restaurante))]
        public int RestauranteFK { get; set; }
        public Restaurante Restaurante { get; set; }


        /// <summary>
        /// Lista de Pedidos de um restaurante
        /// </summary>
        public ICollection<DetalhesPedido> ListaDetalhePedidos { get; set; }

        //public ICollection<Restaurante> ListaRestaurantes { get; set; }


   
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
