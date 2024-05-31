using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Passport : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public string PassportSeries { get; set; }

        public string PassportId { get; set; }

        public string IssuedBy { get; set; }

        public bool IsConfirmed { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public DateTime DateOfIssue { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
