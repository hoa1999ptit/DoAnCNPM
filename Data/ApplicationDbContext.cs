using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using doan_cnpm.Models;
namespace doan_cnpm.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Appoinments> Appoinments { get; set; }
        public DbSet<ProductAddToAppointment> ProductAddToAppointment { get; set; }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationCustomerAccount> ApplicationCustomerAccount { get; set; }
        public DbSet<ApplicationAdminUser> ApplicationAdminUser { get; set; }
    }
}
