using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;

namespace SportsCalendar.DataAccessLayer
{
    ///
    /// Résultat de l'appel à l'API
    /// 
    public class PostMailResult
    {
        ///
        /// ID du mail envoyé
        /// 
        [property: JsonPropertyName("id")]
        public required string Id {get; set;}

        ///
        /// Message
        /// 
        [property: JsonPropertyName("message")]
        public required string Message {get; set;}
    }
    
    ///
    /// Service pour accéder à l'API d'envoi de mail
    /// 
    public class MailApi
    {
        ///
        /// Client HTTP
        /// 
        private readonly HttpClient _httpClient;

        public MailApi()
        {
            _httpClient = new HttpClient();

            // Base de l'URL à appeler
            _httpClient.BaseAddress = new Uri("https://api.eu.mailgun.net/v3/chuwi.ovh/");

            // Indiquer qu'on attend du json
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            // Authentification (basique)
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:3a8fbd57c622d810b1e7bfb95f3d6ccf-5a4acb93-7bddaa83"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(@"Basic", base64String);
        }

        public async Task<PostMailResult> PostMessageAsync(byte[] attachmentByte)
        {
            var multipart = new MultipartFormDataContent();

            // Ajouter les infos
            multipart.Add(new StringContent("Mailgun Sandbox <postmaster@chuwi.ovh>"), "from");
            multipart.Add(new StringContent("Salim A. <salim.abdous@gmail.com>"), "to");
            multipart.Add(new StringContent("Mise à jour du calendrier des matchs"), "subject");
            multipart.Add(new StringContent("Calendrier à jour en PJ du mail."), "text");
            multipart.Add(new ByteArrayContent(attachmentByte), "attachment", "calendar.ics");

            // Envoyer la requête et récupérer le résultat
            var response = await _httpClient.PostAsync("messages", multipart);
            response.EnsureSuccessStatusCode(); // lèvera une exception si code != 2xx

            // Lire le JSON
            var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<PostMailResult>(stream);

            if (result == null)
                throw new InvalidOperationException("Impossible de désérialiser la réponse de l'API Mailgun.");

            return result;
        }
    }
}