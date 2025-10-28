using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.EquineEstates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public class NetEquusDbContext : DbContext
    {
        public NetEquusDbContext(DbContextOptions<NetEquusDbContext> options)
            : base(options)
        {
        }

        #region [DbSet User]

        public virtual DbSet<User> Users { get; set; }

        #region [DbSet EquineEstate]

        public virtual DbSet<EquineEstate> EquineEstates { get; set; }


        public virtual DbSet<EquineEstatesOwner> EquineEstatesOwners { get; set; }
        #endregion
    }
}