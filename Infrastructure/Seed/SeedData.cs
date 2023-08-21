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
            BirthDate = new DateTime(2011, 1, 1),
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

        // Seed board games
        if (_context.BoardGames?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed board games");

            BoardGame bg1 = new()
            {
                Name = "Monopoly",
                Description = "Monopoly is a board game...",
                Genre = Domain.Enums.Genre.Economic,
                GameType = Domain.Enums.GameType.Board,
                IsAdultOnly = false,
                Image = null,
            };

            BoardGame bg2 = new()
            {
                Name = "Scrabble",
                Description = "Scrabble is a word game...",
                Genre = Domain.Enums.Genre.Puzzle,
                GameType = Domain.Enums.GameType.Board,
                IsAdultOnly = false,
                Image = null
            };

            BoardGame bg3 = new()
            {
                Name = "Chess",
                Description = "Chess is a two-player strategy board game...",
                Genre = Domain.Enums.Genre.Strategy,
                GameType = Domain.Enums.GameType.Board,
                IsAdultOnly = false,
                Image = null
            };

            _context.BoardGames.AddRange(new[]
            {
                bg1,
                bg2,
                bg3
            });

            _context.SaveChanges();
            _logger.LogInformation("Board games seeded");
        } else
        {
            _logger.LogInformation("Board games not seeded");
        }

        // Seed board game nights
        if (_context.BoardGameNights?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed board game nights");

            BoardGameNight bgn1 = new()
            {
                MaxAttendees = 4,
                DateTime = DateTime.Now.AddDays(-1),
                IsAdultOnly = false,
                City = "Zwijndrecht",
                Address = "Koninginneweg 12",
                FoodAndDrinksPreferenceId = 5,
                HostId = user1.Id
            };

            BoardGameNight bgn2 = new()
            {
                MaxAttendees = 15,
                DateTime = DateTime.Now.AddDays(-2),
                IsAdultOnly = false,
                City = "Breda",
                Address = "Dorpstraat 45",
                FoodAndDrinksPreferenceId = 6,
                HostId = user1.Id
            };

            BoardGameNight bgn3 = new()
            {
                MaxAttendees = 8,
                DateTime = DateTime.Now.AddDays(-3),
                IsAdultOnly = true,
                City = "Rotterdam",
                Address = "Stadhuisplein 1",
                FoodAndDrinksPreferenceId = 7,
                HostId = user1.Id
            };

            BoardGameNight bgn4 = new()
            {
                MaxAttendees = 6,
                DateTime = DateTime.Now.AddDays(-4),
                IsAdultOnly = false,
                City = "Amsterdam",
                Address = "Damrak 1",
                FoodAndDrinksPreferenceId = 8,
                HostId = user1.Id
            };

            BoardGameNight bgn5 = new()
            {
                MaxAttendees = 10,
                DateTime = DateTime.Now.AddDays(5),
                IsAdultOnly = true,
                City = "Utrecht",
                Address = "Domplein 29",
                FoodAndDrinksPreferenceId = 9,
                HostId = user2.Id
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

    }
}
