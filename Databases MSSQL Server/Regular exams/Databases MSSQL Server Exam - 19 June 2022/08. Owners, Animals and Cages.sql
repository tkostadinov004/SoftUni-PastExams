SELECT CONCAT(Owners.Name, '-', Animals.Name) AS OwnersAnimals, Owners.PhoneNumber, AnimalsCages.CageId
FROM Owners
INNER JOIN Animals
ON Animals.OwnerId = Owners.Id
INNER JOIN AnimalsCages
ON AnimalsCages.AnimalId = Animals.Id
WHERE Animals.AnimalTypeId = (SELECT Id FROM AnimalTypes WHERE AnimalType = 'Mammals')
ORDER BY Owners.Name ASC, Animals.Name DESC