using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<UserProfileViewModel>> GetUserProfile(string userName);
    }
}
