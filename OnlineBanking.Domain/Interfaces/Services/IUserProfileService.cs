using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.User;

namespace OnlineBanking.Domain.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<Result<UserProfileViewModel>> GetUserProfile(string userName);

        Task<Result.Result> EditUserInfo(UserProfileViewModel viewModel, byte[] imageData);
    }
}
