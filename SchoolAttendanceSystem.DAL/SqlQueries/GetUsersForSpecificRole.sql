SELECT usr.Id,
	usr.FullName,
	usr.Email,
	usr.PhoneNumber,
	usr.ImageUrl,
	r.Name
FROM [SchoolAttendance].[security].[Users] AS usr
INNER JOIN [SchoolAttendance].[security].[UserRoles] AS usrole
ON usr.Id = usrole.UserId
INNER JOIN [SchoolAttendance].[security].[Roles] AS r
ON usrole.RoleId = r.Id
WHERE r.Name = 'Admin'