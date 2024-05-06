using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspacoPotencial.Models.Account
{
    public interface ILoggingService
    {
        Task LogLoginAsync(string userId, DateTime loginTime);
        Task LogLogoutAsync(string userId, DateTime logoutTime);
    }
}