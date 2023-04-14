SELECT Animals.Name, DATEPART(YEAR, Animals.Birthdate) AS BirthYear, AnimalTypes.AnimalType
FROM Animals
INNER JOIN AnimalTypes
ON AnimalTypes.Id = Animals.AnimalTypeId
WHERE OwnerId IS NULL AND (DATEPART(YEAR, '01/01/2022') - DATEPART(YEAR, Animals.Birthdate) < 5) AND AnimalTypes.AnimalType != 'Birds'
ORDER BY Animals.Name