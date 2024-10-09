/*INSERT INTO 
	table_name (column1, column2, column3, ...)
VALUES
	(values1, values2, values3, ..);*/

SELECT * 
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'Applications' AND TABLE_SCHEMA = 'dbo';


BEGIN TRANSACTION;
INSERT INTO dbo.Applications
(DTSNumber, [ApplicationName],[UserType], [ApplicantName], [FiledDate], Evaluators, DaysToEvaluate, Status)
VALUES
('DT2023030537', 'Publication Incentive', 'Faculty', 'Dela Cruz, J.', '11/08/2024', 3, 3, 'Pending');


INSERT INTO [dbo].[Applications]
(DTSNumber, ApplicationName, UserType, ApplicantName, FiledDate, Evaluators, DaysToEvaluate, Status)
VALUES
('DT2023030539', 'Thesis and Dissertation Grant', 'Faculty', 'Gozales, B.', '2024-05-17', 1, 3, 'Pending');

DELETE FROM [dbo].[Applications]
WHERE DTSNumber = 'DT2023030538';

CREATE TRIGGER trg_UpdateApplicationStatus
ON [dbo].[Applications]
AFTER UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE Status IN ('Approved', 'Rejected'))
    BEGIN
        INSERT INTO ApplicationStatusLog (ApplicationId, OldStatus, NewStatus, ChangeDate)
        SELECT i.Id, d.Status, i.Status, GETDATE()
        FROM inserted i
        INNER JOIN deleted d ON i.Id = d.Id;
    END
END;



CREATE TABLE [dbo].[Applications] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DTSNumber NVARCHAR(50),
    ApplicationName NVARCHAR(100),
    UserType NVARCHAR(50),
    ApplicantName NVARCHAR(100),
    FiledDate DATE,
    Evaluators INT,
    DaysToEvaluate INT,
    Status NVARCHAR(50)
);






INSERT INTO [dbo].[Applications]
(
    [DTSNumber], 
    [ApplicationName], 
    [UserType], 
    [ApplicantName], 
    [FiledDate],  -- Added this column
    [Evaluators], 
    [DaysToEvaluate], 
    [Status]
)
VALUES
(
    'DTS20240926',             
    'Research Grant Application', 
    'Faculty',               
    'Cruz. J',             
    '2024-09-21',               -- Ensure this is the correct date format
    '1',                         -- This should be a string if you defined Evaluators as nvarchar
    3,                          
    'Pending'
),
(
    'DTS20240927',             
    'Research Grant Application', 
    'Student',               
    'Gonzales. B',             
    '2024-09-23',               
    '1',                         
    3,                          
    'Pending' 
),
(
    'DTS20240928',             
    'Publication Incentives', 
    'Student',               
    'Dela Cruz. J',             
    '2024-08-21',               
    '1',                         
    3,                          
    'Pending'
);


UPDATE [application].[dbo].[Applications]
SET [Status] = 'Approved'
WHERE [DTSNumber] IN ('DTS20240923', 'DTS20240926');







DELETE FROM [application].[dbo].[Applications]
WHERE DTSNumber = 'DTS20240928';

SELECT 
    DTSNumber,
    ApplicationName AS Details,
    Evaluators,
    DaysToEvaluate,
    Status
FROM 
    Applications
WHERE 
    Status = 'Active';



INSERT INTO [dbo].[Checklists] (Id, DocumentName, ApplicationType, ChecklistItems, Actions)
VALUES (101, 'Research Document', 'Research Support Application', 'Title, Abstract, Funding', 'Create');

ALTER TABLE [dbo].[Checklists]
ALTER COLUMN Actions VARCHAR(50) NULL;

INSERT INTO [dbo].[Checklists] (DocumentName, ApplicationType, ChecklistItems)
VALUES ('Research Document', 'Research Support Application', 'Title, Abstract, Funding');

DELETE FROM [dbo].[Checklists]
WHERE DTSNumber = 'DTS20240928';

SELECT COLUMN_NAME 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Applications';

DROP TABLE Applications;

INSERT INTO Evaluators (Name, Email, Specialization, Type, Status)
VALUES ('Juan Dela Cruz', 'user@pup.edu.ph', 'Associate', 'Internal', 'Active');
