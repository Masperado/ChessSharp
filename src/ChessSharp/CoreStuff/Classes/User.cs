using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessSharp.Models
{
    public class User
    {

        public Guid Id { get; set; }

        public String Username { get; set; }

        /// <summary>
        /// Elo is rating commonly used in chess world to determine the player's strength
        /// </summary>
        public int Elo { get; set; }

        public List<Guid> GameHistory { get; set; }

        public User(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            Elo = Constants.StartingElo;
        }

       
    }
}
