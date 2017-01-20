using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.Models;

namespace ChessSharp.CoreStuff.Classes
{
    public class ChessUser
    {
        private double _elo;

        /// <summary>
        /// Type is string beacuse the ASP.net Identity uses string as it's type for Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Username for the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Integer used to determine the strength of a player.
        /// </summary>
        public double Elo
        {
            get { return _elo; }
            set
            {
                if (value < Constants.MinimumElo)
                {
                    _elo = Constants.MinimumElo;
                }
                else
                {
                    _elo = value;
                }
            }
        }

        /// <summary>
        /// All the requests for new game that this user has recieved.
        /// </summary>
        public List<Request> PendingRequests { get; set; }

        /// <summary>
        /// All the requests that this user has sent.
        /// </summary>
        public List<Request> SentRequests { get; set; }

        /// <summary>
        /// All the games this user has played as white.
        /// </summary>
        public List<Game> GamesHistoryAsWhite { get; set; }

        /// <summary>
        /// All the games this user has played as black.
        /// </summary>
        public List<Game> GamesHistoryAsBlack { get; set; }


        
    }
}
