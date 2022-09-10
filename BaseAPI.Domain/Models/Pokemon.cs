using BaseAPI.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAPI.Domain.Models
{
    public class Pokemon : EntityBase
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string ImageUrl { get; set; }
        public virtual IList<PokemonType> Types { get; set; }
    }
}
