-- Create the database
CREATE DATABASE SimpleBankDB;
GO

-- Use the new database
USE [SimpleBankDB];
GO

-- Create the User table
CREATE TABLE [dbo].[User] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(),
    [LastUpdatedOn] DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Create the BankAccount table
CREATE TABLE [dbo].[BankAccount] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [dbo].[User]([Id]),
    [Balance] DECIMAL(18, 2) NOT NULL DEFAULT 0.00,
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(),
    [LastUpdatedOn] DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Create a new login at the server level
USE [master];
GO

CREATE LOGIN [SimpleBankDBLogin] WITH PASSWORD = 'Aa12345678!';
GO

-- Create a new user in the specific database
USE [SimpleBankDB];
GO

CREATE USER [SimpleBankDBUser] FOR LOGIN [SimpleBankDBLogin];
GO

-- Grant all necessary permissions to the user
ALTER ROLE db_owner ADD MEMBER [SimpleBankDBUser];
GO