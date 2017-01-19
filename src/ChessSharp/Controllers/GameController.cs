using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChessSharp.CoreStuff.ChessRepository;
using ChessSharp.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Razor.Parser.Internal;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace ChessSharp.Controllers
{
    public class GameController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChessRepository _repository;

        public GameController(UserManager<ApplicationUser> userManager, IChessRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }



        public IActionResult Index(string gameId)
        {
            var game = _repository.GetGameById(Guid.Parse(gameId));

            return View(game);
        }

        
        public IActionResult SubmitMove(Game game)
        {
            //if (game.PGN == null)
            {
               // game.PGN = String.Empty;
            }
            //So I don't have to generate every input for every field of Game
            var theGame = _repository.GetGameById(game.GameId);

            _repository.UpdateGame(game.GameId, game.FEN, game.PGN);
            return RedirectToAction("Index", "Game", new {gameId = game.GameId});
        }
    }
}