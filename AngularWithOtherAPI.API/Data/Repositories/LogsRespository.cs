using System;
using System.Collections.Generic;
using System.Linq;
using AngularWithOtherAPI.API.Data.Models;
using System.Threading.Tasks;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;

namespace AngularWithOtherAPI.API.Data.Repositories
{
    public class LogsRespository : Repository<Log>, ILogsRespository
    {
        private StudentAppDbContext db;
        public LogsRespository(StudentAppDbContext _db) : base(_db)
        {
            db = _db;
        }
    }
}
