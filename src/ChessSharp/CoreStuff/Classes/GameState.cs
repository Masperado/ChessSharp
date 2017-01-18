using System.Runtime.Remoting.Messaging;

namespace ChessSharp.Models
{
    /// <summary>
    /// This enum determines the state of the current chess game.
    /// </summary>
    public enum GameState
    {
        WHITE_WINS,
        BLACK_WINS,
        DRAW,
        ABORTED,
        STILL_PLAYING

    }


    
}