using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Credit;
using OnlineBanking.Domain.ViewModel.CreditType;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Services
{
    public class CreditService : ICreditService
    {
        private readonly IBaseRepository<Credit> _creditRepository;
        private readonly IBaseRepository<CreditType> _creditTypeRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger _logger;

        public CreditService(IBaseRepository<Credit> creditRepository, IBaseRepository<CreditType> creditTypeRepository, ILogger logger, 
            IBaseRepository<User> userRepository)
        {
            _creditRepository = creditRepository;
            _creditTypeRepository = creditTypeRepository;
            _logger = logger;
            _userRepository = userRepository;
        }

        public Task<Result<CreditViewModel>> CreateCredit(CreateCreditViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<CollectionResult<CreditTypeViewModel>> GetCreditTypes()
        {
            try
            {
                var creditTypes = await _creditTypeRepository.GetAll()
                    .Select(x => new CreditTypeViewModel
                    {
                        Id = x.Id,
                        CreditTypeName = x.CreditTypeName,
                        Description = x.Description,
                        MinCreaditTermInMonths = x.MinCreaditTermInMonths,
                        MaxCreaditTermInMonths = x.MaxCreaditTermInMonths,
                        InterestRate = x.InterestRate,
                    })
                    .ToArrayAsync();

                return new CollectionResult<CreditTypeViewModel>()
                {
                    Data = creditTypes,
                    Count = creditTypes.Length,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<CreditTypeViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<CollectionResult<CreditViewModel>> GetUserCredits(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new CollectionResult<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                var userCredits = await _creditRepository.GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new CreditViewModel
                    {
                        Id = x.Id,
                        CreditSumAmount = x.CreditSumAmount,
                        MoneyLenderReceiveAmount = x.MoneyLenderReceiveAmount,
                        CreditTerm = x.CreditTerm,
                        Percent = x.Percent,
                        CreatedAt = x.CreatedAt,
                        CreditRemainerAmount = x.CreditRemainerAmount,
                    })
                    .ToArrayAsync();

                return new CollectionResult<CreditViewModel>()
                {
                    Data = userCredits,
                    Count = userCredits.Length,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<CreditViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<User>> SetUserIncome(SetIncomeViewModel viewModel, string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new Result<User>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                user.Income = viewModel.UserIncome;
                user.IsIncomeVerified = true; //Изначально подразумевается, что пользователь должен подтвердить свой доход различными справками,
                                    //в качестве теста, доход будет сразу утверждён
                await _userRepository.UpdateAsync(user);

                return new Result<User>()
                {
                    Data = user,
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<User>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }
    }
}
