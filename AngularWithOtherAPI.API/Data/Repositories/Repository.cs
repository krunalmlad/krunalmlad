using Microsoft.EntityFrameworkCore;
using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private StudentAppDbContext db;
        private DbSet<T> entities;
        public Repository(StudentAppDbContext _db)
        {
            db = _db;
            entities = db.Set<T>();
        }
        public T Add(T entity)
        {
            entities.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = db.Set<T>().Find(id);
            if(entity != null)
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("entity");
            }
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public T Update(int id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            db.SaveChanges();
            return entity;

        }
    }
}
