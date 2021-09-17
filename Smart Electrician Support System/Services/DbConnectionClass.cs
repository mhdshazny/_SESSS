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
        public DbSet<EmployeeModel> EmployeeData { get; set; } 
        public DbSet<EmpCategoryModel> EmpCategoryData { get; set; }
        public DbSet<AppointmentModel> AppointmentData { get; set; }
        public DbSet<CustomerModel> CustomerData { get; set; }
        public DbSet<ProductsModel> ProductsData { get; set; }
        public DbSet<UsedProductsModel> UsedProductsData { get; set; }
        public DbSet<JobModel> JobData { get; set; }
        public DbSet<DashTargetModel> DashTargetData { get; set; }
        public DbSet<InvoiceModel> InvoiceData { get; set; }

    }
}
