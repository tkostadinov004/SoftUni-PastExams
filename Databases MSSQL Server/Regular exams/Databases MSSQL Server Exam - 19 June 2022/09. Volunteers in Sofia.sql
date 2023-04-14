SELECT Name, PhoneNumber, LTRIM(SUBSTRING(LTRIM(Address), 8, LEN(LTRIM(Address))))
FROM Volunteers
WHERE DepartmentId = (SELECT Id FROM VolunteersDepartments WHERE DepartmentName = 'Education program assistant') AND CHARINDEX('Sofia', Address, 1) > 0
ORDER BY Name ASC