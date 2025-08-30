using System.Text.Json.Serialization;

namespace SportsCalendar.Objects
{
    ///
    /// Description d'une équipe
    /// 
    public sealed class Team
    {
        ///
        /// ID récupéré à partir de l'API
        /// 
        [property: JsonPropertyName("id")]
        public required int Id { get; set; }

        ///
        /// Code récupéré à partir de l'API
        /// 
        [property: JsonPropertyName("tla")]
        public required string Code { get; set; }

        ///
        /// Nom de l'équipe
        /// 
        [property: JsonPropertyName("name")]
        public required string FullName { get; set; }

        ///
        /// Nom court de l'équipe
        [property: JsonPropertyName("shortName")]
        public required string ShortName { get; set; }
    }
}