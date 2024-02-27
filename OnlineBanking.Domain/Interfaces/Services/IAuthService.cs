using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис отвечающий за работу с авторизацией и аутентификацией c помощью JWT-токена
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Result<ClaimsIdentity>> Register(RegisterUserViewModel viewModel);

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Result<ClaimsIdentity>> Login(LoginUserViewModel viewModel);
    }
}
