CREATE TABLE [dbo].[Product]
(
	[ProductID] INT NOT NULL PRIMARY KEY, 
    [OrderId] INT NOT NULL, 
    [ProductName] NVARCHAR(50) NULL, 
    [Properties] NVARCHAR(50) NULL, 
    [Cost] FLOAT NULL, 
    [Count] INT NULL
)
