
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells
                .Where(i => i.ShellWeight > shellWeight)
                .Select(i => new
                {
                    ShellWeight = i.ShellWeight,
                    Caliber = i.Caliber,
                    Guns = i.Guns.Where(g => g.GunType == GunType.AntiAircraftGun).Select(g => new
                    {
                        GunType = "AntiAircraftGun",
                        g.GunWeight,
                        g.BarrelLength,
                        Range = g.Range > 3000 ? "Long-range" : "Regular range"
                    }).OrderByDescending(g => g.GunWeight).ToArray()
                }).ToArray().OrderBy(i => i.ShellWeight);

            return JsonConvert.SerializeObject(shells, Formatting.Indented);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            XmlRootAttribute root = new XmlRootAttribute("Guns");
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var guns = context.Guns.Include(i => i.Manufacturer)
                .Include(i => i.CountriesGuns)
                .ThenInclude(cg => cg.Country)
                .Where(i => i.Manufacturer.ManufacturerName == manufacturer)
                .Select(i => new ExportGunsDTO
                {
                    Manufacturer = i.Manufacturer.ManufacturerName,
                    GunType = i.GunType,
                    GunWeight = i.GunWeight,
                    BarrelLength = i.BarrelLength,
                    Range = i.Range,
                    Countries = i.CountriesGuns.Where(c => c.Country.ArmySize > 4500000).Select(c => new ExportCountriesDTO()
                    {
                        Country = c.Country.CountryName,
                        ArmySize = c.Country.ArmySize
                    }).OrderBy(c => c.ArmySize).ToArray()
                }).ToList().OrderBy(i => i.BarrelLength).ToArray();

            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(ExportGunsDTO[]), root);

            serializer.Serialize(writer, guns, ns);
            return writer.ToString();
        }
    }
}
