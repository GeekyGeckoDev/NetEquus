namespace Infrastructure
{
    public class NetEquusDbContextFactory : IDesignTimeDbContextFactory<NetEquusDbContext>
    {
        public NetEquusDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NetEquusDbContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-5QSRR7QO;Database=NetEquusDbTest;Trusted_Connection=True;");
            return new NetEquusDbContext(optionsBuilder.Options);
        }
    }
}