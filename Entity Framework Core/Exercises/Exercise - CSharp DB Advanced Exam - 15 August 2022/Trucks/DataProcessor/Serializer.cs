namespace Trucks.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.DataProcessor.ExportDto;
    using Trucks.DataProcessor.ExportDto.SubDTOs;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            var despatchers = context.Despatchers.Where(i => i.Trucks.Count() >= 1).ToList()
                .Select(i => new ExportDespatchersDTO
                {
                    DespatcherName = i.Name,
                    Trucks = i.Trucks.ToList().Select(t => new ExportDespatchersTrucksDTO
                    {
                        Make = t.MakeType.ToString(),
                        RegistrationNumber = t.RegistrationNumber
                    }).OrderBy(t => t.RegistrationNumber).ToArray(),
                    TrucksCount = i.Trucks.ToArray().Length
                }).OrderByDescending(i => i.TrucksCount).ThenBy(i => i.DespatcherName).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportDespatchersDTO[]), new XmlRootAttribute("Despatchers"));
            StringWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            ns.Add("", "");
            serializer.Serialize(writer, despatchers, ns);
            return writer.ToString();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var trucks = context.Clients.ToList()
                .Select(i => new ExportClientsDTO
                {
                    Name = i.Name,
                    Trucks = i.ClientsTrucks.Select(t => t.Truck).Where(t => t.TankCapacity >= capacity).ToArray()
                        .Select(t => new ExportClientsTrucksDTO
                        {
                            CargoCapacity = t.CargoCapacity,
                            CategoryType = t.CategoryType.ToString(),
                            MakeType = t.MakeType.ToString(),
                            TankCapacity = t.TankCapacity,
                            TruckRegistrationNumber = t.RegistrationNumber,
                            VinNumber = t.VinNumber
                        }).ToList().OrderBy(t => t.MakeType).ThenByDescending(t => t.CargoCapacity).ToArray()
                }).Where(i => i.Trucks.Length >= 1).OrderByDescending(i => i.Trucks.Length).ThenBy(i => i.Name).Take(10).ToArray();

            return JsonConvert.SerializeObject(trucks, Formatting.Indented);
        }
    }
}
