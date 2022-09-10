using BaseAPI.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAPI.Domain.Models
{
    public class PokemonTypeMapping : EntityBase
    {
        public Guid PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }
        public Guid PokemonTypeId { get; set; }
        public virtual PokemonType PokemonType { get; set; }
    }
}
