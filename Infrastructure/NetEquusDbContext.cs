using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.Users;
using Domain.Entities.Models.EquineEstates;

namespace Infrastructure
{
    public class NetEquusDbContext : DbContext
    {
        public NetEquusDbContext(DbContextOptions<NetEquusDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<EquineEstate> EquineEstates { get; set; }
        public virtual DbSet<EstateOwner> EstateOwners { get; set; }


    }
}
