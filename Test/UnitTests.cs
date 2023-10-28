using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using NToastNotify;
using System.Security.Claims;
using System.Security.Principal;
using Test.MappingProfiles;
using Web.Controllers;
using Web.Models;
using Xunit.Abstractions;

namespace Test
{
    public class UnitTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        // Private data
        private ControllerContext user1Context;
        private IMapper mapper;

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
        private FoodAndDrinksPreference fadp7;
        private FoodAndDrinksPreference fadp12;

        private Review r1;
        private Review r2;
        private Review r3;
        private Review r4;
        private Review r5;

        public UnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TestMappingProfile());
            }).CreateMapper();

            //Create data
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

            fadp7 = new FoodAndDrinksPreference
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

            // Initialize review objects
            r1 = new Review
            {
                Score = 5,
                Text = "This was a great board game night!",
                User = new User { FirstName = "Christian" },
                UserId = 2,
                BoardGameNightId = 1
            };

            r2 = new Review
            {
                Score = 4,
                Text = "This was a good board game night!",
                User = new User { FirstName = "Christian" },
                UserId = 2,
                BoardGameNightId = 2
            };

            r3 = new Review
            {
                Score = 3,
                Text = "This was an okay board game night!",
                UserId = 3,
                BoardGameNightId = 5
            };

            r4 = new Review
            {
                Score = 2,
                Text = "This was a bad board game night!",
                User = new User { FirstName = "Jelmar" },
                UserId = 4,
                BoardGameNightId = 1
            };

            r5 = new Review
            {
                Score = 1,
                Text = "This was a terrible board game night!",
                UserId = 2,
                BoardGameNightId = 6
            };

            // Initialize user objects
            user1 = new User
            {
                Id = 1,
                FirstName = "Khalid",
                LastName = "Mimouni",
                Email = "DJK@beats.nl",
                BirthDate = new DateTime(2011, 1, 1),
                FoodAndDrinksPreference = fadp1,
                FoodAndDrinksPreferenceId = 1,
                Gender = Domain.Enums.Gender.Other,
                City = "Enschede",
                Address = "Rode Stierweg 42",
                Reviews = new List<Review>() { r1, r2, r4 }
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
                FoodAndDrinksPreference = fadp7,
                FoodAndDrinksPreferenceId = 7,
                Host = user1,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> { bg2, bg4, bg5, bg6 },
                Attendees = new List<User> { }
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
                HostId = user3.Id,
                Attendees = new List<User> { }
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
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
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
            var model = Assert.IsType<(List<BoardGameNightViewModel> joinedGameNights, List<BoardGameNightViewModel> joinableGameNights)>(result!.Model);

            Assert.Single(model.joinedGameNights);
            Assert.Contains(bgn5.Id, model.joinedGameNights.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn3.Id, model.joinedGameNights.Select(bgnvm => bgnvm.Id));
        }
        
        [Fact]
        public void BoardGameNight_Overview_Has_Joinable_Boardgames() //US 01
        {
            //Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);

            BoardGameNightRepositoryMock.GetAllJoinableBoardGameNightsForUser(user1.Id)
                .Returns(new List<BoardGameNight>() { bgn6, bgn7, bgn8 });

            //Act
            var result = sut.Joining() as ViewResult;

            //Assert
            var model = Assert.IsType<(List<BoardGameNightViewModel> joinedGameNights, List<BoardGameNightViewModel> joinableGameNights)>(result!.Model);

            Assert.Equal(3, model.joinableGameNights.Count());
            Assert.Contains(bgn7.Id, model.joinableGameNights.Select(bgnvm => bgnvm.Id));
            Assert.Contains(bgn8.Id, model.joinableGameNights.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn1.Id, model.joinableGameNights.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn5.Id, model.joinableGameNights.Select(bgnvm => bgnvm.Id));
        }

        [Fact]
        public void BoardGameNight_Hosting_Overview_Has_Upcoming_Hosted_Boardgames() //US 01
        {
            //Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);

            BoardGameNightRepositoryMock.GetAllUpcomingHostingBoardGameNightsForUser(user1.Id)
                .Returns(new List<BoardGameNight>() { bgn2, bgn3 });

            //Act
            var result = sut.Hosting() as ViewResult;

            //Assert
            var model = Assert.IsType<(List<BoardGameNightViewModel> upcomingBoardGameNightVMs, List<BoardGameNightViewModel> pastBoardGameNightVMs)>(result!.Model);

            Assert.Equal(2, model.upcomingBoardGameNightVMs.Count());
            Assert.Contains(bgn3.Id, model.upcomingBoardGameNightVMs.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn1.Id, model.upcomingBoardGameNightVMs.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn4.Id, model.upcomingBoardGameNightVMs.Select(bgnvm => bgnvm.Id));
        }

        [Fact]
        public void BoardGameNight_Hosting_Overview_Has_Previously_Hosted_Boardgames() //US 01
        {
            //Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);

            BoardGameNightRepositoryMock.GetAllPastHostingBoardGameNightsForUser(user1.Id)
                .Returns(new List<BoardGameNight>() { bgn1, bgn4 });

            //Act
            var result = sut.Hosting() as ViewResult;

            //Assert
            var model = Assert.IsType<(List<BoardGameNightViewModel> upcomingBoardGameNightVMs, List<BoardGameNightViewModel> pastBoardGameNightVMs)>(result!.Model);

            Assert.Equal(2, model.pastBoardGameNightVMs.Count());
            Assert.Contains(bgn4.Id, model.pastBoardGameNightVMs.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn2.Id, model.pastBoardGameNightVMs.Select(bgnvm => bgnvm.Id));
            Assert.DoesNotContain(bgn3.Id, model.pastBoardGameNightVMs.Select(bgnvm => bgnvm.Id));
        }

        [Fact]
        public void Host_New_BoardGameNight() //US 02
        {
            //Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // BoardGameNight
            var newBgn = new BoardGameNight
            {
                Id = 9,
                MaxAttendees = 2,
                DateTime = DateTime.Now.AddDays(4),
                IsAdultOnly = false,
                City = "Breda",
                Address = "Hogeschoollaan 44",
                FoodAndDrinksPreferenceId = 3,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> { bg4 },
                Attendees = new List<User> { user2 }
            };

            var newBgnVm = mapper.Map<BoardGameNightViewModel>(newBgn);

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);

            //Act
            var result = sut.Create(newBgnVm);

            //Assert
            Assert.True(sut.ModelState.IsValid);
            BoardGameNightRepositoryMock.Received().CreateBoardGameNight(Arg.Is<BoardGameNight>(bgn => bgn.Id == newBgn.Id));
            Assert.Equal("Board game night created successfully!", sut.TempData["SuccessMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Hosting", redirectToAction.ActionName);
        }

        [Fact]
        public void Edit_BoardGameNight() //US 02
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Create a new BoardGameNight and its ViewModel
            var bgn3Copy = new BoardGameNight
            {
                Id = 3,
                MaxAttendees = 8,
                DateTime = DateTime.Now.AddDays(4),
                IsAdultOnly = true,
                City = "Leeuwarden", // Rotterdam -> Leeuwarden
                Address = "Stadhuisplein 1",
                FoodAndDrinksPreferenceId = 7,
                Host = user1,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> { bg2, bg4, bg5, bg6 },
                Attendees = new List<User> { }
            };
            var editedBgnVm = mapper.Map<BoardGameNightViewModel>(bgn3Copy);

            // Mock Returns          
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn3Copy.Id)
                .Returns(bgn3Copy);

            //Act
            var result = sut.Edit(editedBgnVm);

            //Assert
            Assert.True(sut.ModelState.IsValid);
            BoardGameNightRepositoryMock.Received().UpdateBoardGameNight(Arg.Is<BoardGameNight>(bgn => bgn.City!.Equals("Leeuwarden")));
            Assert.Equal("Board game night updated successfully!", sut.TempData["SuccessMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Details", redirectToAction.ActionName);
            Assert.Equal(bgn3Copy.Id, redirectToAction.RouteValues!["boardGameNightId"]);
        }


        [Fact]
        public void Remove_BoardGameNight() //US 02
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;  

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(3)
                .Returns(bgn3);

            // Act
            var result = sut.Delete(3);

            // Assert
            Assert.Equal("Board game night deleted successfully!", sut.TempData["SuccessMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Hosting", redirectToAction.ActionName);
            BoardGameNightRepositoryMock.Received().DeleteBoardGameNight(Arg.Is<BoardGameNight>(bgn => bgn.Id == 3));
        }

        [Fact]
        public void Remove_BoardGameNight_Only_Allowed_With_No_Attendees() //US 02
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(2)
                .Returns(bgn2);

            // Act
            var result = sut.Delete(2);

            // Assert
            Assert.Equal("Can't delete board game night, someone has joined", sut.TempData["ErrorMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Details", redirectToAction.ActionName);
            Assert.Equal(bgn2.Id, redirectToAction.RouteValues!["boardGameNightId"]);
        }

        [Fact]
        public void BoardGame_Has_IsAdultOnly_Field() //US 03
        {
            // Arrange
            var boardGameType = typeof(BoardGame);

            // Act
            var isAdultOnlyProperty = boardGameType.GetProperty("IsAdultOnly");

            // Assert
            Assert.NotNull(isAdultOnlyProperty);
            Assert.True(isAdultOnlyProperty.PropertyType == typeof(bool));
        }


        [Fact]
        public void BoardGameNight_Has_IsAdultOnly_Field() //US 03
        {
            // Arrange
            var boardGameNightType = typeof(BoardGameNight);

            // Act
            var isAdultOnlyProperty = boardGameNightType.GetProperty("IsAdultOnly");

            // Assert
            Assert.NotNull(isAdultOnlyProperty);
            Assert.True(isAdultOnlyProperty.PropertyType == typeof(bool));
        }


        [Fact]
        public void BoardGameNight_With_AdultOnly_BoardGame_Automatically_IsAdultOnly_True() //US 03
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            var adultBg = new BoardGame
            {
                Id = 1,
                Name = "Adults Only Game",
                Description = "Description",
                Genre = Genre.Strategy,
                GameType = GameType.Board,
                IsAdultOnly = true // Adult Bg
            };

            var nonAdultBgn = new BoardGameNight
            {
                Id = 1,
                MaxAttendees = 5,
                DateTime = DateTime.Now.AddDays(1),
                IsAdultOnly = false, // NON-Adult Bgn
                City = "TestCity",
                Address = "TestAddress",
                FoodAndDrinksPreference = fadp12,
                FoodAndDrinksPreferenceId = 12,
                Host = user1,
                HostId = user1.Id,
                BoardGames = new List<BoardGame> (),
                Attendees = new List<User>()
            };
              
            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(nonAdultBgn.Id)
                .Returns(nonAdultBgn);
            BoardGameRepositoryMock.GetBoardGameById(adultBg.Id)
                .Returns(adultBg);

            // Act
            sut.AddBoardGame(nonAdultBgn.Id, adultBg.Id);

            // Assert
            var updatedBgn = BoardGameNightRepositoryMock.GetBoardGameNightById(nonAdultBgn.Id);
            Assert.True(updatedBgn!.IsAdultOnly);
        }

        [Fact]
        public void Underage_User_Cannot_Attend_IsAdultOnly_True_BoardGameNight() //US 03
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            var adultBgn = new BoardGameNight
            {
                Id = 1,
                MaxAttendees = 5,
                DateTime = DateTime.Now.AddDays(1),
                IsAdultOnly = true, // Set as Adults Only
                City = "AdultCity",
                Address = "AdultAddress",
                FoodAndDrinksPreferenceId = 12,
                HostId = user2.Id,
                Attendees = new List<User>()
            };

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(adultBgn.Id)
                .Returns(adultBgn);

            // Act
            var result = sut.Join(adultBgn.Id);

            // Assert
            Assert.Equal("This board game night is for adults only", sut.TempData["ErrorMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Details", redirectToAction.ActionName);
            Assert.Equal(adultBgn.Id, redirectToAction.RouteValues!["boardGameNightId"]);
            BoardGameNightRepositoryMock.DidNotReceive().AttendBoardGameNight(Arg.Any<int>(), Arg.Any<int>());
        }

        [Fact]
        public void User_Can_Join_BoardGameNight() //US 04
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn8.Id)
                .Returns(bgn8);

            // Act
            var result = sut.Join(bgn8.Id);

            // Assert
            Assert.Equal("You have joined the board game night!", sut.TempData["SuccessMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Details", redirectToAction.ActionName);
            Assert.Equal(bgn8.Id, redirectToAction.RouteValues!["boardGameNightId"]);
            BoardGameNightRepositoryMock.Received().AttendBoardGameNight(user1.Id, bgn8.Id);
        }

        [Fact]
        public void User_Cannot_Join_Full_BoardGameNight() //US 04
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            var bgn8full = new BoardGameNight
            {
                Id = 8,
                MaxAttendees = 2,
                DateTime = DateTime.Now.AddDays(8),
                IsAdultOnly = false,
                City = "Groningen",
                Address = "Grote Markt 1",
                FoodAndDrinksPreferenceId = 12,
                HostId = user3.Id,
                Attendees = new List<User> { user2, user4 }
            };

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn8full.Id)
                .Returns(bgn8full);

            //Act
            var result = sut.Join(bgn8full.Id);

            //Assert
            Assert.Equal("Board game night is already full", sut.TempData["ErrorMessage"]);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Details", redirectToAction.ActionName);
            Assert.Equal(bgn8full.Id, redirectToAction.RouteValues!["boardGameNightId"]);
            BoardGameNightRepositoryMock.DidNotReceive().AttendBoardGameNight(Arg.Any<int>(), Arg.Any<int>());
        }

        [Fact]
        public void User_Can_Only_Join_One_BoardGameNight_Per_Day() //US 04
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            var bgnTomorrow1 = new BoardGameNight
            {
                Id = 10,
                MaxAttendees = 10,
                DateTime = DateTime.Now.Date.AddDays(1).AddHours(1),
                IsAdultOnly = false,
                Attendees = new List<User>()
            };

            var bgnTomorrow2 = new BoardGameNight
            {
                Id = 11,
                MaxAttendees = 10,
                DateTime = DateTime.Now.Date.AddDays(1).AddHours(2),
                IsAdultOnly = false,
                Attendees = new List<User>() 
            };

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(10)
                .Returns(bgnTomorrow1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(11)
                .Returns(bgnTomorrow2);
            BoardGameNightRepositoryMock.HasBoardGameNightAtSameDay(user1.Id, bgnTomorrow1.DateTime)
                .Returns(false);
            BoardGameNightRepositoryMock.HasBoardGameNightAtSameDay(user1.Id, bgnTomorrow2.DateTime)
                .Returns(true);

            // Act
            var result1 = sut.Join(10);
            var result2 = sut.Join(11);

            // Assert
            Assert.Equal("You have already joined a board game night at the same day", sut.TempData["ErrorMessage"]);
            BoardGameNightRepositoryMock.Received().AttendBoardGameNight(Arg.Any<int>(), Arg.Is<int>(10));
            BoardGameNightRepositoryMock.DidNotReceive().AttendBoardGameNight(Arg.Any<int>(), Arg.Is<int>(11));
        }

        [Fact]
        public void BoardGameNight_Overview_Has_BoardGames_Played_Listed() //US 05
        {
            // Arrange
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;
      
            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn3.Id)
                .Returns(bgn3); //bgn 3 has bg 2, 4, 5, 6

            // Act
            var result = sut.Details(bgn3.Id);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsType<BoardGameNightViewModel>(viewResult.Model);
            var boardGameNightVM = (BoardGameNightViewModel)viewResult.Model;
            Assert.Equal(4, boardGameNightVM.BoardGames!.Count); 
            Assert.Contains(bg2.Name, boardGameNightVM.BoardGames.Select(bg => bg.Name));
            Assert.Contains(bg4.Name, boardGameNightVM.BoardGames.Select(bg => bg.Name));
            Assert.Contains(bg5.Name, boardGameNightVM.BoardGames.Select(bg => bg.Name));
            Assert.Contains(bg6.Name, boardGameNightVM.BoardGames.Select(bg => bg.Name));
        }

        [Fact]
        public void BoardGame_Overview_Has_Required_Info_Listed() //US 05
        {
            // Arrange
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameController(mapper, BoardGameRepositoryMock);

            BoardGameRepositoryMock.GetBoardGameById(1)
                .Returns(bg1);

            // Act
            var result = sut.Details(1);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsType<BoardGameViewModel>(viewResult.Model);
            var boardGameVM = (BoardGameViewModel)viewResult.Model;
            //Rquired Info: Name, Description, Genre, IsAdultOnly
            Assert.Equal(bg1.Name, boardGameVM.Name);
            Assert.Equal(bg1.Description, boardGameVM.Description);
            Assert.Equal(bg1.Genre, boardGameVM.Genre);
            Assert.False(boardGameVM.IsAdultOnly);
        }

        [Fact]
        public void BoardGameNight_Overview_Has_FoodAndDrinks_Info_Listed() //US 06
        {
            //Info: Lactose, Nuts, Vegetarion, Alcohol
            // Arrange
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn3.Id)
                .Returns(bgn3);

            // Act
            var result = sut.Details(bgn3.Id);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsType<BoardGameNightViewModel>(viewResult.Model);
            var boardGameNightVM = (BoardGameNightViewModel)viewResult.Model;
            Assert.False(boardGameNightVM.FoodAndDrinksPreference!.LactoseFree);
            Assert.False(boardGameNightVM.FoodAndDrinksPreference.NutFree);
            Assert.False(boardGameNightVM.FoodAndDrinksPreference.Vegetarian);
            Assert.False(boardGameNightVM.FoodAndDrinksPreference.AlcoholFree);
        }

        [Fact]
        public void BoardGameNight_User_Join_Incompatible_FoodAndDrinkPreferences_Notified() //US 06
        {
            // Arrange
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            user1.FoodAndDrinksPreference!.AlcoholFree = true; // Change Preference for Warning
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn3.Id) // Bgn3 NOT Alcoholfree
                .Returns(bgn3);
       
            // Act
            var result = sut.Details(bgn3.Id);
            user1.FoodAndDrinksPreference.AlcoholFree = false; // Revert Changed Preference

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var boardGameNightVM = viewResult.Model as BoardGameNightViewModel;
            Assert.NotNull(boardGameNightVM);
            var foodAndDrinksWarning = viewResult.ViewData["FoodAndDrinksWarning"] as bool?;
            Assert.NotNull(foodAndDrinksWarning);
            Assert.True(foodAndDrinksWarning);
        }

        [Fact]
        public void User_Able_To_Set_FoodAndDrinkPreferences() //US 06
        {
            // Arrange
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            //var UserManagerMock = Substitute.For<UserManager<IdentityUser>>(); Not Mockable with NSubstitute
            //var SignInManagerMock = Substitute.For<SignInManager<IdentityUser>>(); Not Mockable with NSubstitute

            // Controller (System Under Test)
            var sut = new AccountController(null!, null!, mapper, ToastNotificationMock, UserRepositoryMock, FoodAndDrinksPrefRepositoryMock);
            sut.ControllerContext = user1Context;

            var fadpAltered = new FoodAndDrinksPreference
            {
                AlcoholFree = true, // False -> True
                Vegetarian = false,
                NutFree = true, // False -> True
                LactoseFree = false,
            };

            var fadpAlteredVm = mapper.Map<UserPreferencesViewModel>(fadpAltered);

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);

            // Act
            var result = sut.Preferences(fadpAlteredVm);

            // Assert
            Assert.IsType<ViewResult>(result);
            FoodAndDrinksPrefRepositoryMock
                .Received()
                .UpdateFoodAndDrinksPref(Arg.Is<FoodAndDrinksPreference>(receivedFadp =>
                    receivedFadp.AlcoholFree == fadpAltered.AlcoholFree &&
                    receivedFadp.Vegetarian == fadpAltered.Vegetarian &&
                    receivedFadp.NutFree == fadpAltered.NutFree &&
                    receivedFadp.LactoseFree == fadpAltered.LactoseFree
                ));
            ToastNotificationMock.Received().AddSuccessToastMessage("Preferences updated");
        }

        [Fact]
        public void Review_Contains_Required_Info() //US 08
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new ReviewController(UserRepositoryMock, ToastNotificationMock, BoardGameNightRepositoryMock, ReviewRepositoryMock, mapper);
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            ReviewRepositoryMock.GetReviewsForBoardGameNightsHostedByUser(user1.Id)
                .Returns(user1.Reviews);
            BoardGameNightRepositoryMock.GetAllPastHostingBoardGameNightsForUser(user1.Id)
                .Returns(new List<BoardGameNight>() { bgn1, bgn4 });

            // Act
            var result = sut.Overview();

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var reviewOverviewVM = viewResult.Model as ReviewOverviewViewModel;
            Assert.NotNull(reviewOverviewVM);
            var reviews = reviewOverviewVM.Reviews;
            Assert.NotNull(reviews);
            Assert.NotEmpty(reviews);
            foreach (var review in reviews)
            {
                Assert.InRange(review.Score, 1, 5);
                Assert.False(string.IsNullOrWhiteSpace(review.Text)); // Check if the review text is not empty
            }
        }

        [Fact]
        public void Review_Show_User_That_Wrote_It() //US 08
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new ReviewController(UserRepositoryMock, ToastNotificationMock, BoardGameNightRepositoryMock, ReviewRepositoryMock, mapper);
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            ReviewRepositoryMock.GetReviewsForBoardGameNightsHostedByUser(user1.Id)
                .Returns(user1.Reviews);
            BoardGameNightRepositoryMock.GetAllPastHostingBoardGameNightsForUser(user1.Id)
                .Returns(new List<BoardGameNight>() { bgn1, bgn4 });

            // Act
            var result = sut.Overview();

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var reviewOverviewVM = viewResult.Model as ReviewOverviewViewModel;
            Assert.NotNull(reviewOverviewVM);
            var reviews = reviewOverviewVM.Reviews;
            Assert.NotNull(reviews);
            Assert.NotEmpty(reviews);
            var reviewerNames = new List<string>() { user2.FirstName!, user4.FirstName! };
            foreach (var review in reviews)
            {
                Assert.Contains(review.User!.FirstName, reviewerNames);
            }
        }

        [Fact]
        public void BoardGameNight_Host_Average_ReviewScore_Listed() //US 08
        {
            // Arrange
            // Mocks
            var ToastNotificationMock = Substitute.For<IToastNotification>();
            var UserRepositoryMock = Substitute.For<IUserRepository>();
            var BoardGameNightRepositoryMock = Substitute.For<IBoardGameNightRepository>();
            var BoardGameRepositoryMock = Substitute.For<IBoardGameRepository>();
            var FoodAndDrinksPrefRepositoryMock = Substitute.For<IFoodAndDrinksPrefRepository>();
            var ReviewRepositoryMock = Substitute.For<IReviewRepository>();

            // Controller (System Under Test)
            var sut = new BoardGameNightController(mapper, ToastNotificationMock, UserRepositoryMock, BoardGameNightRepositoryMock, BoardGameRepositoryMock, FoodAndDrinksPrefRepositoryMock, ReviewRepositoryMock);
            sut.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
            sut.ControllerContext = user1Context;

            // Mock Returns
            UserRepositoryMock.GetUserByEmail("DJK@beats.nl")
                .Returns(user1);
            BoardGameNightRepositoryMock.GetBoardGameNightById(bgn3.Id)
                .Returns(bgn3); 

            // Act
            var result = sut.Details(bgn3.Id);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var boardGameNightVM = viewResult.Model as BoardGameNightViewModel;
            Assert.NotNull(boardGameNightVM);
            Assert.Equal(user1.Reviews!.Average(review => review.Score), boardGameNightVM.Host!.Reviews!.Average(review => review.Score));
        }
    }
}