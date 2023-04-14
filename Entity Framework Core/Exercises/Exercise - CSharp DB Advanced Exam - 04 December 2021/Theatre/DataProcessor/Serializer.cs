namespace Theatre.DataProcessor
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theaters = context.Theatres
                .Where(i => i.NumberOfHalls >= numbersOfHalls && i.Tickets.Count >= 20)
                .ToList()
                .Select(i => new
                {
                    Name = i.Name,
                    Halls = i.NumberOfHalls,
                    TotalIncome = i.Tickets.Where(t => t.RowNumber >= 1 && t.RowNumber <= 5)
                        .Sum(t => t.Price),
                    Tickets = i.Tickets.Where(t => t.RowNumber >= 1 && t.RowNumber <= 5)
                        .Select(t => new
                        {
                            Price = t.Price,
                            RowNumber = t.RowNumber
                        })
                        .OrderByDescending(t => t.Price)
                })
                .ToArray()
                .OrderByDescending(i => i.Halls).ThenBy(i => i.Name);

            return JsonConvert.SerializeObject(theaters, Formatting.Indented);
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            var plays = context.Plays
                .Where(i => i.Rating <= rating)
                .ToList()
                .Select(i => new ExportPlaysDTO
                {
                    Title = i.Title,
                    Duration = i.Duration.ToString("c"),
                    Rating = (i.Rating == 0 ? "Premier" : i.Rating.ToString()),
                    Genre = i.Genre.ToString(),
                    Actors = i.Casts.Where(c => c.IsMainCharacter).Select(c => new ExportActorDTO()
                    {
                        FullName = c.FullName,
                        MainCharacter = $"Plays main character in '{c.Play.Title}'."
                    }).OrderByDescending(i => i.FullName).ToArray()
                })
                .OrderBy(i => i.Title)
                .ThenByDescending(i => i.Genre)
                .ToArray();

            StringWriter origWriter = new StringWriter();

            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            XmlSerializer serializer = new XmlSerializer(typeof(ExportPlaysDTO[]), new XmlRootAttribute("Plays"));
            
            serializer.Serialize(origWriter, plays, xns);

            return origWriter.ToString();
        }
    }
}
