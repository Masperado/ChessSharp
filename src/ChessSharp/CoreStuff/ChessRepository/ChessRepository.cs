using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.Classes;
using ChessSharp.Models;
using System.Data.Entity;

namespace ChessSharp.CoreStuff.ChessRepository
{
    public class ChessRepository : IChessRepository
    {
        private readonly ChessSharpDbContext _context;


        public List<ChessUser> GetUsersBasedOnElo(int minElo, int maxElo)
        {
            return _context.ChessUsers.Where(cu => cu.Elo >= minElo && cu.Elo <= maxElo).ToList();
        }

        public List<ChessUser> GetAllUsers()
        {
            return _context.ChessUsers.ToList();
        }

        public void AddNewUser(string userId, string username)
        {
            _context.ChessUsers.Add(new ChessUser
            {
                UserId = userId,
                Username = username
            });
            _context.SaveChanges();
        }

        public ChessUser GetUserByName(string username)
        {
            return _context.ChessUsers.FirstOrDefault(cu => cu.Username.Equals(username));
        }

        public List<ChessUser> GetFilteredUsers(Func<ChessUser, bool> filterFunc)
        {
            return _context.ChessUsers.Where(filterFunc).ToList();
        }

        public ChessUser GetUserById(string userId)
        {
            return _context.ChessUsers.Find(userId);
        }

        public List<Request> GetPendingRequests(string userId)
        {
            try
            {
                var requests = _context.ChessUsers
                .Where(cu => cu.UserId.Equals(userId))
                .Include(cu => cu.PendingRequests)
                .FirstOrDefault()
                .PendingRequests;

                return requests;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
            
            
        }

        public List<Request> GetSentRequests(string userId)
        {
            try
            {
                var requests = _context.ChessUsers
                .Where(cu => cu.UserId.Equals(userId))
                .Include(cu => cu.SentRequests)
                .FirstOrDefault()
                .SentRequests;

                return requests;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public void AddNewPendingRequest(string userId, Request request)
        {
            var user = _context.ChessUsers
                               .Where(cu => cu.UserId.Equals(userId))
                               .Include(cu => cu.PendingRequests)
                               .FirstOrDefault();
            if (user != null)
            {
                user.PendingRequests.Add(request);
                _context.SaveChanges();
            }
            
        }

        public void AddNewSentRequest(string userId, Request request)
        {
            var user = _context.ChessUsers
                              .Where(cu => cu.UserId.Equals(userId))
                              .Include(cu => cu.SentRequests)
                              .FirstOrDefault();
            if (user != null)
            {
                user.SentRequests.Add(request);
                _context.SaveChanges();
            }
        }

        public List<Game> GetAllUserGames(string userId)
        {
            return _context.ChessUsers.Find(userId).GamesHistory;
        }

        public void CreateNewGame(string whitePlayerId, string blackPlayerId)
        {
            _context.Games.Add(new Game(whitePlayerId, blackPlayerId));
            _context.SaveChanges();
        }

        public ChessRepository(ChessSharpDbContext context)
        {
            _context = context;
        }
    }
}
