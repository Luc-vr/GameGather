using AutoMapper;
using Domain.Entities;
using DomainServices;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using NToastNotify;
using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Principal;
using Test.MappingProfiles;
using Web.Controllers;
using Web.Models;
using Xunit.Abstractions;

namespace Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        // Private data
        private ControllerContext user1Context;
        private IMapper mapperMock;

        private User user1;
        private User user2;
        private User user3;
        private User user4;

        private BoardGame bg1;
        private BoardGame bg2;
        private BoardGame bg3;
        private BoardGame bg4;
        private BoardGame bg5;
        private BoardGame bg6;

        private BoardGameNight bgn1;
        private BoardGameNight bgn2;
        private BoardGameNight bgn3;
        private BoardGameNight bgn4;
        private BoardGameNight bgn5;
        private BoardGameNight bgn6;
        private BoardGameNight bgn7;
        private BoardGameNight bgn8;

        private FoodAndDrinksPreference fadp1;
        private FoodAndDrinksPreference fadp2;
        private FoodAndDrinksPreference fadp3;
        private FoodAndDrinksPreference fadp4;
        private FoodAndDrinksPreference fadp5;
        private FoodAndDrinksPreference fadp6;
        private FoodAndDrinksPreference fadp7;
        private FoodAndDrinksPreference fadp8;
        private FoodAndDrinksPreference fadp9;
        private FoodAndDrinksPreference fadp10;
        private FoodAndDrinksPreference fadp11;
        private FoodAndDrinksPreference fadp12;

        private Review r1;
        private Review r2;
        private Review r3;
        private Review r4;
        private Review r5;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TestMappingProfile());
            }).CreateMapper();

            //Create data
            InitializePrivateData();
        }

        private void InitializePrivateData()
        {
            // Initialize Controller Context
            user1Context = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new GenericIdentity("DJK@beats.nl", "test"))
                }
            };

            // Initialize food and drinks preference objects
            fadp1 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp2 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp3 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp4 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp5 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp6 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp7 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp8 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp9 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp10 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp11 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            fadp12 = new FoodAndDrinksPreference
            {
                AlcoholFree = false,
                Vegetarian = false,
                NutFree = false,
                LactoseFree = false,
            };

            // Initialize user objects
            user1 = new User
            {
                Id = 1,
                FirstName = "Khalid",
                LastName = "Mimouni",
                Email = "DJK@beats.nl",
                BirthDate = new DateTime(2011, 1, 1),
                FoodAndDrinksPreferenceId = 1,
                Gender = Domain.Enums.Gender.Other,
                City = "Enschede",
                Address = "Rode Stierweg 42"
            };

            user2 = new User
            {
                Id = 2,
                FirstName = "Christian",
                LastName = "Nijssen",
                Email = "runescape@chris.nl",
                BirthDate = new DateTime(1995, 1, 1),
                FoodAndDrinksPreferenceId = 2,
                Gender = Domain.Enums.Gender.Male,
                City = "Tilburg",
                Address = "Verdwijnlaan 6"
            };

            user3 = new User
            {
                Id = 3,
                FirstName = "Janou",
                LastName = "Blom",
                Email = "breda@janou.nl",
                BirthDate = new DateTime(1993, 1, 1),
                FoodAndDrinksPreferenceId = 3,
                Gender = Domain.Enums.Gender.Female,
                City = "Breda",
                Address = "De Haven 9"
            };

            user4 = new User
            {
                Id = 4,
                FirstName = "Jelmar",
                LastName = "Dekker",
                Email = "jelmar@geld.nl",
                BirthDate = new DateTime(1993, 1, 1),
                FoodAndDrinksPreferenceId = 4,
                Gender = Domain.Enums.Gender.Male,
                City = "Maasdam",
                Address = "Welvaartstraat 1"
            };

            bg1 = new BoardGame
            {
                Id = 1,
                Name = "Monopoly",
                Description = "Monopoly is a board game about buying and trading properties.",
                Genre = Domain.Enums.Genre.Economic,
                GameType = Domain.Enums.GameType.Board,
                IsAdultOnly = false,
            };

            bg2 = new BoardGame
            {
                Id = 2,
                Name = "Scrabble",
                Description = "Scrabble is a word game that is played on a square board divided into a grid of cells. " +
                "The game's primary objective is to form words on the board using letter tiles, each of which is marked " +
                "with a specific point value. The game combines elements of vocabulary, spelling, strategy, and competition, " +
                "making it a popular choice for players of all ages.",
                Genre = Domain.Enums.Genre.Puzzle,
                GameType = Domain.Enums.GameType.Board,
                IsAdultOnly = false,
            };

            bg3 = new BoardGame
            {
                Id = 3,
                Name = "Chess",
                Description = "Chess is a two-player strategy board game that is widely regarded as one of the most " +
                "intellectually demanding and historically significant games in the world. It is played on a square " +
                "board divided into 64 squares of alternating colors, and it involves moving various types of pieces " +
                "with distinct movement rules across the board to capture the opponent's king.",
                Genre = Domain.Enums.Genre.Strategy,
                GameType = Domain.Enums.GameType.Board,
                IsAdultOnly = false,
            };

            bg4 = new BoardGame
            {
                Id = 4,
                Name = "Poker",
                Description = "Poker is a popular card game that combines skill, strategy, and chance. It is played in " +
                "various formats, with the objective of winning chips or money from other players by forming the best " +
                "possible hand or by bluffing opponents into folding their hands.",
                Genre = Domain.Enums.Genre.Strategy,
                GameType = Domain.Enums.GameType.Cards,
                IsAdultOnly = false,
            };

            bg5 = new BoardGame
            {
                Id = 5,
                Name = "Minecraft",
                Description = "Minecraft is a renowned sandbox video game that invites players into a vast and imaginative " +
                "realm where they can shape their own experiences. Set in a world composed of blocks, players mine resources, " +
                "craft tools, and construct structures, ranging from simple shelters to intricate cities. With modes like " +
                "Survival, where players must gather resources and fend off creatures, and Creative, which offers unlimited " +
                "resources for building without limitations, Minecraft caters to a wide range of gameplay styles.",
                Genre = Domain.Enums.Genre.Other,
                GameType = Domain.Enums.GameType.VideoGame,
                IsAdultOnly = false,
            };

            bg6 = new BoardGame
            {
                Id = 6,
                Name = "Puzzle",
                Description = "A jigsaw puzzle is a game where pieces are fitted together to form a complete picture, " +
                "providing an engaging and satisfying experience for all ages.",
                Genre = Domain.Enums.Genre.Puzzle,
                GameType = Domain.Enums.GameType.Puzzle,
                IsAdultOnly = false,
            };

            // Initialize board game night objects
            bgn1 = new BoardGameNight
            {
                Id = 1,
                MaxAttendees = 4,
                DateTime = DateTime.Now.AddDays(-5),
                IsAdultOnly = false,
                City = "Zwijndrecht",
                Address = "Koninginneweg 12",
                FoodAndDrinksPreferenceId = 5,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> { bg1, bg2, bg3 },
                Attendees = new List<User> { user2, user3, user4 }
            };

            bgn2 = new BoardGameNight
            {
                Id = 2,
                MaxAttendees = 15,
                DateTime = DateTime.Now.AddDays(1),
                IsAdultOnly = false,
                City = "Breda",
                Address = "Dorpstraat 45",
                FoodAndDrinksPreferenceId = 6,
                HostId = user1.Id,
                Attendees = new List<User> { user2, user3, user4 }
            };

            bgn3 = new BoardGameNight
            {
                Id = 3,
                MaxAttendees = 8,
                DateTime = DateTime.Now.AddDays(4),
                IsAdultOnly = true,
                City = "Rotterdam",
                Address = "Stadhuisplein 1",
                FoodAndDrinksPreferenceId = 7,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> { bg2, bg4, bg5, bg6 }
            };

            bgn4 = new BoardGameNight
            {
                Id = 4,
                MaxAttendees = 6,
                DateTime = DateTime.Now.AddDays(-4),
                IsAdultOnly = false,
                City = "Amsterdam",
                Address = "Damrak 1",
                FoodAndDrinksPreferenceId = 8,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> { bg1, bg3, bg5 }
            };

            bgn5 = new BoardGameNight
            {
                Id = 5,
                MaxAttendees = 10,
                DateTime = DateTime.Now.AddDays(-2),
                IsAdultOnly = true,
                City = "Utrecht",
                Address = "Domplein 29",
                FoodAndDrinksPreferenceId = 9,
                HostId = user2.Id,
                BoardGames = new List<BoardGame> { bg1, bg2, bg3, bg4, bg5, bg6 },
                Attendees = new List<User> { user1, user3 }
            };

            bgn6 = new BoardGameNight
            {
                Id = 6,
                MaxAttendees = 5,
                DateTime = DateTime.Now.AddDays(6),
                IsAdultOnly = true,
                City = "Eindhoven",
                Address = "Stationsplein 17",
                FoodAndDrinksPreferenceId = 10,
                HostId = user3.Id
            };

            bgn7 = new BoardGameNight
            {
                Id = 7,
                MaxAttendees = 12,
                DateTime = DateTime.Now.AddDays(7),
                IsAdultOnly = false,
                City = "Den Haag",
                Address = "Binnenhof 8",
                FoodAndDrinksPreferenceId = 11,
                HostId = user3.Id
            };

            bgn8 = new BoardGameNight
            {
                Id = 8,
                MaxAttendees = 3,
                DateTime = DateTime.Now.AddDays(8),
                IsAdultOnly = false,
                City = "Groningen",
                Address = "Grote Markt 1",
                FoodAndDrinksPreferenceId = 12,
                HostId = user3.Id
            };

            // Initialize review objects
            r1 = new Review
            {
                Score = 5,
                Text = "This was a great board game night!",
                UserId = user2.Id,
                BoardGameNightId = bgn1.Id
            };

            r2 = new Review
            {
                Score = 4,
                Text = "This was a good board game night!",
                UserId = user2.Id,
                BoardGameNightId = bgn2.Id
            };

            r3 = new Review
            {
                Score = 3,
                Text = "This was an okay board game night!",
                UserId = user3.Id,
                BoardGameNightId = bgn5.Id
            };

            r4 = new Review
            {
                Score = 2,
                Text = "This was a bad board game night!",
                UserId = user4.Id,
                BoardGameNightId = bgn1.Id
            };

            r5 = new Review
            {
                Score = 1,
                Text = "This was a terrible board game night!",
                UserId = user2.Id,
                BoardGameNightId = bgn6.Id
            };
        }

        [Fact]
        public void BoardGameNight_Overview_Has_Joined_Boardgames() //US 01
        {
            //Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var bgn5vm = mapperMock.Map<BoardGameNightViewModel>(bgn5);

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapperMock, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);

            BoardGameNightRepositoryMock.GetAllAttendingBoardGameNightsForUser(user1.Id)
                .Returns(new List<BoardGameNight>(){ bgn5 });

            //Act
            var result = sut.Joining() as ViewResult;

            //Assert
            var model = Assert.IsType<(List<BoardGameNightViewModel> joinedGameNights, List<BoardGameNightViewModel> joinableGameNights)>(result.Model);

            Assert.Single(model.joinedGameNights);
            Assert.Contains(bgn5.Id, model.joinedGameNights.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn3.Id, model.joinedGameNights.Select(bgnvm => bgnvm.Id));
        }
        
        [Fact]
        public void BoardGameNight_Overview_Has_Joinable_Boardgames() //US 01
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGameNight_Hosting_Overview_Has_Upcoming_Hosted_Boardgames() //US 01
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGameNight_Hosting_Overview_Has_Previously_Hosted_Boardgames() //US 01
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Host_New_BoardGame() //US 02
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Edit_New_BoardGame() //US 02
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }


        [Fact]
        public void Remove_New_BoardGame() //US 02
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Remove_BoardGame_Only_Allowed_With_No_Attendees() //US 02
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGame_Has_IsAdultOnly_Field() //US 03
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }


        [Fact]
        public void BoardGameNight_Has_IsAdultOnly_Field() //US 03
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }


        [Fact]
        public void BoardGameNight_With_AdultOnly_BoardGame_Automatically_IsAdultOnly_True() //US 03
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Underage_User_Cannot_Attend_IsAdultOnly_True_BoardGameNight() //US 03
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void User_Can_Join_BoardGameNight() //US 04
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void User_Can_Not_Join_Full_BoardGameNight() //US 04
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void User_Can_Only_Join_One_BoardGameNight_Per_Day() //US 04
        {
            //User cannot join two boardgame nights on the same day, time doesnt matter only date.
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGameNight_Overview_Has_BoardGames_Played_Listed() //US 05
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGame_Overview_Has_Required_Info_Listed() //US 05
        {
            //Rquired Info: Name, Description, Genre, IsAdultOnly
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGameNight_Overview_Has_FoodAndDrinks_Info_Listed() //US 06
        {
            //Info: Lactose, Nuts, Vegetarion, Alcohol
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void BoardGameNight_User_Join_Incompatible_FoodAndDrinkPreferences_Notified() //US 06
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void User_Able_To_Set_FoodAndDrinkPreferences() //US 06
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Review_Contains_Required_Info() //US 08
        {
            //Required Info: Score 1-5, Text
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void Review_Show_User_That_Wrote_It() //US 08
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void GameBoardNight_Host_Average_ReviewScore_Listed() //US 08
        {
            //Arrange

            //Act

            //Assert
            Assert.True(true);
        }
    }
}