using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryEat.Models
{
    public class DetalhesPedido
    {

        
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do prato
        /// </summary>
        public String NomePrato { get; set; }

        /// <summary>
        /// Quantidade do prato
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Preço do prato.
        /// </summary>
        public decimal Preco { get; set; }

        [NotMapped] // esta anotação impede a EF de exportar este atributo para a BD
        [RegularExpression("[0-9]+(.|,)?[0-9]{0,2}", ErrorMessage = "Só pode escrever algarismos e, " + "se desejar, duas casas decimais no {0}")]
        [Display(Name = "Preço")]
        public string PrecoPedidoAux { get; set; }


        //*************************

        /// <summary>
        /// Fk para a tabela Pedidos
        /// </summary>
        [ForeignKey(nameof(Pedidos))]
        public int PedidosFK { get; set; }
        public Pedido Pedidos { get; set; }

        /// <summary>
        /// Fk para a tabela Pratos
        /// </summary>
        [ForeignKey(nameof(Pratos))]
        public int PratoFK { get; set; }
        public Prato Pratos { get; set; }


    }
}
