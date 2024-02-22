using Microsoft.AspNetCore.Http;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.User
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public IFormFile Avatar { get; set; }

        public byte[] Image { get; set; }

        public List<AccountViewModel> UserAccounts { get; set; }
    }
}
