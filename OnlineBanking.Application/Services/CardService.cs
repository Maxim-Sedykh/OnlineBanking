using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Resources.Success;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Helpers;
using OnlineBanking.Domain.Interfaces.Database;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Card;
using OnlineBanking.Domain.ViewModel.User;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Services
{
    /// <inheritdoc/>
    public class CardService : ICardService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Card> _cardRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IAccountValidator _accountValidator;
        private readonly ICardValidator _cardValidator;

        public CardService(IBaseRepository<Card> cardRepository, IBaseRepository<Account> accountRepository,
            IAccountValidator accountValidator, ICardValidator cardValidator, ILogger logger, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
            _accountValidator = accountValidator;
            _cardValidator = cardValidator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<Result> CreateCardForAccount(long accountId)
        {
            var account = await _accountRepository.GetAll()
                    .Where(x => x.Id == accountId)
                    .FirstOrDefaultAsync();

            var nullValidationResult = _accountValidator.ValidateEntityOnNull(account);
            if (!nullValidationResult.IsSuccess) return nullValidationResult;

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Card currentCard = new()
                    {
                        CardNumber = BankCardDataGenerator.GenerateCardNumber(),
                        Validity = DateTime.UtcNow.AddYears(7),
                        CVV = BankCardDataGenerator.GenerateCVV(),
                        AccountId = accountId,
                        CreatedAt = DateTime.UtcNow,
                    };

                    var cardWithSameNumber = await _cardRepository.GetAll().FirstOrDefaultAsync(x => x.CardNumber == currentCard.CardNumber);

                    var cardNumberValidationResult = _cardValidator.ValidateCardNumber(cardWithSameNumber);
                    if (!cardNumberValidationResult.IsSuccess) return cardNumberValidationResult;

                    await _cardRepository.CreateAsync(currentCard);

                    account.IsCardLinked = true;

                    await _accountRepository.UpdateAsync(account);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    transaction.Rollback();
                }
            }

            return new Result()
            {
                SuccessMessage = SuccessMessage.LinkCardMessage,
            };
        }

        /// <inheritdoc/>
        public async Task<Result<CardViewModel>> GetCardByAccountId(long accountId)
        {
            var card = await _cardRepository.GetAll()
                .Where(x => x.AccountId == accountId)
                .FirstOrDefaultAsync();

            var nullValidationResult = _cardValidator.ValidateEntityOnNull(card);
            if (!nullValidationResult.IsSuccess) 
            {
                return new Result<CardViewModel>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
                };
            }

            return new Result<CardViewModel>()
            {
                Data = card.Adapt<CardViewModel>()
            };
        }
    }
}
