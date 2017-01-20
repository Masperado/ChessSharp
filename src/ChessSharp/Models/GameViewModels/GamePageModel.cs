using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessSharp.Models.GameViewModels
{
    public class GamePageModel
    {
        public string CurrentUserId { get; set; }
        public Game Game { get; set; }

    }
}
