using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class DbConnectionClass : DbContext
    {
        public DbConnectionClass(DbContextOptions<DbConnectionClass> options) : base(options)
        {

        }

        public DbSet<EmpIdentityModel> EmpIdentityData { get; set; }
    }
}
