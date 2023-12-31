USE [master]
GO
/****** Object:  Database [Libros]    Script Date: 06/10/2023 13:01:34 ******/
CREATE DATABASE [Libros]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Libros', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Libros.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Libros_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Libros_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Libros] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Libros].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Libros] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Libros] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Libros] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Libros] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Libros] SET ARITHABORT OFF 
GO
ALTER DATABASE [Libros] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Libros] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Libros] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Libros] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Libros] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Libros] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Libros] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Libros] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Libros] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Libros] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Libros] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Libros] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Libros] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Libros] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Libros] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Libros] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Libros] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Libros] SET RECOVERY FULL 
GO
ALTER DATABASE [Libros] SET  MULTI_USER 
GO
ALTER DATABASE [Libros] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Libros] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Libros] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Libros] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Libros] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Libros] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Libros', N'ON'
GO
ALTER DATABASE [Libros] SET QUERY_STORE = ON
GO
ALTER DATABASE [Libros] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Libros]
GO
/****** Object:  Table [dbo].[Autor]    Script Date: 06/10/2023 13:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autor](
	[IdAutor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Nacionalidad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Autor] PRIMARY KEY CLUSTERED 
(
	[IdAutor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 06/10/2023 13:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante](
	[IdLector] [int] IDENTITY(1,1) NOT NULL,
	[CI] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Carrera] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
 CONSTRAINT [PK_Estudiante] PRIMARY KEY CLUSTERED 
(
	[IdLector] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibAut]    Script Date: 06/10/2023 13:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibAut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAutor] [int] NOT NULL,
	[IdLibro] [int] NOT NULL,
 CONSTRAINT [PK_LibAut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 06/10/2023 13:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libro](
	[IdLibro] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[Editorial] [varchar](50) NOT NULL,
	[Area] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Libro] PRIMARY KEY CLUSTERED 
(
	[IdLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prestamo]    Script Date: 06/10/2023 13:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prestamo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLector] [int] NOT NULL,
	[IdLibro] [int] NOT NULL,
	[FechaPrestamo] [date] NOT NULL,
	[FechaDevolucion] [date] NOT NULL,
	[Devuelto] [bit] NOT NULL,
 CONSTRAINT [PK_Prestamo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LibAut]  WITH CHECK ADD  CONSTRAINT [FK_LibAut_Autor] FOREIGN KEY([IdAutor])
REFERENCES [dbo].[Autor] ([IdAutor])
GO
ALTER TABLE [dbo].[LibAut] CHECK CONSTRAINT [FK_LibAut_Autor]
GO
ALTER TABLE [dbo].[LibAut]  WITH CHECK ADD  CONSTRAINT [FK_LibAut_Libro] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libro] ([IdLibro])
GO
ALTER TABLE [dbo].[LibAut] CHECK CONSTRAINT [FK_LibAut_Libro]
GO
ALTER TABLE [dbo].[Prestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Estudiante] FOREIGN KEY([IdLector])
REFERENCES [dbo].[Estudiante] ([IdLector])
GO
ALTER TABLE [dbo].[Prestamo] CHECK CONSTRAINT [FK_Prestamo_Estudiante]
GO
ALTER TABLE [dbo].[Prestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Libro] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libro] ([IdLibro])
GO
ALTER TABLE [dbo].[Prestamo] CHECK CONSTRAINT [FK_Prestamo_Libro]
GO
USE [master]
GO
ALTER DATABASE [Libros] SET  READ_WRITE 
GO
