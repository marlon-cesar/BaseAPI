using BaseAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAPI.Domain.DTO
{
    public class PokemonDTO
    {
        public PokemonDTO() { }

        public PokemonDTO(Pokemon entity)
        {
            Id = entity.Id;
            Order = entity.Order;
            Name = entity.Name;
            Height = entity.Height;
            Weight = entity.Weight;
            ImageUrl = entity.ImageUrl;
            Types = entity.Types.Select(t => new PokemonTypeDTO(t)).ToList();
        }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string ImageUrl { get; set; }
        public IList<PokemonTypeDTO> Types { get; set; }
    }

    public class PokemonTypeDTO
    {
        public PokemonTypeDTO() { }
        public PokemonTypeDTO(PokemonType entity)
        {
            Name = entity.Name;
        }
        public string Name { get; set; }
    }
}
