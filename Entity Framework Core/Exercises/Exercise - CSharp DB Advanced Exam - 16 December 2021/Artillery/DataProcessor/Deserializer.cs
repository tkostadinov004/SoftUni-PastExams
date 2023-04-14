namespace Artillery.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage =
                "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCountriesDTO[]), new XmlRootAttribute("Countries"));

            var countriesDTOS = (ImportCountriesDTO[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            List<Country> countries = new List<Country>();
            foreach (var dto in countriesDTOS)
            {
                if (IsValid(dto))
                {
                    Country country = new Country()
                    {
                        CountryName = dto.CountryName,
                        ArmySize = dto.ArmySize
                    };

                    countries.Add(country);
                    sb.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Countries.AddRange(countries);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportManufacturersDTO[]), new XmlRootAttribute("Manufacturers"));

            var manufacturersDTOS = (ImportManufacturersDTO[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (var dto in manufacturersDTOS)
            {
                if (IsValid(dto) && manufacturers.FirstOrDefault(i => i.ManufacturerName == dto.ManufacturerName) == null)
                {
                    Manufacturer manufacturer = new Manufacturer()
                    {
                        ManufacturerName = dto.ManufacturerName,
                        Founded = dto.Founded
                    };

                    manufacturers.Add(manufacturer);
                    sb.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, string.Join(", ", manufacturer.Founded.Split(", ", StringSplitOptions.RemoveEmptyEntries).TakeLast(2))));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Manufacturers.AddRange(manufacturers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportShellsDTO[]), new XmlRootAttribute("Shells"));

            var shellsDTOS = (ImportShellsDTO[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            List<Shell> shells = new List<Shell>();
            foreach (var dto in shellsDTOS)
            {
                if (IsValid(dto))
                {
                    Shell shell = new Shell()
                    {
                        ShellWeight = dto.ShellWeight,
                        Caliber = dto.Caliber
                    };

                    shells.Add(shell);
                    sb.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Shells.AddRange(shells);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var gunsDTOS = JsonConvert.DeserializeObject<ImportGunsDTO[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Gun> guns = new List<Gun>();
            foreach (var dto in gunsDTOS)
            {
                GunType gunType;
                bool isValidGun = Enum.TryParse<GunType>(dto.GunType, out gunType);
                if (IsValid(dto) && isValidGun)
                {
                    Gun gun = new Gun()
                    {
                        ManufacturerId = dto.ManufacturerId,
                        GunWeight = dto.GunWeight,
                        BarrelLength = dto.BarrelLength,
                        NumberBuild = dto.NumberBuild,
                        Range = dto.Range,
                        GunType = gunType,
                        ShellId = dto.ShellId
                    };

                    gun.CountriesGuns = dto.Countries.Select(i => new CountryGun()
                    {
                        CountryId = i.Id,
                        Gun = gun
                    }).ToArray();

                    guns.Add(gun);
                    sb.AppendLine(string.Format(SuccessfulImportGun, gun.GunType, gun.GunWeight, gun.BarrelLength));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Guns.AddRange(guns);
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
