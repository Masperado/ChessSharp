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

        public async Task<IActionResult> Profile()
        {
            var user = _repository.GetUserById(await GetUserIdAsync());
            var requests = _repository.GetPendingRequests(user.UserId);
            var users = _repository.GetAllUsers();
            
            var model = new ProfilePageModel
            {
                User = user,
                Users = users,
                PendingRequests = requests
            };
            
            return View(model);
        }

        public IActionResult SendRequest(SendRequestModel requestModel)
        {
            Request request = new Request(requestModel.SenderId, requestModel.ReceiverId);

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

        public async Task<string> GetUserIdAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return user.Id;
        } 
    }
}
