CREATE PROCEDURE usp_AnimalsWithOwnersOrNot(@animalName VARCHAR(30))
AS
	SELECT Animals.Name, CASE WHEN Animals.OwnerId IS NULL THEN 'For adoption' ELSE Owners.Name END AS OwnersName
	FROM Animals
	FULL OUTER JOIN Owners
	ON Owners.Id = Animals.OwnerId
	WHERE Animals.Name = @animalName