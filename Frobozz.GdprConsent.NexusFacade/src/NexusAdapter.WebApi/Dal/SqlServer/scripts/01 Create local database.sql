USE Master
GO

CREATE DATABASE LeverExampleGdpr COLLATE Finnish_Swedish_CI_AS
GO

USE LeverExampleGdpr
GO

CREATE LOGIN LeverExampleGdprUser WITH PASSWORD = 'Password123!';
CREATE USER LeverExampleGdprUser FOR LOGIN LeverExampleGdprUser
exec sp_addrolemember 'db_owner', 'LeverExampleGdprUser';
GO