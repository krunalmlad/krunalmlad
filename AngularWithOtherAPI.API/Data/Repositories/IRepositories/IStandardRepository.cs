using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Repositories.IRepositories
{
    public interface IStandardRepository : IRepository<Standard>
    {
        List<DropdownListViewModel> GetStandardList();
    }
}
