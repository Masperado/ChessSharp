using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.Classes;
using ChessSharp.Models;

namespace ChessSharp.CoreStuff.ChessRepository
{
    /// <summary>
    /// Repository to be used for easier querying trough database and doing
    /// all kinds of work.
    /// </summary>
    public interface IChessRepository
    {
        /// <summary>
        /// Gets users based on the parameters which user will insert through
        /// a form on a webapp.
        /// </summary>
        /// <param name="minElo">Minimal elo a player has to have</param>
        /// <param name="maxElo">Maximum elo a player has to have</param>
        /// <returns>All users that satisfy the criteria given.</returns>
        List<ChessUser> GetUsersBasedOnElo(int minElo, int maxElo);

        /// <summary>
        /// Gets all users of this webapp.
        /// </summary>
        /// <returns>All users of this webapp</returns>
        List<ChessUser> GetAllUsers(string userId);

        /// <summary>
        /// Adds new user to database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        void AddNewUser(string userId, string username);

        /// <summary>
        /// Gets user that has same username as passed parameter.
        /// </summary>
        /// <param name="username">Username as criteria</param>
        /// <returns>User that satisfies criteria</returns>
        ChessUser GetUserByName(string username);

        /// <summary>
        /// Gets all users that satisfy filter criteria.
        /// </summary>
        /// <param name="filterFunc">The filter criteria</param>
        /// <returns>All users that satisfy filter criteria.</returns>
        List<ChessUser> GetFilteredUsers(Func<ChessUser, bool> filterFunc);

        /// <summary>
        /// Gets user by his Id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns>User as ChessUser</returns>
        ChessUser GetUserById(string userId);

        /// <summary>
        /// Gets all pending requests from other players for a new game that
        /// a player (whose ID is inserted as a parameter) recieved.
        /// </summary>
        /// <param name="userId">User's id which requests will be returned</param>
        /// <returns>All pending requests</returns>
        List<Request> GetPendingRequests(string userId);

        /// <summary>
        /// Gets all sent requests to other players for a new game that
        /// have been sent by the player(which id is passed as parameter).
        /// </summary>
        /// <param name="userId">User's id</param>
        /// <returns>All sent requests.</returns>
        List<Request> GetSentRequests(string userId);

        /// <summary>
        /// Gets  request by its Id.
        /// </summary>
        /// <param name="requestId">Request's Id.</param>
        /// <returns>Request with that Id.</returns>
        Request GetRequestByID(Guid requestId);

        /// <summary>
        /// Deletes request
        /// </summary>
        /// <param name="request"></param>
        void DeleteRequest(Request request);

        /// <summary>
        /// Gets all games that satisfy filter criteria.
        /// </summary>
        /// <param name="filterFunc">The filter criteria.</param>
        /// <returns>Games that satisfy filter criteria.</returns>
        List<Game> GetFilteredGames(Func<Game, bool> filterFunc);

        /// <summary>
        /// Adds new pending request to user.
        /// </summary>
        /// <param name="userId">User's id to whom new pending request will be added.</param>
        /// <param name="request">Request to be added to user.</param>
        void AddNewPendingRequest(string userId, Request request);

        /// <summary>
        /// Adds new sent request to user.
        /// </summary>
        /// <param name="userId">User's id to whom new sent request will be added.</param>
        /// <param name="request">Request to be added to user.</param>
        void AddNewSentRequest(string userId, Request request);

        /// <summary>
        /// Gets all user games(Id will be passed as parameter).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All user's games.</returns>
        List<Game> GetAllUserGames(string userId);

        /// <summary>
        /// Creates new game between two players.
        /// </summary>
        /// <param name="whitePlayerId">Player1 id</param>
        /// <param name="blackPlayerId">Player2 id</param>
        void CreateNewGame(Game newGame);



    }
}
