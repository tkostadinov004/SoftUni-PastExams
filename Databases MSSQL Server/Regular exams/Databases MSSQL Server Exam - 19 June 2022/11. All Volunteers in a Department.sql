CREATE FUNCTION udf_GetVolunteersCountFromADepartment (@VolunteersDepartment VARCHAR(30))
RETURNS INT AS
BEGIN
	RETURN
	(
		SELECT COUNT(Volunteers.Id)
		FROM Volunteers
		WHERE Volunteers.DepartmentId = (SELECT Id FROM VolunteersDepartments WHERE DepartmentName = @VolunteersDepartment)
	)
END