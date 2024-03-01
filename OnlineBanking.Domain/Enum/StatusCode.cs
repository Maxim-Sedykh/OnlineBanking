using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound = 11,
        UserAlreadyExist = 12,

        PasswordIsWrong = 21,
        PasswordNotEqualsPasswordConfirm = 22,

        AccountNotFound = 31,
        AccountAlreadyExist = 32,
        AccountBalanceNotEmpty = 33,

        AccountTypesNotFound = 41,
        AccountTypeNotFound = 42,

        CardNotFound = 51,

        PaymentMethodsNotFound = 61,

        NotEnoughFunds = 71,

        UserIncomeNotVerified = 82,

        CreditTypeNotFound = 91,
        CreditTypesNotFound = 92,

        InvalidCreditTerm = 101,
        CreditNotApproved = 102,
        CreditNotFound = 103,

        TryAgain = 451,

        InternalServerError = 500,
    }
}
