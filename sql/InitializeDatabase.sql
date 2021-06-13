USE master
GO

----------------------------------------------------------------------------
--- Serilog DB CREATION
----------------------------------------------------------------------------

DROP DATABASE IF EXISTS Serilog
GO

CREATE DATABASE Serilog
GO 
USE Serilog
GO 

----------------------------------------------------------------------------
--- DB USER CREATION
----------------------------------------------------------------------------
USE master;
GO
CREATE LOGIN [cr_dbuser] WITH PASSWORD=N'Sql1nContainersR0cks!', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE Serilog;
GO
CREATE USER [cr_dbuser] FOR LOGIN [cr_dbuser];
GO
EXEC sp_addrolemember N'db_owner', [cr_dbuser];
GO