SELECT TOP (1000) [Id]
      ,[DTSNumber]
      ,[ApplicationName]
      ,[UserType]
      ,[ApplicantName]
      ,[FiledDate]
      ,[Evaluators]
      ,[DaysToEvaluate]
      ,[Status]
  FROM [application].[dbo].[Applications]


INSERT INTO [application].[dbo].[Applications]
(
    [DTSNumber], 
    [ApplicationName], 
    [UserType], 
    [ApplicantName], 
    [FiledDate], 
    [Evaluators], 
    [DaysToEvaluate], 
    [Status]
)
VALUES
(
    'DTS20240921',             
    'Research Grant Application', 
    'Student',               
    'Dela Cruz. J',             
    '2024-09-21',               
    1,							
    3,                          
    'Pending'  
);

DELETE FROM [application].[dbo].[Applications]
WHERE DTSNumber = 'DTS20240923';
