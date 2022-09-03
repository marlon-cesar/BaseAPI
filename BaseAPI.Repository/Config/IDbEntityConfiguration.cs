using Microsoft.EntityFrameworkCore;

namespace  BaseAPI.Repository.Config
{
    public interface IDbEntityConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
