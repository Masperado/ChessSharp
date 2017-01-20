using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessSharp.Models
{
    public static class Constants
    {
        public static int StartingElo = 1600;
        public static int MinimumElo = 1200;
        public static int WeakElo = 1300;
        public static int StrongElo = 2000;

        //Constant used to calculate new Elo 
        public static int WeakK = 32;

        public static int StrongK = 16;

        public static string StartingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public static double GetExpectedScore(double par1, double par2)
        {
            return 1/(1 + Math.Pow(10, (par1 - par2)/ 400));
        }
    }
}
