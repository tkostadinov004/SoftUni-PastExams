namespace Footballers.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCoachesDTO[]), new XmlRootAttribute("Coaches"));
            var coachesDTOs = (ImportCoachesDTO[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var coaches = new List<Coach>();

            foreach (var dto in coachesDTOs)
            {
                if (IsValid(dto))
                {
                    Coach coach = new Coach()
                    {
                        Name = dto.Name,
                        Nationality = dto.Nationality
                    };

                    foreach (var footballerDTO in dto.Footballers)
                    {
                        if (IsValid(footballerDTO))
                        {
                            BestSkillType bestSkillType;
                            PositionType positionType;
                            DateTime startDate;
                            DateTime endDate;

                            bool isValidSkill = Enum.TryParse<BestSkillType>(footballerDTO.BestSkillType, out bestSkillType);
                            bool isValidPosition = Enum.TryParse<PositionType>(footballerDTO.PositionType, out positionType);

                            bool isValidStartDate = DateTime.TryParseExact(footballerDTO.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                            bool isValidEndDate = DateTime.TryParseExact(footballerDTO.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                            if (isValidSkill && isValidPosition && isValidEndDate && isValidStartDate && startDate < endDate)
                            {
                                coach.Footballers.Add(new Footballer()
                                {
                                    Name = footballerDTO.Name,
                                    ContractStartDate = startDate,
                                    ContractEndDate = endDate,
                                    BestSkillType = bestSkillType,
                                    PositionType = positionType
                                });
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

                    coaches.Add(coach);
                    sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Coaches.AddRange(coaches);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            var teamsDTOS = JsonConvert.DeserializeObject<ImportTeamsDTO[]>(jsonString);

            var teams = new List<Team>();
            StringBuilder sb = new StringBuilder();

            foreach (var dto in teamsDTOS)
            {
                if (IsValid(dto))
                {
                    int trophies;

                    bool isValidTrophies = int.TryParse(dto.Trophies, out trophies);
                    if (isValidTrophies && trophies > 0)
                    {
                        Team team = new Team()
                        {
                            Name = dto.Name,
                            Nationality = dto.Nationality,
                            Trophies = trophies
                        };
                        var footballers = context.Footballers.ToArray().Select(i => i.Id);

                        foreach (int id in dto.Footballers.Distinct())
                        {
                            if (footballers.Contains(id))
                            {
                                team.TeamsFootballers.Add(new TeamFootballer()
                                {
                                    FootballerId = id,
                                    Team = team
                                });
                            }
                            else
                            {
                                sb.AppendLine(ErrorMessage);
                            }
                        }

                        teams.Add(team);
                        sb.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
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

            context.Teams.AddRange(teams);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
