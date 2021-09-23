using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Models
{
    public class StudentAppDbContext: DbContext
    {
        public StudentAppDbContext(DbContextOptions<StudentAppDbContext> options): base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
