using BaseAPI.Domain.Models;
using BaseAPI.Repository.Config;
using BaseAPI.Repository.Mapping.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseAPI.Repository.Mapping
{
    internal class PokemonTypeMap : DbEntityConfiguration<PokemonType>, IEntityMap
    {
        public override void Configure(EntityTypeBuilder<PokemonType> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
        }
    }
}
