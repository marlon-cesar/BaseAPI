using BaseAPI.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAPI.Domain.Models
{
    public class PokemonType : EntityBase
    {
        public string Name { get; set; }

        public virtual IList<Pokemon> Pokemons { get; set; }
    }
}
