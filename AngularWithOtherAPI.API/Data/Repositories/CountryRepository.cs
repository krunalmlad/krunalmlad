using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using AngularWithOtherAPI.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private StudentAppDbContext db;
        public CountryRepository(StudentAppDbContext _db) : base(_db)
        {
            db = _db;
        }

        public List<DropdownListViewModel> GetCountriesList()
        {
            return db.Countries.Where(w => w.IsActive == true).Select(S => new DropdownListViewModel
            {
                Id = S.CountryId,
                Name = S.Name
            }).OrderBy(o => o.Name).ToList();
        }
    }
}
