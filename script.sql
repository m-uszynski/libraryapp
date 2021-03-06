USE [master]
GO
/****** Object:  Database [library]    Script Date: 12.01.2020 08:31:55 ******/
CREATE DATABASE [library]
GO

ALTER DATABASE [library] MODIFY FILE
( NAME = N'library', SIZE = 512MB, MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
GO

ALTER DATABASE [library] MODIFY FILE
( NAME = N'library_log', SIZE = 256MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10% )

ALTER DATABASE [library] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [library].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [library] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [library] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [library] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [library] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [library] SET ARITHABORT OFF 
GO
ALTER DATABASE [library] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [library] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [library] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [library] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [library] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [library] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [library] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [library] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [library] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [library] SET  DISABLE_BROKER 
GO
ALTER DATABASE [library] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [library] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [library] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [library] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [library] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [library] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [library] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [library] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [library] SET  MULTI_USER 
GO
ALTER DATABASE [library] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [library] SET DB_CHAINING OFF 
GO
ALTER DATABASE [library] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [library] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [library] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [library] SET QUERY_STORE = OFF
GO
USE [library]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 12.01.2020 08:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Author] [nchar](50) NOT NULL,
	[Title] [nchar](50) NOT NULL,
	[ReleaseDate] [datetime] NULL,
	[ISBN] [nchar](50) NOT NULL,
	[BookGenreId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Borrow]    Script Date: 12.01.2020 08:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Borrow](
	[BorrowId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[IsReturned] [bit] NOT NULL,
 CONSTRAINT [PK_Borrow] PRIMARY KEY CLUSTERED 
(
	[BorrowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictBookGenre]    Script Date: 12.01.2020 08:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictBookGenre](
	[BookGenreId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_DictBookGenre] PRIMARY KEY CLUSTERED 
(
	[BookGenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12.01.2020 08:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NOT NULL,
	[LastName] [nchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[Email] [nchar](50) NOT NULL,
	[Phone] [nchar](20) NULL,
	[AddDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (1, N'Lisa Jewell                                       ', N'Then She Was Gone                                 ', CAST(N'2010-05-22T12:00:00.000' AS DateTime), N'123456789                                         ', 1, 10, CAST(N'2017-12-02T13:43:23.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (2, N'Lisa Wingate                                      ', N'Before We Were Yours                              ', CAST(N'2010-01-10T11:34:12.000' AS DateTime), N'81-8971-892                                       ', 1, 1, CAST(N'2018-11-13T11:11:34.000' AS DateTime), CAST(N'2018-11-14T11:12:34.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (3, N'Delia Owens                                       ', N'Where the Crawdads Sing                           ', CAST(N'2011-02-12T14:12:23.000' AS DateTime), N'289-729-829                                       ', 1, 21, CAST(N'2019-02-25T15:34:02.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (4, N'Kerry Fisher                                      ', N'The Silent Wife                                   ', CAST(N'2015-11-12T13:21:02.000' AS DateTime), N'8942-489-28                                       ', 1, 2, CAST(N'2019-03-21T15:23:12.000' AS DateTime), CAST(N'2020-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (5, N'Jodi Picoult                                      ', N'Small Great Things: A Novel                       ', CAST(N'2006-01-02T17:43:01.000' AS DateTime), N'41-984-189-14                                     ', 1, 70, CAST(N'2018-01-23T08:23:02.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (6, N'Walter Isaacson                                   ', N'Steve Jobs (Hardcover)                            ', CAST(N'2001-02-23T10:10:23.000' AS DateTime), N'128-351-235-8                                     ', 2, 20, CAST(N'2019-03-22T10:11:12.000' AS DateTime), CAST(N'2019-05-23T10:11:12.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (7, N'Dawid McCullough                                  ', N'John Adams (Paperback)                            ', CAST(N'2000-03-24T10:11:24.000' AS DateTime), N'1239-81-2309                                      ', 2, 10, CAST(N'2018-03-24T10:23:21.000' AS DateTime), CAST(N'2018-05-23T10:14:15.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (8, N'John Krakauer                                     ', N'Into the Wild (Paperback)                         ', CAST(N'2002-12-12T16:13:24.000' AS DateTime), N'1293-8190-2809                                    ', 2, 56, CAST(N'2018-03-24T12:23:23.000' AS DateTime), CAST(N'2018-03-24T13:04:04.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (9, N'Michelle Obama                                    ', N'Becoming (Hardcover)                              ', CAST(N'2015-12-24T18:23:21.000' AS DateTime), N'12938-091285-09                                   ', 2, 7, CAST(N'2016-12-23T17:23:14.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (10, N'Amy Poehler                                       ', N'Yes Please (Hardcover)                            ', CAST(N'2013-10-23T23:21:21.000' AS DateTime), N'1290-380-923                                      ', 2, 99, CAST(N'2014-06-21T14:32:12.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (11, N'Fatimah Asghar                                    ', N'Soft Sience                                       ', CAST(N'1987-04-17T17:56:34.000' AS DateTime), N'8971-89717-98                                     ', 3, 11, CAST(N'2014-06-18T15:12:50.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (12, N'Richard Blanco                                    ', N'Blud                                              ', CAST(N'1995-12-21T06:50:25.000' AS DateTime), N'0230-20323-00                                     ', 3, 85, CAST(N'2013-07-13T08:09:22.000' AS DateTime), CAST(N'2017-06-22T09:10:24.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (13, N'Sarah Blake                                       ', N'Mary Wants to be a Superwoman                     ', CAST(N'1992-03-14T09:53:27.000' AS DateTime), N'12939-0322-3908                                   ', 3, 19, CAST(N'2006-05-19T02:41:10.000' AS DateTime), CAST(N'2018-10-06T10:12:14.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (14, N'Andres Cerpa                                      ', N'Heart Like a Window                               ', CAST(N'1993-06-12T04:21:39.000' AS DateTime), N'2323-245-57574                                    ', 3, 121, CAST(N'2015-02-13T11:59:31.000' AS DateTime), CAST(N'2017-08-02T23:14:42.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (15, N'Tess Gallagher                                    ', N'Hunger                                            ', CAST(N'1995-07-01T04:43:06.000' AS DateTime), N'212-335-658-78                                    ', 3, 52, CAST(N'2011-01-10T03:31:42.000' AS DateTime), CAST(N'2018-06-28T15:38:41.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (16, N'Marcel Proust                                     ', N'In Search of Lost Time                            ', CAST(N'2004-03-02T20:01:27.000' AS DateTime), N'92038-2590-9283                                   ', 4, 1, CAST(N'2016-09-29T15:41:44.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (17, N'James Joyce                                       ', N'Ulysses                                           ', CAST(N'1983-07-26T07:05:39.000' AS DateTime), N'2938-9232-093                                     ', 4, 84, CAST(N'2008-03-15T07:11:13.000' AS DateTime), CAST(N'2018-06-06T03:00:46.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (18, N'Miguel de Cervantes                               ', N'Don Quixote                                       ', CAST(N'1996-08-22T04:18:47.000' AS DateTime), N'20390-294-0920                                    ', 4, 21, CAST(N'2018-09-19T10:44:29.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (19, N'F.Scott Fitzgerald                                ', N'The Great Gatsby                                  ', CAST(N'1990-09-23T22:45:33.000' AS DateTime), N'203-902-930                                       ', 4, 23, CAST(N'2014-09-19T05:59:21.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (20, N'Herman Melville                                   ', N'Moby Dick                                         ', CAST(N'1995-03-08T01:33:18.000' AS DateTime), N'203-902-930                                       ', 4, 100, CAST(N'2009-03-13T09:28:48.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (21, N'Theresa Breslin                                   ', N'An Illustrated Treasury of Scottish Folk          ', CAST(N'1989-08-11T11:28:14.000' AS DateTime), N'203-9290-3092                                     ', 5, 25, CAST(N'2008-07-03T22:57:24.000' AS DateTime), CAST(N'2018-10-14T11:15:40.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (22, N'Hans Christian Andersen                           ', N'Hans Christian Andersen''s Fairy Tales             ', CAST(N'1990-06-11T17:12:17.000' AS DateTime), N'203-923-00                                        ', 5, 98, CAST(N'2013-04-06T18:26:24.000' AS DateTime), CAST(N'2019-11-03T14:05:59.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (23, N'Justin Richards                                   ', N'Doctor Who                                        ', CAST(N'1998-10-20T10:19:47.000' AS DateTime), N'0029-3209-523                                     ', 5, 54, CAST(N'2019-06-13T09:58:11.000' AS DateTime), NULL)
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (24, N'E.D. Baker                                        ', N'The Dragon Princess                               ', CAST(N'1983-11-20T11:49:58.000' AS DateTime), N'20390-293-9023                                    ', 5, 76, CAST(N'2015-02-04T01:08:13.000' AS DateTime), CAST(N'2018-11-19T14:49:56.000' AS DateTime))
INSERT [dbo].[Book] ([BookId], [Author], [Title], [ReleaseDate], [ISBN], [BookGenreId], [Count], [AddDate], [ModifiedDate]) VALUES (25, N'Marrisa Meyer                                     ', N'Cinder                                            ', CAST(N'1986-11-18T21:48:38.000' AS DateTime), N'9238-923-89                                       ', 5, 85, CAST(N'2009-10-11T01:05:54.000' AS DateTime), CAST(N'2010-10-11T01:05:54.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Book] OFF
SET IDENTITY_INSERT [dbo].[Borrow] ON 

INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (1, 1, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-03-01T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (2, 1, 9, CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-03-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (3, 2, 1, CAST(N'2018-04-23T00:00:00.000' AS DateTime), CAST(N'2018-07-23T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (4, 4, 22, CAST(N'2018-01-22T00:00:00.000' AS DateTime), CAST(N'2018-04-22T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (5, 7, 2, CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-03-08T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (6, 9, 25, CAST(N'2018-12-24T00:00:00.000' AS DateTime), CAST(N'2019-03-24T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (7, 9, 24, CAST(N'2018-12-24T00:00:00.000' AS DateTime), CAST(N'2019-03-24T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (8, 9, 9, CAST(N'2018-12-24T00:00:00.000' AS DateTime), CAST(N'2019-04-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (9, 10, 17, CAST(N'2019-01-23T00:00:00.000' AS DateTime), CAST(N'2019-04-23T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Borrow] ([BorrowId], [UserId], [BookId], [FromDate], [ToDate], [IsReturned]) VALUES (10, 10, 19, CAST(N'2019-01-24T00:00:00.000' AS DateTime), CAST(N'2019-04-24T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Borrow] OFF
SET IDENTITY_INSERT [dbo].[DictBookGenre] ON 

INSERT [dbo].[DictBookGenre] ([BookGenreId], [Name]) VALUES (1, N'Drama                                             ')
INSERT [dbo].[DictBookGenre] ([BookGenreId], [Name]) VALUES (2, N'Biography                                         ')
INSERT [dbo].[DictBookGenre] ([BookGenreId], [Name]) VALUES (3, N'Poetry                                            ')
INSERT [dbo].[DictBookGenre] ([BookGenreId], [Name]) VALUES (4, N'Novel                                             ')
INSERT [dbo].[DictBookGenre] ([BookGenreId], [Name]) VALUES (5, N'Fairy tale                                        ')
SET IDENTITY_INSERT [dbo].[DictBookGenre] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (1, N'Bruce                                             ', N'Porath                                            ', CAST(N'1998-02-02T00:00:00.000' AS DateTime), N'BPorar@myspy.com                                  ', N'410-369-8312        ', CAST(N'2018-11-19T14:49:56.000' AS DateTime), CAST(N'2020-01-11T18:42:30.250' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (2, N'Joel                                              ', N'Arnold                                            ', CAST(N'1989-08-11T11:28:14.000' AS DateTime), N'JoelArnold@armyspy.com                            ', N'
668 5677          ', CAST(N'2018-10-14T11:15:40.000' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (3, N'Henry                                             ', N'Holmes                                            ', CAST(N'1990-06-11T17:12:17.000' AS DateTime), N'NicolaiLSkov@dayrep.com                           ', N'
0485 79 85 08     ', CAST(N'2018-10-15T12:12:12.000' AS DateTime), CAST(N'2018-11-14T12:12:03.000' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (4, N'Mia                                               ', N'Mathiesen                                         ', CAST(N'1997-10-17T20:15:30.000' AS DateTime), N'
MiaNMathiesen@dayrep.com                        ', N'
0470 89 77 12     ', CAST(N'2017-11-27T11:07:25.000' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (5, N'Frederikke                                        ', N'Danielsen                                         ', CAST(N'1983-06-08T15:00:07.000' AS DateTime), N'
FrederikkeJDanielsen@rhyta.com                  ', N'0490 70 55 19       ', CAST(N'2017-08-16T13:42:47.000' AS DateTime), CAST(N'2018-08-16T13:42:47.000' AS DateTime), 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (6, N'Amadej                                            ', N'Sokołowski                                        ', CAST(N'1975-12-12T13:12:02.000' AS DateTime), N'
AmadejSokolowski@teleworm.us                    ', N'
53 527 50 80      ', CAST(N'2016-11-24T23:21:02.000' AS DateTime), CAST(N'2016-11-25T21:23:01.000' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (7, N'Miron                                             ', N'Borkowski                                         ', CAST(N'1998-01-23T13:42:23.000' AS DateTime), N'MironBorkowski@armyspy.com                        ', N'
53 675 26 74      ', CAST(N'2017-02-23T21:22:22.000' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (8, N'Penny                                             ', N'Rogers                                            ', CAST(N'2000-01-01T00:00:01.000' AS DateTime), N'
PennyWRogers@dayrep.com                         ', N'604-522-5443        ', CAST(N'2018-05-10T06:23:54.000' AS DateTime), CAST(N'2018-05-10T07:00:00.000' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (9, N'Anthony                                           ', N'Gray                                              ', CAST(N'1965-03-30T00:02:05.000' AS DateTime), N'
AnthonyHGray@teleworm.us                        ', N'
807-826-4244      ', CAST(N'2018-05-12T17:43:23.000' AS DateTime), NULL, 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (10, N'Janet                                             ', N'Savala                                            ', CAST(N'1989-12-23T00:23:23.000' AS DateTime), N'JanetLSavala@teleworm.us                          ', N'
819-548-5756      ', CAST(N'2018-06-02T15:23:12.000' AS DateTime), CAST(N'2018-07-23T12:12:12.000' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (11, N'Jan                                               ', N'Kowalski                                          ', CAST(N'1985-12-23T00:00:00.000' AS DateTime), N'jankowalski@gmail.com                             ', N'765 234 532         ', CAST(N'2020-01-11T17:45:03.020' AS DateTime), NULL, 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (12, N'Tadeusz                                           ', N'Nowak                                             ', CAST(N'1998-07-23T00:00:00.000' AS DateTime), N'tnowak@o2.pl                                      ', N'209 213 697         ', CAST(N'2020-01-11T17:45:27.410' AS DateTime), NULL, 0)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (13, N'Matthew                                           ', N'Forrester                                         ', CAST(N'1965-12-12T00:00:00.000' AS DateTime), N'MatthewEForrester@rhyta.com                       ', NULL, CAST(N'2020-01-11T18:01:27.953' AS DateTime), CAST(N'2020-01-11T20:25:51.107' AS DateTime), 1)
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [BirthDate], [Email], [Phone], [AddDate], [ModifiedDate], [IsActive]) VALUES (14, N'John                                              ', N'Dee                                               ', CAST(N'1995-02-01T00:00:00.000' AS DateTime), N'john.dee@gmail.com                                ', N'952 156 789         ', CAST(N'2020-01-11T18:05:46.857' AS DateTime), CAST(N'2020-01-11T18:41:23.717' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Borrow] ADD  CONSTRAINT [DF_Borrow_IsReturned]  DEFAULT ((0)) FOR [IsReturned]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_DictBookGenre] FOREIGN KEY([BookGenreId])
REFERENCES [dbo].[DictBookGenre] ([BookGenreId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_DictBookGenre]
GO
ALTER TABLE [dbo].[Borrow]  WITH CHECK ADD  CONSTRAINT [FK_Borrow_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Borrow] CHECK CONSTRAINT [FK_Borrow_Book]
GO
ALTER TABLE [dbo].[Borrow]  WITH CHECK ADD  CONSTRAINT [FK_Borrow_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Borrow] CHECK CONSTRAINT [FK_Borrow_User]
GO
USE [master]
GO
ALTER DATABASE [library] SET  READ_WRITE 
GO
