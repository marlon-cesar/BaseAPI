using BaseAPI.Domain.Constants;
using BaseAPI.Domain.DTO;
using BaseAPI.Domain.Interfaces.Repositories;
using BaseAPI.Domain.Interfaces.Services;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;
using BaseAPI.Domain.Services.Common;
using Newtonsoft.Json;
using System.Net;

namespace BaseAPI.Domain.Services
{
    public class PokemonService : CrudServiceBase<Pokemon>, IPokemonService
    {
        private readonly IPokemonRepository _repository;
        private readonly IPokemonTypeRepository _pokemonTypeRepository;
        private const string POKEAPI_URL = "https://pokeapi.co/api/v2/";
        public PokemonService(IPokemonRepository repository,
            IPokemonTypeRepository pokemonTypeRepository) : base(repository)
        {
            _repository = repository;
            _pokemonTypeRepository = pokemonTypeRepository;
        }

        public async Task LoadPokemons()
        {
            var pokemonsTypes = await GetPokemonTypes();

            foreach (var item in pokemonsTypes.Results)
            {
                var pokemonType = await _pokemonTypeRepository.GetByName(item.Name) ?? new PokemonType();

                pokemonType.Name = item.Name;
                await _pokemonTypeRepository.SaveAsync(pokemonType);
            }

            var pokemons = await GetPokemons();

            foreach (var item in pokemons.Results)
            {
                var pokemon = await _repository.GetByName(item.Name);

                if(pokemon == null)
                {
                    var details = await GetPokemonByName(item.Name);
                    pokemon = new Pokemon
                    {
                        Order = details.Id,
                        Name = details.Name,
                        Height = details.Height,
                        Weight = details.Weight,
                        ImageUrl = details.Sprites?.Other?.OfficialArt?.FrontDefault ?? string.Empty,
                        Types = new List<PokemonType>()
                    };

                    foreach (var types in details.Types)
                    {
                        var type = await _pokemonTypeRepository.GetByName(types.Type.Name);

                        if (type != null)
                            pokemon.Types.Add(type);
                    }

                    await _repository.SaveAsync(pokemon);
                }
            }
        }

        private async Task<APIListResult> GetPokemonTypes()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var requestUri = POKEAPI_URL + "type";

                var httpResponseMessage = await httpClient.GetAsync(requestUri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<APIListResult>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else
                    throw new Exception("API: Get error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }
        private async Task<APIListResult> GetPokemons()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var requestUri = POKEAPI_URL + "pokemon?limit=151&offset=0";

                var httpResponseMessage = await httpClient.GetAsync(requestUri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<APIListResult>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else
                    throw new Exception("API: Get error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }
        private async Task<PokemonDetails> GetPokemonByName(string name)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var requestUri = POKEAPI_URL + $"pokemon/{name}";

                var httpResponseMessage = await httpClient.GetAsync(requestUri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<PokemonDetails>(result);
                else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return default;
                else
                    throw new Exception("API: Get error. URL: " + requestUri + ". Status code: " + httpResponseMessage.StatusCode + ". Message: " + result);
            }
        }

        public async Task<PagedResult<PokemonDTO>> GetByName(int page, string expression = null) =>
            await this._repository.Page(
                this.Query(p => string.IsNullOrEmpty(expression) || p.Name.ToLower().Contains(expression.ToLower()) ||
                p.Order.ToString().Contains(expression.ToLower()))
            .OrderBy(p => p.Order)
            .Select(p => new PokemonDTO(p)), page == 0 ? 1 : page, GlobalConstants.DefaultPageSize);
    }
}
