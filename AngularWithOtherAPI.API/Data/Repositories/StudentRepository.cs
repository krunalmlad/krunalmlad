using Microsoft.EntityFrameworkCore;
using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using AngularWithOtherAPI.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private StudentAppDbContext db;
        public StudentRepository(StudentAppDbContext _db): base(_db)
        {
            db = _db;
        }

        public List<StudentGridViewModel> GetStudentGridList()
        {
            var query = db.Students.Include(x => x.Standard).Include(x => x.Country)
                .Where(w=> w.Standard != null && w.Country != null);

            var result = query.Select(s => new StudentGridViewModel
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Gender = s.Gender,
                Address = s.Address,
                State = s.State,
                City = s.City,
                StandardId = s.StandardId,
                StandardName = s.Standard.Name,
                CountryId = s.CountryId,
                CountryName = s.Country.Name,
            }).OrderBy(o => o.FirstName).ToList();
            return result;
        }

        public Student UpdateStudentModel(Student model,int studentId)
        {
            var objStudent = db.Students.FirstOrDefault(f => f.StudentId == studentId);
            if(objStudent != null)
            {
                objStudent.FirstName = model.FirstName;
                objStudent.LastName = model.LastName;
                objStudent.Gender = model.Gender;
                objStudent.Address = model.Address;
                objStudent.State = model.State;
                objStudent.City = model.City;
                objStudent.StandardId = model.StandardId;
                objStudent.CountryId = model.CountryId;
                db.SaveChanges();
            }
            return objStudent;
        }

    }
}
