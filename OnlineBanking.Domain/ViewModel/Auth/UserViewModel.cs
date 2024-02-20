using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.ValueObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Auth
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsOnlineBankingUser { get; set; }

        public Role Role { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
