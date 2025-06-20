using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025TPI.Data
{
    public class Dsw2025TpiContextFactory : IDesignTimeDbContextFactory<Dsw2025TpiContext>
    {
        public Dsw2025TpiContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Dsw2025TpiContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=Dsw2025TpiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultiSubnetFailover=False");

            return new Dsw2025TpiContext(optionsBuilder.Options);
        }
    }
}
