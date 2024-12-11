--Create roles
USE [YourAnimeListDB]
GO

INSERT INTO [dbo].[AspNetRoles]
       ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES
       (NEWID(), -- Automatically generates a GUID
        'Admin', 
        'ADMIN', 
        NEWID()) -- Optionally add a random ConcurrencyStamp
		,(NEWID(), -- Automatically generates a GUID
        'User', 
        'USER', 
        NEWID()) -- Optionally add a random ConcurrencyStamp
GO


--Assign the Role to a User
USE [YourAnimeListDB]
GO

INSERT INTO [dbo].[AspNetUserRoles]
          ([UserId], [RoleId])
VALUES
          ('<user-id>', '<role-id>');
GO
