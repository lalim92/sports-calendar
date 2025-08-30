using SportsCalendar.Objects;
using System.Text.Json.Serialization;

namespace SportsCalendar.Objects
{
    ///
    /// Description d'une équipe
    /// 
    public sealed class Match
    {
        ///
        /// ID récupéré à partir de l'API
        /// 
        [property: JsonPropertyName("id")]
        public required int Id { get; set; }

        ///
        /// Jour de match
        /// 
        [property: JsonPropertyName("utcDate")]
        public required DateTime Date { get; set; }

        ///
        /// Compétition concernée
        /// 
        [property: JsonPropertyName("competition")]
        public required Competition Competition { get; set; }

        ///
        /// Equipe à domicile
        /// 
        [property: JsonPropertyName("homeTeam")]
        public required Team HomeTeam { get; set; }

        ///
        /// Equipe à l'extérieur
        /// 
        [property: JsonPropertyName("awayTeam")]
        public required Team AwayTeam { get; set; }
        
        ///
        /// Statut du match. Valeurs possibles: SCHEDULED, TIMED, IN_PLAY, PAUSED, FINISHED, SUSPENDED, POSTPONED, CANCELLED, AWARDED.
        /// 
        [property: JsonPropertyName("status")]
        public required string Status {get; set;}
    }
}