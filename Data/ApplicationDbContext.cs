using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyGroup.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserXGroups> UserXGroups { get; set; }

        public DbSet<Reminder> Reminders { get; set; }

        public DbSet<Text> Text { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<BlockedUsers> BlockedUsers { get; set; }
    }
}
