namespace DeliveryEat.Models
{
    public class ViewModel
    {

        public class PessoaViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }


        }

        public class RestauranteViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string CP { get; set; }

        }

        public class PratoViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }   
            public string Restaurante { get; set; } 
        }
        public class LoginViewModel
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }


        public class ErrorViewModel
        {
            public string RequestId { get; set; }

            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        }
    }
}
