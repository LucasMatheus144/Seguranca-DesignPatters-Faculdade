using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspacoPotencial.Models.Account
{
    public class AcessUsers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public TimeSpan AccessDuration { get; set; }
    }
}