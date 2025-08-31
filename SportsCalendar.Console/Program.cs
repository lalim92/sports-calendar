using SportsCalendar.Objects;
using SportsCalendar.DataAccessLayer;
using System;
using System.Text;

namespace SportsCalendar.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // ID des équipes à récupérer
            List<int> Teams = new List<int>(){ 57, 576 };

            // Initialiser l'API et le client HTTP
            var footballApi = new FootballApi();

            // Liste complète des matchs
            List<Match> allMatches = new List<Match>();

            // Récupérer les matchs de chaque équipe
            foreach (var currentTeam in Teams)
            {
                var apiResult = await footballApi.GetMatchesAsync(currentTeam);

                // Si pas de résultat => on continue
                if (apiResult == null || apiResult.Matches == null || apiResult.Matches.Count() == 0)
                {
                    System.Console.WriteLine($"No result for Team: {currentTeam}.");
                    continue;
                }

                // Sinon, on ajoute les résultats au tableau final
                allMatches.AddRange(apiResult.Matches);
            }

            // Appeler l'API des calendriers
            var calendarApi = new CalendarApi();
            allMatches.ForEach(m => calendarApi.AddEvent(m));

            // Récupérer le résultat
            string icsContent = calendarApi.GetContent();
            byte[] icsByte = Encoding.UTF8.GetBytes(icsContent);

            // Initialiser le service de mail
            var mailApi = new MailApi();
            // Et envoyer le mail !
            var mailResult = await mailApi.PostMessageAsync(icsByte);

            System.Console.WriteLine($"{mailResult.Id} - {mailResult.Message}");

            // Et écrire tout ça dans un fichier pour l'utiliser
            // System.IO.File.WriteAllText($"{Guid.NewGuid()}.ics", icsContent);

            // // Sinon, on itère et affiche les informations
            // foreach (Match currentMatch in allMatches)
            // {
            //     System.Console.WriteLine($"[{currentMatch.Date}] - [{currentMatch.Competition.Code}] - {currentMatch.HomeTeam.FullName} ({currentMatch.HomeTeam.Code}) vs {currentMatch.AwayTeam.FullName} ({currentMatch.AwayTeam.Code})");
            // }
        }
    }
}