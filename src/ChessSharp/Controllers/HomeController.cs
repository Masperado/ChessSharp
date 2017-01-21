using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.ChessRepository;
using ChessSharp.CoreStuff.Classes;
using ChessSharp.Data;
using ChessSharp.Models;
using ChessSharp.Models.ProfileViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChessSharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChessRepository _repository;

        public HomeController(UserManager<ApplicationUser> userManager, IChessRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            {
              //  return RedirectToAction("Profile");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = _repository.GetUserById(await GetUserIdAsync());
            var pendingRequests = _repository.GetPendingRequests(user.UserId).OrderByDescending(r=>r.TimeSent).ToList();
            var sentRequests = _repository.GetSentRequests(user.UserId).OrderByDescending(r => r.TimeSent).ToList();
            var games =
                _repository.GetAllUserGames(user.UserId);
            var users = _repository.GetAllUsers(user.UserId);
            
            var model = new ProfilePageModel
            {
                User = user,
                Users = users,
                PendingRequests = pendingRequests,
                SentRequests = sentRequests,
                Games = games
            };
            
            return View(model);
        }

        [Authorize]
        public IActionResult SendRequest(SendRequestModel requestModel)
        {
            Request request = new Request(requestModel.SenderId, requestModel.ReceiverId, requestModel.ColorRequest);

            _repository.AddNewPendingRequest(requestModel.ReceiverId, request);
            _repository.AddNewSentRequest(request.SenderId, request);

            return RedirectToAction("Profile");
        }

        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public async Task<string> GetUserIdAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return user.Id;
        }

        [Authorize]
        public IActionResult AcceptRequest(Guid requestId)
        {
            var request = _repository.GetRequestById(requestId);

            Game newGame;
            if (request.ColorRequest == ColorRequest.BLACK)
            {
                //Sender wants to play black.
                newGame = new Game(request.RecieverId, request.SenderId);
            }

            else if (request.ColorRequest == ColorRequest.WHITE)
            {
                //Sender wants to play white.
                newGame = new Game(request.SenderId, request.RecieverId);
            }
            else
            {
                int deciderRand = new Random().Next(0, 2);

                if (deciderRand == 0)
                {
                    //decide at random, here sender will play black.
                    newGame = new Game(request.RecieverId, request.SenderId);
                }

                else
                {
                    //here sender will play white
                    newGame = new Game(request.SenderId, request.RecieverId);
                }
            }
            _repository.CreateNewGame(newGame);
            _repository.DeleteRequest(request);

            return RedirectToAction("Profile");
        }

        [Authorize]
        public IActionResult DeclineRequest(Guid requestId)
        {
            var request = _repository.GetRequestById(requestId);
            _repository.DeleteRequest(request);

            return RedirectToAction("Profile");
            
        }
    }
}
