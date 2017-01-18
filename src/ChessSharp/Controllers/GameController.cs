using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.ChessRepository;
using ChessSharp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult SubmitMove()
        {
            throw new NotImplementedException();
        }
    }
}