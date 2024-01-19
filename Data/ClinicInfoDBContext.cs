using ClinicManagment.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagment.Data
{
    public class ClinicInfoDBContext : DbContext
    {
        public ClinicInfoDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Signup> Signups { get; set; }
    }

       



}