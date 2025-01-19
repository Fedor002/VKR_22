using Microsoft.EntityFrameworkCore;
using VKR_Visik;
using Microsoft.Extensions.Configuration;
using System;
using System.Numerics;
using VKR_Visik.Models;
using VKR_Visik.Classes;

namespace VKR_Visik
{
    public class ApplicationDdContext : DbContext
    {
        public ApplicationDdContext(DbContextOptions<ApplicationDdContext> options) : base(options)
        {

        }

        public DbSet<Sections> Sections { get; set; }
        public DbSet<Themes> Themes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<MessageHistory> MessageHistory { get; set; }
    }
}
