namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Plays");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPlaysDTO[]), root);

            var playsDTOS = (ImportPlaysDTO[])serializer.Deserialize(new StringReader(xmlString));

            List<Play> plays = new List<Play>();
            StringBuilder sb = new StringBuilder();

            foreach (ImportPlaysDTO dto in playsDTOS)
            {
                if (IsValid(dto))
                {
                    Genre genre;
                    bool isGenreValid = Enum.TryParse<Genre>(dto.Genre, out genre);
                    if (isGenreValid)
                    {
                        Play play = new Play()
                        {
                            Title = dto.Title,
                            Duration = TimeSpan.ParseExact(dto.Duration, "c", CultureInfo.InvariantCulture),
                            Rating = dto.Rating,
                            Description = dto.Description,
                            Screenwriter = dto.Screenwriter,
                            Genre = genre
                        };

                        plays.Add(play);
                        sb.AppendLine(string.Format(SuccessfulImportPlay, play.Title, dto.Genre, play.Rating));
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                    }
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Casts");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCastsDTO[]), root);

            var castsDTOS = (ImportCastsDTO[])serializer.Deserialize(new StringReader(xmlString));

            List<Cast> casts = new List<Cast>();
            StringBuilder sb = new StringBuilder();

            foreach (ImportCastsDTO dto in castsDTOS)
            {
                if (IsValid(dto))
                {
                    Cast cast = new Cast()
                    {
                        FullName = dto.FullName,
                        IsMainCharacter = dto.IsMainCharacter,
                        PhoneNumber = dto.PhoneNumber,
                        PlayId = dto.PlayId
                    };
                    casts.Add(cast);
                    sb.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter ? "main" : "lesser"));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var theatersDTOS = JsonConvert.DeserializeObject<ImportTheatersTicketsDTO[]>(jsonString);

            List<Theatre> theatres = new List<Theatre>();
            StringBuilder sb = new StringBuilder();

            foreach (var dto in theatersDTOS)
            {
                if (IsValid(dto))
                {
                    Theatre theatre = new Theatre()
                    {
                        Name = dto.Name,
                        NumberOfHalls = dto.NumberOfHalls,
                        Director = dto.Director
                    };

                    foreach (TicketDTO ticket in dto.Tickets)
                    {
                        if (IsValid(ticket))
                        {
                            theatre.Tickets.Add(new Ticket()
                            {
                                Price = ticket.Price,
                                RowNumber = ticket.RowNumber,
                                PlayId = ticket.PlayId
                            });
                        }
                        else
                        {
                            sb.AppendLine(ErrorMessage);
                        }
                    }

                    theatres.Add(theatre);
                    sb.AppendLine(string.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Theatres.AddRange(theatres);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
