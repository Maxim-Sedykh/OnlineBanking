using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Enum
{
    public enum StatusCode
    {
        /// <summary>
        /// Группа статус кодов для пользователя
        /// </summary>
        UserNotFound = 11,
        UserAlreadyExist = 12,
        UserIncomeNotVerified = 13,

        /// <summary>
        /// Группа статус кодов для работы с авторизацией
        /// </summary>
        PasswordIsWrong = 21,
        PasswordNotEqualsPasswordConfirm = 22,

        /// <summary>
        /// Группа статус кодов для счетов
        /// </summary>
        AccountNotFound = 31,
        AccountAlreadyExist = 32,
        AccountBalanceNotEmpty = 33,

        /// <summary>
        /// Группа статус кодов для типов счёта
        /// </summary>
        AccountTypesNotFound = 41,
        AccountTypeNotFound = 42,

        /// <summary>
        /// Группа статус кодов для банковских карт
        /// </summary>
        CardNotFound = 51,

        /// <summary>
        /// Группа статус кодов для платежного метода
        /// </summary>
        PaymentMethodsNotFound = 61,

        /// <summary>
        /// Группа статус кодов для транзакций
        /// </summary>
        NotEnoughFunds = 71,

        /// <summary>
        /// Группа статус кодов для типов кредита
        /// </summary>
        CreditTypeNotFound = 81,
        CreditTypesNotFound = 82,

        /// <summary>
        /// Группа статус кодов для кредита
        /// </summary>
        InvalidCreditTerm = 91,
        CreditNotApproved = 92,
        CreditNotFound = 93,

        /// <summary>
        /// Группа статус кодов для исключительных ситуаций
        /// </summary>
        TryAgain = 451,
        InternalServerError = 500,
    }
}
