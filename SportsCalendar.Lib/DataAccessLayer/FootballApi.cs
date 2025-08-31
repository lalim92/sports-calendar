using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SportsCalendar.Objects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SportsCalendar.DataAccessLayer
{
    ///
    /// Résultat de l'appel à l'API
    /// 
    public class GetMatchesResult
    {
        [property: JsonPropertyName("matches")]
        public required IEnumerable<Match> Matches {get; set;}
    }

    ///
    /// Service pour accéder à l'API de récupération des données en lien avec le Football
    /// 
    public class FootballApi
    {
        ///
        /// Client HTTP
        /// 
        private readonly HttpClient _httpClient;

        public FootballApi()
        {
            _httpClient = new HttpClient();

            // Base de l'URL à appeler
            _httpClient.BaseAddress = new Uri("https://api.football-data.org/v4/");

            // Indiquer qu'on attend du json
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Ajouter la clé API aux headers
            string footballApiToken = Environment.GetEnvironmentVariable("FOOTBALL_API_TOKEN");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", footballApiToken);
        }

        public async Task<GetMatchesResult> GetMatchesAsync(int teamId)
            => await _httpClient.GetFromJsonAsync<GetMatchesResult>($"teams/{teamId}/matches?dateFrom={DateTime.Now.ToString("yyyy-MM-dd")}&dateTo={DateTime.Now.AddYears(1).ToString("yyyy-MM-dd")}");
    }
}