using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChessSharp.CoreStuff.ChessRepository;
using ChessSharp.CoreStuff.Classes;
using ChessSharp.Models;
using ChessSharp.Models.GameViewModels;
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



        public async Task<IActionResult> Index(string gameId)
        {
            var game = _repository.GetGameById(Guid.Parse(gameId));
            ViewData["UserId"] = await GetUserIdAsync();

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
            theGame.FEN = game.FEN;
            theGame.PGN = game.PGN;
            theGame.CurrentGameState = game.CurrentGameState;
            _repository.UpdateGame(theGame);
            return RedirectToAction("Index", new {gameId = theGame.GameId.ToString()});
        }

        public async Task<IActionResult> Resign(string gameId)
        {
            var game = _repository.GetGameById(Guid.Parse(gameId));
            
            //This user resigned.
            var currentUserId = _repository.GetUserById(await GetUserIdAsync()).UserId;

            game.CurrentGameState = currentUserId.Equals(game.BlackPlayerId) ? GameState.WHITE_WINS : GameState.BLACK_WINS;

            

            _repository.UpdateGame(game);

            return RedirectToAction("Profile", "Home");
        }

        public IActionResult OfferDraw(string gameId, DrawOfferState drawState)
        {
            var game = _repository.GetGameById(Guid.Parse(gameId));
            game.DrawOffered = drawState;

            _repository.UpdateGame(game);
            return RedirectToAction("Index", new {gameId = gameId});
        }

        public IActionResult Abort(string gameId)
        {
            var game = _repository.GetGameById(Guid.Parse(gameId));

            game.CurrentGameState = GameState.ABORTED;

            _repository.UpdateGame(game);

            return RedirectToAction("Profile", "Home");
        }

        public IActionResult AcceptDraw(string gameId)
        {
            var game = _repository.GetGameById(Guid.Parse(gameId));

            game.CurrentGameState = GameState.DRAW;

            _repository.UpdateGame(game);
            return RedirectToAction("Profile", "Home");
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<string> GetUserIdAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return user.Id;
        }
    }
}