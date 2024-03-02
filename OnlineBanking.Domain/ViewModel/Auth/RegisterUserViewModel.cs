using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Auth
{
    /// <summary>
    /// Модель представления для регистрации пользователя
    /// </summary>
    public record RegisterUserViewModel
    {
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }  
        
        public string PasswordConfirm { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}
