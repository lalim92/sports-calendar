using System.Text.Json.Serialization;

namespace SportsCalendar.Objects
{
    ///
    /// Description d'une compétition
    /// 
    public sealed class Competition
    {
        ///
        /// ID récupéré à partir de l'API
        /// 
        [property: JsonPropertyName("id")]
        public required int Id { get; set; }

        ///
        /// Code récupéré à partir de l'API
        /// 
        [property: JsonPropertyName("code")]
        public required string Code { get; set; }

        ///
        /// Nom de la compétition
        /// 
        [property: JsonPropertyName("name")]
        public required string Name { get; set; }
    }
}