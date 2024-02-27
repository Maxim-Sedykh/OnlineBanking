using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Resources.Success;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Helpers;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Card;
using OnlineBanking.Domain.ViewModel.User;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Services
{
    public class CardService : ICardService
    {
        private readonly IBaseRepository<Card> _cardRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly ILogger _logger;

        public CardService(IBaseRepository<Card> cardRepository, IBaseRepository<Account> accountRepository, ILogger logger)
        {
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<Result<Card>> CreateCardForAccount(long accountId)
        {
            try
            {
                var account = await _accountRepository.GetAll()
                    .Where(x => x.Id == accountId)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new Result<Card>()
                    {
                        ErrorCode = (int)StatusCode.AccountNotFound,
                        ErrorMessage = ErrorMessage.AccountNotFound,
                    };
                }

                var cards = await _cardRepository.GetAll().ToListAsync();

                Card currentCard = new Card
                {
                    CardNumber = BankCardDataGenerator.GenerateCardNumber(),
                    Validity = DateTime.UtcNow.AddYears(7),
                    CVV = BankCardDataGenerator.GenerateCVV(),
                    AccountId = accountId,
                    CreatedAt = DateTime.UtcNow,
                };

                if (cards.Any(c => c.CardNumber == currentCard.CardNumber))
                {
                    return new Result<Card>
                    {
                        ErrorCode = (int)StatusCode.TryAgain,
                        ErrorMessage = ErrorMessage.TryAgain,
                    };
                }

                await _cardRepository.CreateAsync(currentCard);

                account.IsCardLinked = true;

                await _accountRepository.UpdateAsync(account);

                return new Result<Card>()
                {
                    SuccessMessage = SuccessMessage.LinkCardMessage,
                    Data = currentCard,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<Card>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<CardViewModel>> GetCardByAccountId(long accountId)
        {
            try
            {
                var card = await _cardRepository.GetAll()
                    .Where(x => x.AccountId == accountId)
                    .FirstOrDefaultAsync();

                if (card == null)
                {
                    return new Result<CardViewModel>()
                    {
                        ErrorCode = (int)StatusCode.CardNotFound,
                        ErrorMessage = ErrorMessage.CardNotFound,
                    };
                }

                return new Result<CardViewModel>()
                {
                    Data = card.Adapt<CardViewModel>()
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<CardViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }
    }
}
