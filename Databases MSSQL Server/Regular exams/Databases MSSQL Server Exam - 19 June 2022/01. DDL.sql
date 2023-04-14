CREATE TABLE Owners
(
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Address VARCHAR(50)
)
  
CREATE TABLE AnimalTypes
(
	Id INT PRIMARY KEY IDENTITY,
	AnimalType VARCHAR(30) NOT NULL
)

CREATE TABLE Cages
(
	Id INT PRIMARY KEY IDENTITY,
	AnimalTypeId INT NOT NULL

	CONSTRAINT fk_cages_animalTypes
		FOREIGN KEY(AnimalTypeId)
		REFERENCES AnimalTypes(Id)
)
  
CREATE TABLE Animals
(
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(30) NOT NULL,
	Birthdate DATE NOT NULL,
	OwnerId INT,
	AnimalTypeId INT NOT NULL

	CONSTRAINT fk_animals_animalTypes
		FOREIGN KEY(AnimalTypeId)
		REFERENCES AnimalTypes(Id),
	CONSTRAINT fk_animals_owners
		FOREIGN KEY(OwnerId)
		REFERENCES Owners(Id)
)
  
CREATE TABLE AnimalsCages
(
	CageId INT NOT NULL,
	AnimalId INT NOT NULL

	CONSTRAINT fk_animalsCages_cages
		FOREIGN KEY(CageId)
		REFERENCES Cages(Id),
	CONSTRAINT fk_animalsCages_animals
		FOREIGN KEY(AnimalId)
		REFERENCES Animals(Id),
	CONSTRAINT complex_pk
		PRIMARY KEY(CageId, AnimalId)
)
  
CREATE TABLE VolunteersDepartments
(
	Id INT PRIMARY KEY IDENTITY,
	DepartmentName VARCHAR(30) NOT NULL
)

CREATE TABLE Volunteers
(
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Address VARCHAR(50),
	AnimalId INT,
	DepartmentId INT NOT NULL

	CONSTRAINT fk_volunteers_animals
		FOREIGN KEY(AnimalId)
		REFERENCES Animals(Id),
	CONSTRAINT fk_volunteers_volunteersDepartments
		FOREIGN KEY(DepartmentId)
		REFERENCES VolunteersDepartments(Id)
)