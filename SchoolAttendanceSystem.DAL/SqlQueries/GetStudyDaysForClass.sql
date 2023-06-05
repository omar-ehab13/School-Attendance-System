SELECT *
FROM StudyingDays AS d
WHERE d.ClassName = '5A'

SELECT p.*
FROM StudyPeriods AS p
INNER JOIN StudyingDays AS d
ON p.DayCode = d.DayCode
WHERE d.ClassName = '5A'

SELECT sub.SubjcetName
FROM Subjects AS sub
WHERE sub.ClassName = '5A'
AND sub.SubjectCode = 'AR-5A'