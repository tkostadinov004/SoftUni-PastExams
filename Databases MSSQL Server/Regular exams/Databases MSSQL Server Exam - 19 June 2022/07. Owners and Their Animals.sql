SELECT TOP 5 Owners.Name, COUNT(Animals.Id) AS CountOfAnimals
FROM Owners
INNER JOIN Animals
ON Animals.OwnerId = Owners.Id
GROUP BY Owners.Name
ORDER BY CountOfAnimals DESC, Owners.Name ASC