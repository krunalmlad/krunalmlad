using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using AngularWithOtherAPI.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Repositories
{
    public class StandardRepository:  Repository<Standard>, IStandardRepository
    {
        private StudentAppDbContext db;
        public StandardRepository(StudentAppDbContext _db) : base(_db)
        {
            db = _db;
        }

        public List<DropdownListViewModel> GetStandardList()
        {
            return db.Standards.Where(w => w.IsActive == true).Select(S => new DropdownListViewModel {
                Id = S.StandardId,
                Name = S.Name
            }).OrderBy(o=> o.Name).ToList();
        }
    }
}
