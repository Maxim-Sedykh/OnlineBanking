using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.RefreshTokenExpireTime).IsRequired();

            builder.HasData(new List<UserToken>()
            {
                new UserToken()
                {
                    Id = 1,
                    RefreshToken = "gjtirp[g$tipgjipep",
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(5),
                    UserId = 1,
                },
                new UserToken()
                {
                    Id = 2,
                    RefreshToken = "bbjitrbjrpotipbjtr",
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(5),
                    UserId = 2,
                },
            });
        }
    }
}
