using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChessSharp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Elo is rating commonly used scoring system in chess world to determine the player's strength
        /// </summary>
        public int Elo { get; set; }

        public List<Game> AllGames { get; set; }
    }
}
