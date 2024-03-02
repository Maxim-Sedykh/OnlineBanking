using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Введите ваш логин")]
        [MinLength(2, ErrorMessage = "Длина логина счёта должна быть больше двух символов")]
        [MaxLength(50, ErrorMessage = "Длина логина счёта должна быть меньше 50 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите ваше имя")]
        [MinLength(2, ErrorMessage = "Длина имени должна быть больше двух символов")]
        [MaxLength(50, ErrorMessage = "Длина имени должна быть меньше 50 символов")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Введите ваше фамилию")]
        [MinLength(2, ErrorMessage = "Длина фамилии должна быть больше двух символов")]
        [MaxLength(50, ErrorMessage = "Длина фамилии счёта должна быть меньше 50 символов")]
        public string Surname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Введите вашу почту")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(5, ErrorMessage = "Длина пароля должна быть больше пяти символов")]
        [MaxLength(20, ErrorMessage = "Длина пароля должна быть меньше двадцати символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [MinLength(5, ErrorMessage = "Длина пароля должна быть больше пяти символов")]
        [MaxLength(20, ErrorMessage = "Длина пароля должна быть меньше двадцати символов")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Введите ваш адрес - вашу улицу")]
        [MaxLength(50, ErrorMessage = "Длина улицы должна быть меньше пятидесяти символов")]
        [MinLength(4, ErrorMessage = "Длина улицы должна быть меньше четырёх символов")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Введите ваш адрес - ваш город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите ваш индекс")]
        [MaxLength(10, ErrorMessage = "Длина индекса должна быть меньше десяти символов")]
        public string ZipCode { get; set; }
    }
}
