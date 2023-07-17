using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seed
{
    public class SeedDataIdentity : ISeedData
    {
        private IdentityDbContext _context;
        private ILogger<SeedDataIdentity> _logger;

        public SeedDataIdentity(IdentityDbContext context, ILogger<SeedDataIdentity> logger)
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
            if (_context.Users?.Count() == 0)
            {
                _logger.LogInformation("Preparing to seed identity users");
                PasswordHasher<IdentityUser> ph = new();

                var user1 = new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb1",
                    Email = "DJK@beats.nl",
                    UserName = "DJK@beats.nl",
                    NormalizedEmail = "DJK@BEATS.NL",
                    NormalizedUserName = "DJK@BEATS.NL"
                };

                user1.PasswordHash = ph.HashPassword(user1, "Khalid123");

                var user2 = new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
                    Email = "runescape@chris.nl",
                    UserName = "runescape@chris.nl",
                    NormalizedEmail = "RUNESCAPE@CHRIS.NL",
                    NormalizedUserName = "RUNESCAPE@CHRIS.NL"
                };

                user2.PasswordHash = ph.HashPassword(user2, "Chris123");

                var user3 = new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb3",
                    Email = "breda@janou.nl",
                    UserName = "breda@janou.nl",
                    NormalizedEmail = "BREDA@JANOU.NL",
                    NormalizedUserName = "BREDA@JANOU.NL"
                };

                user3.PasswordHash = ph.HashPassword(user3, "Janou123");

                var user4 = new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb4",
                    Email = "jelmar@geld.nl",
                    UserName = "jelmar@geld.nl",
                    NormalizedEmail = "JELMAR@GELD.NL",
                    NormalizedUserName = "JELMAR@GELD.NL"
                };

                user4.PasswordHash = ph.HashPassword(user4, "Jelmar123");


                _context.Users.AddRange(new[]
                {
                    user1,
                    user2,
                    user3,
                    user4
                });

                _context.SaveChanges();
                _logger.LogInformation("Identity users seeded");
            } else
            {
                _logger.LogInformation("Identity users not seeded");
            }
        }
    }
}
