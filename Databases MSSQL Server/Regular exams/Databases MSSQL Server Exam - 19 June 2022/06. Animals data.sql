SELECT Animals.Name, AnimalTypes.AnimalType, FORMAT(Animals.Birthdate, 'dd.MM.yyyy') AS BirthDate
FROM Animals
INNER JOIN AnimalTypes
ON AnimalTypes.Id = Animals.AnimalTypeId
ORDER BY Animals.Name ASC