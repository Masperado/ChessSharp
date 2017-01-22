using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using ChessSharp.CoreStuff.Classes;
using ChessSharp.Models;

namespace ChessSharp.Data
{
    public class ChessSharpDbContext : DbContext
    {
        public IDbSet<ChessUser> ChessUsers { get; set; }
        public IDbSet<Request> Requests { get; set; }
        public IDbSet<Game> Games { get; set; }

        public ChessSharpDbContext(string connectionString) : base(connectionString) 
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChessUser>().HasKey(cu => cu.UserId);
            modelBuilder.Entity<ChessUser>().HasMany(cu => cu.PendingRequests).WithRequired(r => r.Reciever).HasForeignKey(r => r.RecieverId).WillCascadeOnDelete(false);
            modelBuilder.Entity<ChessUser>().HasMany(cu => cu.SentRequests).WithRequired(r => r.Sender).HasForeignKey(r => r.SenderId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>().HasKey(r => r.RequestId);

            modelBuilder.Entity<Game>().HasKey(g => g.GameId);
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.BlackPlayer)
                .WithMany(cu => cu.GamesHistoryAsBlack)
                .HasForeignKey(g => g.BlackPlayerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.WhitePlayer)
                .WithMany(cu => cu.GamesHistoryAsWhite)
                .HasForeignKey(g => g.WhitePlayerId)
                .WillCascadeOnDelete(false);
        }
    }
}
