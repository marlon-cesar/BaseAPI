using BaseAPI.Domain.Models;
using BaseAPI.Repository.Config;
using BaseAPI.Repository.Mapping.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseAPI.Repository.Mapping
{
    internal class PokemonMap : DbEntityConfiguration<Pokemon>, IEntityMap
    {
        public override void Configure(EntityTypeBuilder<Pokemon> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Height).IsRequired();
            entity.Property(x => x.Weight).IsRequired();
            entity.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
