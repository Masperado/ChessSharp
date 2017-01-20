using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.Classes;

namespace ChessSharp.Models
{
    /// <summary>
    /// This class will be used as model for storage of all the games played on the website
    /// </summary>
    public class Game
    {
        public Guid GameId { get; set; }

        public string WhitePlayerId { get; set; }
        public ChessUser WhitePlayer { get; set; }

        public string BlackPlayerId { get; set; }
        public ChessUser BlackPlayer { get; set; }

        public DateTime GameDate { get; set; }

        /// <summary>
        /// This string represents list of moves played in game in respective order.
        /// </summary>
        public string PGN { get; set; }

        /// <summary>
        /// This string determines the current status of chessboard(location of chess pieces).
        /// Also represents info about legal castlings, enpassants...
        /// Useful for chessboard which was imported.
        /// </summary>
        public string FEN { get; set; }

        /// <summary>
        /// This class member determines if draw has been offered by one of the players.
        /// </summary>
        public bool DrawOffered { get; set; }

        /// <summary>
        /// State of this game.
        /// </summary>
        public GameState CurrentGameState { get; set; }

        /// <summary>
        /// Gets whose turn it is in this game.
        /// If it is black's turn, returns true, else returns false
        /// </summary>
        /// <returns>If it is black's turn, return true, else return false</returns>
        public string GetWhoseTurn()
        {
            if (FEN.Split(' ')[1] == "b")
            {
                return "Black";
            }
            else
            {
                return "White";
            }
        }

        public string GetPlayerOnTurnId()
        {
            if (GetWhoseTurn().Equals("Black"))
            {
                return BlackPlayerId;
            }
            else
            {
                return WhitePlayerId;
            }
        }

        public Game(string whitePlayer, string blackPlayer)
        {
            GameId = Guid.NewGuid();
            WhitePlayerId = whitePlayer;
            BlackPlayerId = blackPlayer;
            PGN = String.Empty;
            FEN = Constants.StartingFEN;
            DrawOffered = false;
            CurrentGameState = GameState.STILL_PLAYING;
        }

        public Game()
        {
            
        }


        public string gameState()
        {
            if (CurrentGameState.Equals(GameState.STILL_PLAYING)) return "%";
            else if(CurrentGameState.Equals(GameState.ABORTED)) return "&";
            else if (CurrentGameState.Equals(GameState.WHITE_WINS)) return "1-0";
            else if (CurrentGameState.Equals(GameState.DRAW)) return "0.5-0.5";
            else return "0-1";






        }
    }
}
