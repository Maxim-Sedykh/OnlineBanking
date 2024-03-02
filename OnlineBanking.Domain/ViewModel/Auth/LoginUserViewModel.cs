using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Auth
{
    /// <summary>
    /// Модель представления для логина пользователя
    /// </summary>
    public record LoginUserViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Введите вашу почту")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(5, ErrorMessage = "Длина пароля должна быть больше пяти символов")]
        [MaxLength(20, ErrorMessage = "Длина пароля должна быть меньше двадцати символов")]
        public string Password { get; set; }
    }
}
