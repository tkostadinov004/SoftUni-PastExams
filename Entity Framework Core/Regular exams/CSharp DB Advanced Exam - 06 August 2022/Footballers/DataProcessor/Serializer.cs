namespace Footballers.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            var coaches = context.Coaches.Where(i => i.Footballers.Count >= 1)
                .ToArray()
                .Select(i => new ExportCoachDTO()
                {
                    CoachName = i.Name,
                    FootballersCount = i.Footballers.Count,
                    Footballers = i.Footballers.Select(f => new ExportFootballerDTO()
                    {
                        Name = f.Name,
                        Position = f.PositionType.ToString()
                    }).OrderBy(f => f.Name).ToArray()
                })
                .OrderByDescending(i => i.FootballersCount)
                .ThenBy(i => i.CoachName)
                .ToArray();

            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCoachDTO[]), new XmlRootAttribute("Coaches"));
            serializer.Serialize(writer, coaches, ns);

            return writer.ToString();
        }
          
        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Where(i => i.TeamsFootballers.Where(tf => tf.Footballer.ContractStartDate >= date).Count() > 1)
                .ToList()
                .Select(i => new
                {
                    Name = i.Name,
                    Footballers = i.TeamsFootballers.Where(tf => tf.Footballer.ContractStartDate >= date)
                                .ToArray()
                                .OrderByDescending(f => f.Footballer.ContractEndDate)
                                .ThenBy(f => f.Footballer.Name)
                                .Select(f => new
                                {
                                    FootballerName = f.Footballer.Name,
                                    ContractStartDate = f.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                                    ContractEndDate = f.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                                    BestSkillType = f.Footballer.BestSkillType.ToString(),
                                    PositionType = f.Footballer.PositionType.ToString()
                                })  
                                .ToArray()
                })
                .OrderByDescending(i => i.Footballers.Count())
                .ThenBy(i => i.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }
}
