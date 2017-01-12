using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using ChessSharp.Models;

namespace ChessSharp.CoreStuff
{
    public class ChessSharpDbContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Game> Games { get; set; }

        public ChessSharpDbContext(string connectionString) : base(connectionString) 
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();

            modelBuilder.Entity<Game>().HasKey(g => g.GameId);
            modelBuilder.Entity<Game>().Property(g => g.BlackPlayer).IsRequired();

        }
    }
}
