
IF db_id('TestMatchProfiler') is null 
    begin
        CREATE DATABASE [TestMatchProfiler]
        CONTAINMENT = NONE
        ON  PRIMARY 
        ( NAME = N'TestMatchProfiler', FILENAME = N'/var/opt/mssql/data/TestMatchProfiler.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
        LOG ON 
        ( NAME = N'TestMatchProfiler_log', FILENAME = N'/var/opt/mssql/data/TestMatchProfiler_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
        COLLATE SQL_Latin1_General_CP1_CI_AS
        IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [TestMatchProfiler] MODIFY FILEGROUP [PRIMARY] DEFAULT
    end
GO
ALTER DATABASE [TestMatchProfiler] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [TestMatchProfiler] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET AUTO_CREATE_STATISTICS OFF
GO
ALTER DATABASE [TestMatchProfiler] SET AUTO_UPDATE_STATISTICS OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestMatchProfiler] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestMatchProfiler] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestMatchProfiler] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestMatchProfiler] SET  READ_WRITE 
GO
ALTER DATABASE [TestMatchProfiler] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TestMatchProfiler] SET  MULTI_USER 
GO
ALTER DATABASE [TestMatchProfiler] SET PAGE_VERIFY TORN_PAGE_DETECTION  
GO
ALTER DATABASE [TestMatchProfiler] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestMatchProfiler] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TestMatchProfiler]
GO