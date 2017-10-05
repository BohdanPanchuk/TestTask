CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL IDENTITY ,
	[Name] NVARCHAR(50) NOT NULL,
	[Price] INT NULL,
	PRIMARY KEY ([Id])
)
GO

INSERT INTO [dbo].[Products] ([Name], [Price]) 
	   VALUES 				 ('product1', NULL);
GO
INSERT INTO [dbo].[Products] ([Name], [Price]) 
	   VALUES 				 ('product2', NULL);
GO
INSERT INTO [dbo].[Products] ([Name], [Price]) 
	   VALUES 				 ('product3', NULL);
GO