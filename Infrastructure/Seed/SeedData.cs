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
        FoodAndDrinksPreference fadp1 = new()
        {
            AlcoholFree = false,
            Vegetarian = false,
            NutFree = false,
            LactoseFree = false,
        };

        FoodAndDrinksPreference fadp2 = new()
        {
            AlcoholFree = true,
            Vegetarian = true,
            NutFree = true,
            LactoseFree = true,
        };

        FoodAndDrinksPreference fadp3 = new()
        {
            AlcoholFree = true,
            Vegetarian = false,
            NutFree = false,
            LactoseFree = false,
        };

        FoodAndDrinksPreference fadp4 = new()
        {
            AlcoholFree = false,
            Vegetarian = true,
            NutFree = false,
            LactoseFree = false,
        };

        FoodAndDrinksPreference fadp5 = new()
        {
            AlcoholFree = false,
            Vegetarian = false,
            NutFree = false,
            LactoseFree = false,
        };

        FoodAndDrinksPreference fadp6 = new()
        {
            AlcoholFree = true,
            Vegetarian = true,
            NutFree = true,
            LactoseFree = true,
        };

        User user1 = new()
        {
            FirstName = "Khalid",
            LastName = "Mimouni",
            Email = "DJK@beats.nl",
            BirthDate = new DateTime(1990, 1, 1),
            FoodAndDrinksPreference = fadp1,
            Gender = Domain.Enums.Gender.Male,
            City = "Enschede",
            Street = "Rode Stierweg",
            HouseNumber = 42,
        };

        if (dropExisting)
        {
            _context.Database.EnsureDeleted();
        }
        _context.Database.Migrate();

        // Seed food and drinks preferences
        if (_context.FoodAndDrinksPreferences?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed food and drinks prefs");

            _context.FoodAndDrinksPreferences.AddRange(new[]
            {
                fadp1,
                fadp2,
                fadp3,
                fadp4,
                fadp5,
                fadp6,
                });
            _context.SaveChanges();
            _logger.LogInformation("Food and drinks prefs seeded");
        } else
        {
            _logger.LogInformation("Food and drinks prefs not seeded");
        }

        // Seed users
        if (_context.Users?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed users");

            _context.Users.AddRange(new[]
            {
                user1,
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
                DateTime = DateTime.Now.AddDays(1),
                IsAdultOnly = false,
                City = "Zwijndrecht",
                Street = "Koninginneweg",
                HouseNumber = 12,
                FoodAndDrinksPreferenceId = fadp5.Id,
                HostId = user1.Id
            };

            BoardGameNight bgn2 = new()
            {
                MaxAttendees = 15,
                DateTime = DateTime.Now.AddDays(2),
                IsAdultOnly = false,
                City = "Breda",
                Street = "Dorpstraat",
                HouseNumber = 45,
                FoodAndDrinksPreferenceId = 6,
                HostId = user1.Id
            };

            _context.BoardGameNights.AddRange(new[]
            {
                bgn1,
                bgn2
            });

            _context.SaveChanges();
            _logger.LogInformation("Board game nights seeded");
        } else
        {
            _logger.LogInformation("Board game nights not seeded");
        }

    }
}
