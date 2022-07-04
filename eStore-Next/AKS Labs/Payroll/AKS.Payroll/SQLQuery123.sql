select EmployeeId, Status ,Count(Status) from V1_Attendances where OnDate>'2022/05/30'  AND
OnDate<'2022/07/1'  Group by EmployeeId, Status 
order by EmployeeId