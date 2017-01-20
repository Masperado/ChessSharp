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
        public DrawOfferState DrawOffered { get; set; }

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
            DrawOffered = DrawOfferState.NONE;
            CurrentGameState = GameState.STILL_PLAYING;
        }

        public Game()
        {
            
        }


        public string GetGameStateScore()
        {
            if (CurrentGameState.Equals(GameState.STILL_PLAYING)) return "%";
            else if(CurrentGameState.Equals(GameState.ABORTED)) return "&";
            else if (CurrentGameState.Equals(GameState.WHITE_WINS)) return "1-0";
            else if (CurrentGameState.Equals(GameState.DRAW)) return "0.5-0.5";
            else return "0-1";
        }

        public DrawOfferState GetRightDrawState(string userId)
        {
            return userId.Equals(WhitePlayerId) ? DrawOfferState.WHITE_OFFERED : DrawOfferState.BLACK_OFFERED;
        }

        public void ChangePlayersElo()
        {
            if (!CurrentGameState.Equals(GameState.STILL_PLAYING) || CurrentGameState.Equals(GameState.ABORTED))
            {
                double whiteScore = double.Parse(GetGameStateScore().Split('-')[0]);
                double blackScore = double.Parse(GetGameStateScore().Split('-')[1]);

                double expectedBlack = Constants.GetExpectedScore(WhitePlayer.Elo, BlackPlayer.Elo);
                double expectedWhite = Constants.GetExpectedScore(BlackPlayer.Elo, WhitePlayer.Elo);

                int whiteConstant, blackConstant;

                if (BlackPlayer.Elo >= Constants.StrongElo)
                    blackConstant = Constants.StrongK;
                else
                    blackConstant = Constants.WeakK;

                BlackPlayer.Elo = Calculate(BlackPlayer.Elo, blackConstant, blackScore, expectedBlack);

                if (WhitePlayer.Elo >= Constants.StrongElo)
                    whiteConstant = Constants.StrongK;
                else
                    whiteConstant = Constants.WeakK;

                WhitePlayer.Elo = Calculate(WhitePlayer.Elo, whiteConstant, whiteScore, expectedWhite);


            }
        }

        public double Calculate(double elo, int constant, double score, double expected)
        {
            return elo + constant * (score - expected);
        }

        public string GetUserColor(string userId)
        {
            if (userId.Equals(WhitePlayerId))
            {
                return "white";
            }
            else
            {
                return "black";
            }
        }
    }
}
