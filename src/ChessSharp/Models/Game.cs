using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessSharp.Models
{
    /// <summary>
    /// This class will be used to store all the games played on the website
    /// </summary>
    public class Game
    {
        public Guid GameId { get; set; }

        public Guid WhitePlayer { get; set; }
        public Guid BlackPlayer { get; set; }

        public DateTime GameDate { get; set; }

        /// <summary>
        /// This string represents list of moves played in game in respective order.
        /// </summary>
        public String PGN { get; set; }

        /// <summary>
        /// This string determines the current status of chessboard(location of chess pieces).
        /// Also represents info about legal castlings, enpassants...
        /// Useful for chessboard which was imported.
        /// </summary>
        public String FEN { get; set; }

        /// <summary>
        /// This class member determines if draw has been offered by one of the players.
        /// </summary>
        public bool DrawOffered { get; set; }

        /// <summary>
        /// State of this game.
        /// </summary>
        public GameState CurrentGameState { get; set; }
    }
}
