namespace DeliveryEat.Models
{
    public class ViewModel
    {

        /// <summary>
        /// View model para uma pessoa
        /// </summary>
        public class PessoaViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }

        }

        /// <summary>
        /// View Model de um Restaurante 
        /// </summary>
        public class RestauranteViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string CP { get; set; }

        }

        /// <summary>
        /// View Model de detalhes de um pedido
        /// </summary>
        public class DetalhesPedidoModel
        {
            public int Id { get; set; }
            public string NomePrato { get; set; }  
            
            public int Quantidade { get; set; } 
            public decimal Preco { get; set; }  
            
        }

        /// <summary>
        /// View model de um prato
        /// </summary>

        public class PratoViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }  
            public decimal Preco { get; set; }
            public string Restaurante { get; set; } 
        }

        /// <summary>
        /// View Model do Login
        /// </summary>
        public class LoginViewModel
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        /// <summary>
        /// View model de um Pedido
        /// </summary>
        public class PedidoViewModel
        {
            public int Id { get; set; }
            public bool Confirmed { get; set; }

        } 

        public class ErrorViewModel
        {
            public string RequestId { get; set; }

            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        }
    }
}
