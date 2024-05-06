using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspacoPotencial.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace EspacoPotencial.Models.Account
{
    public class LoggingService : ILoggingService
    {
        private readonly ApaDbContext _dbContext;
         private readonly UserManager<IdentityUser> _userManager;
         private int lastLoginRecordId;

        public LoggingService(ApaDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        async Task ILoggingService.LogLoginAsync(string userId, DateTime loginTime)
        {
            // Implemente a lógica para registrar o login no banco de dados
            // Exemplo:
            var loginRecord = new AcessUsers
            {
                UserId = userId,
                LoginTime = loginTime
            };

            _dbContext.AcessUsers.Add(loginRecord);
            await _dbContext.SaveChangesAsync();
            lastLoginRecordId = loginRecord.Id;
        }
       async Task ILoggingService.LogLogoutAsync(string userId, DateTime logoutTime)
        {
            var lastLoginRecord = await _dbContext.AcessUsers.FirstOrDefaultAsync(u => u.UserId == userId);

            if (lastLoginRecord != null)
            {
                lastLoginRecord.LogoutTime = logoutTime;
                lastLoginRecord.AccessDuration = logoutTime - lastLoginRecord.LoginTime;
                await _dbContext.SaveChangesAsync();
            }
        }

       

       
    }
}
