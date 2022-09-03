using BaseAPI.Domain.Models;
using BaseAPI.Repository.Config;
using BaseAPI.Repository.Mapping.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseAPI.Repository.Mapping
{
    internal class UsuarioMap : DbEntityConfiguration<Usuario>, IEntityMap
    {
        public override void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Nome).IsRequired();
            entity.Property(x => x.Email).IsRequired();
            entity.Property(x => x.Senha).IsRequired();
            entity.Property(x => x.TipoUsuario).IsRequired();
            entity.Property(x => x.Ativo).IsRequired();
        }
    }
}
