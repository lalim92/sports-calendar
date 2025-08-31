using SportsCalendar.Objects;
using System.Text;

namespace SportsCalendar.DataAccessLayer
{
    ///
    /// API de gestion des calendriers
    /// 
    public class CalendarApi
    {
        private StringBuilder _content;

        public CalendarApi()
        {
            _content = new StringBuilder();

            // Valeurs initiales
            _content.AppendLine("BEGIN:VCALENDAR");
            _content.AppendLine("VERSION:2.0");
            _content.AppendLine("PRODID:SportsCalendar");
            _content.AppendLine("BEGIN:VTIMEZONE");
            _content.AppendLine("TZID:Europe/Paris");
            _content.AppendLine("X-LIC-LOCATION:Europe/Paris");
            _content.AppendLine("END:VTIMEZONE");
        }

        ///
        /// Ajouter un évènement (match)
        /// 
        public void AddEvent(Match match)
        {
            // Entête
            _content.AppendLine("BEGIN:VEVENT");

            // Dates
            string startDate = FormatDateTime(match.Date.ToLocalTime());
            _content.AppendLine($"DTSTART:{startDate}");
            _content.AppendLine($"DTSTAMP:{startDate}");
            string endDate = FormatDateTime(match.Date.ToLocalTime().AddMinutes(105));
            _content.AppendLine($"DTEND:{endDate}");

            _content.AppendLine("SEQUENCE:0");
            
            // Infos sur l'évènement
            string summary = $"[{match.Competition.Code}] {match.HomeTeam.FullName} vs {match.AwayTeam.FullName}";
            _content.AppendLine($"SUMMARY:{summary}");

            // ID unique (utile pour la mise à jour plus tard)
            string uid = match.Id.ToString();
            _content.AppendLine($"UID:Ical{uid}");

            // Notification(s)
            _content.AppendLine("BEGIN:VALARM");
            _content.AppendLine("ACTION:DISPLAY");
            _content.AppendLine("TRIGGER:-P0DT1H0M0S");
            _content.AppendLine("DESCRIPTION:This is an event reminder");
            _content.AppendLine("END:VALARM");
            _content.AppendLine("BEGIN:VALARM");
            _content.AppendLine("ACTION:DISPLAY");
            _content.AppendLine("TRIGGER:-P1D");
            _content.AppendLine("DESCRIPTION:This is an event reminder");
            _content.AppendLine("END:VALARM");

            // Fin
            _content.AppendLine("END:VEVENT");
        }

        ///
        /// Récupérer le contenu du calendrier
        /// 
        public string GetContent()
        {
            _content.AppendLine("END:VCALENDAR");

            return _content.ToString();
        }

        ///
        /// Utile pour formater les DateTime dans le format prévu dans les fichiers ICS
        /// 
        private string FormatDateTime(DateTime dateTime)
        {
            return $"{dateTime.ToString("yyyyMMdd")}T{dateTime.ToString("HHmmss")}";
        }
    }
}