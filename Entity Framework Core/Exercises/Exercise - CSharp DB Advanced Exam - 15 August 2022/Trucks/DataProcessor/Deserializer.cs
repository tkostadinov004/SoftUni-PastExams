namespace Trucks.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;
    using Trucks.DataProcessor.ImportDto.SubDTOs;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportDespatcherDTO[]), new XmlRootAttribute("Despatchers"));
            var despatchersDTOS = (ImportDespatcherDTO[])serializer.Deserialize(new StringReader(xmlString));

            var despatchers = new List<Despatcher>();
            foreach (var despatcher in despatchersDTOS)
            {
                Console.WriteLine(despatcher.Trucks.Count());
                if (IsValid(despatcher))
                {
                    if (string.IsNullOrEmpty(despatcher.Position) == false)
                    {
                        var validTrucks = new List<Truck>();
                        foreach (var truck in despatcher.Trucks)
                        {
                            CategoryType categoryType;
                            MakeType makeType;
                            bool isValidCategory = Enum.TryParse<CategoryType>(truck.CategoryType, out categoryType);
                            bool isValidMakeType = Enum.TryParse<MakeType>(truck.MakeType, out makeType);
                            if (IsValid(truck) && isValidCategory && isValidMakeType)
                            {
                                validTrucks.Add(new Truck
                                {
                                    RegistrationNumber = truck.RegistrationNumber,
                                    VinNumber = truck.VinNumber,
                                    TankCapacity = truck.TankCapacity,
                                    CargoCapacity = truck.CargoCapacity,
                                    CategoryType = categoryType,
                                    MakeType = makeType
                                });
                            }
                            else
                            {
                                sb.AppendLine(ErrorMessage);
                            }
                        }

                        despatchers.Add(new Despatcher
                        {
                            Name = despatcher.Name,
                            Position = despatcher.Position,
                            Trucks = validTrucks
                        });
                        sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, despatcher.Name, validTrucks.Count));
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
            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var clientsDTOs = JsonConvert.DeserializeObject<ImportClientDTO[]>(jsonString);

            var clients = new List<Client>();
            foreach (var dto in clientsDTOs)
            {
                if (IsValid(dto))
                {
                    if (dto.Type != "usual")
                    {
                        var validTrucksIds = new List<int>();
                        var availableTrucks = context.Trucks.Select(i => i.Id).ToList();
                        foreach (var truckDTOId in dto.Trucks.Distinct())
                        {
                            if (availableTrucks.Contains(truckDTOId))
                            {
                                validTrucksIds.Add(truckDTOId);
                            }
                            else
                            {
                                sb.AppendLine(ErrorMessage);
                            }
                        }

                        clients.Add(new Client
                        {
                            Name = dto.Name,
                            Nationality = dto.Nationality,
                            Type = dto.Type,
                            ClientsTrucks = validTrucksIds.Select(i => new ClientTruck
                            {
                                TruckId = i
                            }).ToList()
                        });

                        sb.AppendLine(string.Format(SuccessfullyImportedClient, dto.Name, validTrucksIds.Count));
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
            context.Clients.AddRange(clients);
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
