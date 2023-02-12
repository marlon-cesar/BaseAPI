using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseAPI.Domain.DTO
{
    public class PokemonDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }
        public decimal Height { get; set; }
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }
        public int Order { get; set; }
        public decimal Weight { get; set; }
        public Sprites Sprites { get; set; }
        public List<PokemonTypeInfo> Types { get; set; }
    }


    public class Sprites
    {
        [JsonProperty("back_default")]
        public string BackDefault { get; set; }
        [JsonProperty("back_female")]
        public string BackFemale { get; set; }
        [JsonProperty("back_shiny")]
        public string BackShiny { get; set; }
        [JsonProperty("back_shiny_female")]
        public string BackShinyFemale { get; set; }
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
        [JsonProperty("front_female")]
        public string FrontFemale { get; set; }
        [JsonProperty("front_shiny")]
        public string FrontShiny { get; set; }
        [JsonProperty("front_shiny_female")]
        public string FrontShinyFemale { get; set; }
        public OtherSprites Other{ get; set; }
    }
    public class OtherSprites
    {
        [JsonProperty("official-artwork")]
        public OfficialArt OfficialArt { get; set; }
    }
    public class OfficialArt
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }
    public class PokemonTypeInfo
    {
        public int Slot { get; set; }
        public Result Type { get; set; }
    }
}
