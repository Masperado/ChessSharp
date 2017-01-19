using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.Classes;
using ChessSharp.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ChessSharp.Data;

namespace ChessSharp.CoreStuff.ChessRepository
{
    public class ChessRepository : IChessRepository
    {
        private readonly ChessSharpDbContext _context;


        public List<ChessUser> GetUsersBasedOnElo(string userId, int minElo, int maxElo)
        {
            return GetAllUsers(userId).Where(cu => cu.Elo >= minElo && cu.Elo <= maxElo).ToList();
        }

        public List<ChessUser> GetAllUsers(string userId)
        {
            return _context.ChessUsers.Where(cu => !cu.UserId.Equals(userId)).ToList();
        }

        public void AddNewUser(string userId, string username)
        {
            _context.ChessUsers.Add(new ChessUser
            {
                UserId = userId,
                Username = username,
                Elo=Constants.StartingElo
                
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

        public Request GetRequestById(Guid requestId)
        {
            return _context.Requests.Find(requestId);
        }

        public void DeleteRequest(Request request)
        {
            _context.Requests.Remove(request);
            _context.SaveChanges();
        }

        public List<Game> GetFilteredGames(Func<Game, bool> filterFunc)
        {
            return _context.Games.Where(filterFunc).ToList();
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
            try
            {
                var gamesHistoryAsWhite = _context.ChessUsers
                    .Where(cu => cu.UserId.Equals(userId))
                    .Include(cu => cu.GamesHistoryAsWhite)
                    .FirstOrDefault()
                    .GamesHistoryAsWhite;
                var gamesHistoryAsBlack = _context.ChessUsers
                    .Where(cu => cu.UserId.Equals(userId))
                    .Include(cu => cu.GamesHistoryAsBlack)
                    .FirstOrDefault()
                    .GamesHistoryAsBlack;

                var gamesHistory = new List<Game>(gamesHistoryAsWhite);
                gamesHistory.AddRange(gamesHistoryAsBlack);
                gamesHistory = gamesHistory.OrderByDescending(g => g.GameDate).ToList();

                return gamesHistory;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public Game GetGameById(Guid gameId)
        {
            try
            {
                var game = _context.Games
                    .Where(g => g.GameId.Equals(gameId))
                    .Include(g => g.WhitePlayer)
                    .Include(g => g.BlackPlayer)
                    .FirstOrDefault();

                return game;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }

        public void CreateNewGame(Game newGame)
        {
            //Add game to whitePlayer's whitegames, and to blackPlayer's black games
            var whitePlayer = _context.ChessUsers
                              .Where(cu => cu.UserId.Equals(newGame.WhitePlayerId))
                              .Include(cu => cu.GamesHistoryAsWhite)
                              .FirstOrDefault();
            if (whitePlayer != null)
            {
                whitePlayer.GamesHistoryAsWhite.Add(newGame);
                _context.SaveChanges();
            }

            var blackPlayer = _context.ChessUsers
                              .Where(cu => cu.UserId.Equals(newGame.BlackPlayerId))
                              .Include(cu => cu.GamesHistoryAsBlack)
                              .FirstOrDefault();
            if (blackPlayer != null)
            {
                blackPlayer.GamesHistoryAsBlack.Add(newGame);
                _context.SaveChanges();
            }
        }

        public void UpdateGame(Guid gameId, string fen, string pgn)
        {
            var game = _context.Games.Find(gameId);

            if (game != null)
            {
                game.FEN = fen;
                game.PGN = pgn;
                _context.Games.AddOrUpdate(game);
                _context.SaveChanges(); 
            }
        }

        public ChessRepository(ChessSharpDbContext context)
        {
            _context = context;
        }
    }
}

