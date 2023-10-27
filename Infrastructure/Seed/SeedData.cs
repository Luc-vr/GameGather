using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Seed;
public class SeedData : ISeedData
{
    private GameGatherDbContext _context;
    private ILogger<SeedData> _logger;

    public SeedData(GameGatherDbContext context, ILogger<SeedData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void EnsurePopulated(bool dropExisting = false)
    {
        if (dropExisting)
        {
            _context.Database.EnsureDeleted();
        }
        _context.Database.Migrate();

        // Seed food and drinks preferences
        if (_context.FoodAndDrinksPreferences?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed food and drinks prefs");

            for (int i = 0; i < 12; i++)
            {
                FoodAndDrinksPreference fadp = new()
                {
                    AlcoholFree = false,
                    Vegetarian = false,
                    NutFree = false,
                    LactoseFree = false,
                };

                _context.FoodAndDrinksPreferences.Add(fadp);
            }

            _context.SaveChanges();
            _logger.LogInformation("Food and drinks prefs seeded");
        } else
        {
            _logger.LogInformation("Food and drinks prefs not seeded");
        }

        // Seed users
        User user1 = new()
        {
            FirstName = "Khalid",
            LastName = "Mimouni",
            Email = "DJK@beats.nl",
            BirthDate = new DateTime(2007, 1, 1),
            FoodAndDrinksPreferenceId = 1,
            Gender = Domain.Enums.Gender.Other,
            City = "Enschede",
            Address = "Rode Stierweg 42"
        };

        User user2 = new()
        {
            FirstName = "Christian",
            LastName = "Nijssen",
            Email = "runescape@chris.nl",
            BirthDate = new DateTime(1995, 1, 1),
            FoodAndDrinksPreferenceId = 2,
            Gender = Domain.Enums.Gender.Male,
            City = "Tilburg",
            Address = "Verdwijnlaan 6"
        };

        User user3 = new()
        {
            FirstName = "Janou",
            LastName = "Blom",
            Email = "breda@janou.nl",
            BirthDate = new DateTime(1993, 1, 1),
            FoodAndDrinksPreferenceId = 3,
            Gender = Domain.Enums.Gender.Female,
            City = "Breda",
            Address = "De Haven 9"
        };

        User user4 = new()
        {
            FirstName = "Jelmar",
            LastName = "Dekker",
            Email = "jelmar@geld.nl",
            BirthDate = new DateTime(1993, 1, 1),
            FoodAndDrinksPreferenceId = 4,
            Gender = Domain.Enums.Gender.Male,
            City = "Maasdam",
            Address = "Welvaartstraat 1"
        };

        if (_context.Users?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed users");

            _context.Users.AddRange(new[]
            {
                user1,
                user2,
                user3,
                user4
            });
            _context.SaveChanges();
            _logger.LogInformation("Users seeded");
        } else
        {
            _logger.LogInformation("Users not seeded");
        }

        BoardGame bg1 = new()
        {
            Name = "Monopoly",
            Description = "Monopoly is a board game about buying and trading properties.",
            Genre = Domain.Enums.Genre.Economic,
            GameType = Domain.Enums.GameType.Board,
            IsAdultOnly = false,
            Image = File.ReadAllBytes(Path.Combine("wwwroot/seed-images", "monopoly.jpg"))
        };

        BoardGame bg2 = new()
        {
            Name = "Scrabble",
            Description = "Scrabble is a word game that is played on a square board divided into a grid of cells. " +
            "The game's primary objective is to form words on the board using letter tiles, each of which is marked " +
            "with a specific point value. The game combines elements of vocabulary, spelling, strategy, and competition, " +
            "making it a popular choice for players of all ages.",
            Genre = Domain.Enums.Genre.Puzzle,
            GameType = Domain.Enums.GameType.Board,
            IsAdultOnly = false,
            Image = File.ReadAllBytes(Path.Combine("wwwroot/seed-images", "scrabble.jpg"))
        };

        BoardGame bg3 = new()
        {
            Name = "Chess",
            Description = "Chess is a two-player strategy board game that is widely regarded as one of the most " +
            "intellectually demanding and historically significant games in the world. It is played on a square " +
            "board divided into 64 squares of alternating colors, and it involves moving various types of pieces " +
            "with distinct movement rules across the board to capture the opponent's king.",
            Genre = Domain.Enums.Genre.Strategy,
            GameType = Domain.Enums.GameType.Board,
            IsAdultOnly = false,
            Image = File.ReadAllBytes(Path.Combine("wwwroot/seed-images", "chess.jpg"))
        };

        BoardGame bg4 = new()
        {
            Name = "Poker",
            Description = "Poker is a popular card game that combines skill, strategy, and chance. It is played in " +
            "various formats, with the objective of winning chips or money from other players by forming the best " +
            "possible hand or by bluffing opponents into folding their hands.",
            Genre = Domain.Enums.Genre.Strategy,
            GameType = Domain.Enums.GameType.Cards,
            IsAdultOnly = false,
            Image = File.ReadAllBytes(Path.Combine("wwwroot/seed-images", "poker.jpg"))
        };

        BoardGame bg5 = new()
        {
            Name = "Minecraft",
            Description = "Minecraft is a renowned sandbox video game that invites players into a vast and imaginative " +
            "realm where they can shape their own experiences. Set in a world composed of blocks, players mine resources, " +
            "craft tools, and construct structures, ranging from simple shelters to intricate cities. With modes like " +
            "Survival, where players must gather resources and fend off creatures, and Creative, which offers unlimited " +
            "resources for building without limitations, Minecraft caters to a wide range of gameplay styles.",
            Genre = Domain.Enums.Genre.Other,
            GameType = Domain.Enums.GameType.VideoGame,
            IsAdultOnly = false,
            Image = File.ReadAllBytes(Path.Combine("wwwroot/seed-images", "minecraft.jpg"))
        };

        BoardGame bg6 = new()
        {
            Name = "Puzzle",
            Description = "A jigsaw puzzle is a game where pieces are fitted together to form a complete picture, " +
            "providing an engaging and satisfying experience for all ages.",
            Genre = Domain.Enums.Genre.Puzzle,
            GameType = Domain.Enums.GameType.Puzzle,
            IsAdultOnly = false,
            Image = File.ReadAllBytes(Path.Combine("wwwroot/seed-images", "puzzle.jpg"))
        };

        // Seed board games
        if (_context.BoardGames?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed board games");

            _context.BoardGames.AddRange(new[]
            {
                bg1,
                bg2,
                bg3,
                bg4,
                bg5,
                bg6
            });

            _context.SaveChanges();
            _logger.LogInformation("Board games seeded");
        } else
        {
            _logger.LogInformation("Board games not seeded");
        }

        // Seed board game nights
        BoardGameNight bgn1 = new()
        {
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

        BoardGameNight bgn2 = new()
        {
            MaxAttendees = 15,
            DateTime = DateTime.Now.AddDays(1),
            IsAdultOnly = false,
            City = "Breda",
            Address = "Dorpstraat 45",
            FoodAndDrinksPreferenceId = 6,
            HostId = user1.Id,
            Attendees = new List<User> { user2, user3, user4 }
        };

        BoardGameNight bgn3 = new()
        {
            MaxAttendees = 8,
            DateTime = DateTime.Now.AddDays(4),
            IsAdultOnly = true,
            City = "Rotterdam",
            Address = "Stadhuisplein 1",
            FoodAndDrinksPreferenceId = 7,
            HostId = user1.Id,
            BoardGames = new List<BoardGame> { bg2, bg4, bg5, bg6 }
        };

        BoardGameNight bgn4 = new()
        {
            MaxAttendees = 6,
            DateTime = DateTime.Now.AddDays(-4),
            IsAdultOnly = false,
            City = "Amsterdam",
            Address = "Damrak 1",
            FoodAndDrinksPreferenceId = 8,
            HostId = user1.Id,
            BoardGames = new List<BoardGame> { bg1, bg3, bg5 }
        };

        BoardGameNight bgn5 = new()
        {
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

        BoardGameNight bgn6 = new()
        {
            MaxAttendees = 5,
            DateTime = DateTime.Now.AddDays(6),
            IsAdultOnly = true,
            City = "Eindhoven",
            Address = "Stationsplein 17",
            FoodAndDrinksPreferenceId = 10,
            HostId = user3.Id
        };

        BoardGameNight bgn7 = new()
        {
            MaxAttendees = 12,
            DateTime = DateTime.Now.AddDays(7),
            IsAdultOnly = false,
            City = "Den Haag",
            Address = "Binnenhof 8",
            FoodAndDrinksPreferenceId = 11,
            HostId = user3.Id
        };

        BoardGameNight bgn8 = new()
        {
            MaxAttendees = 3,
            DateTime = DateTime.Now.AddDays(8),
            IsAdultOnly = false,
            City = "Groningen",
            Address = "Grote Markt 1",
            FoodAndDrinksPreferenceId = 12,
            HostId = user3.Id
        };

        if (_context.BoardGameNights?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed board game nights");

            _context.BoardGameNights.AddRange(new[]
            {
                bgn1,
                bgn2,
                bgn3,
                bgn4,
                bgn5,
                bgn6,
                bgn7,
                bgn8
            });

            _context.SaveChanges();
            _logger.LogInformation("Board game nights seeded");
        } else
        {
            _logger.LogInformation("Board game nights not seeded");
        }

        // Seed reviews
        if (_context.Reviews?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed reviews");

            Review r1 = new()
            {
                Score = 5,
                Text = "This was a great board game night!",
                UserId = user2.Id,
                BoardGameNightId = bgn1.Id
            };

            Review r2 = new()
            {
                Score = 4,
                Text = "This was a good board game night!",
                UserId = user2.Id,
                BoardGameNightId = bgn2.Id
            };

            Review r3 = new()
            {
                Score = 3,
                Text = "This was an okay board game night!",
                UserId = user3.Id,
                BoardGameNightId = bgn5.Id
            };

            Review r4 = new()
            {
                Score = 2,
                Text = "This was a bad board game night!",
                UserId = user4.Id,
                BoardGameNightId = bgn1.Id
            };

            Review r5 = new()
            {
                Score = 1,
                Text = "This was a terrible board game night!",
                UserId = user2.Id,
                BoardGameNightId = bgn6.Id
            };

            _context.Reviews.AddRange(new[]
            {
                r1,
                r2,
                r3,
                r4,
                r5
            });

            _context.SaveChanges();
            _logger.LogInformation("Reviews seeded");
        } else
        {
            _logger.LogInformation("Reviews not seeded");
        }

    }
}
