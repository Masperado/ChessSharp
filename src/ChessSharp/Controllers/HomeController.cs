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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var user = _repository.GetUserById(await GetUserIdAsync());
            var pendingRequests = _repository.GetPendingRequests(user.UserId).OrderByDescending(r=>r.TimeSent.Date).ThenBy(r=>r.TimeSent.TimeOfDay).ToList();
            var sentRequests = _repository.GetSentRequests(user.UserId).OrderByDescending(r => r.TimeSent.Date).ThenBy(r => r.TimeSent.TimeOfDay).ToList();
            var users = _repository.GetAllUsers(user.UserId);
            
            var model = new ProfilePageModel
            {
                User = user,
                Users = users,
                PendingRequests = pendingRequests,
                SentRequests = sentRequests,
            

            };
            
            return View(model);
        }

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

        public async Task<string> GetUserIdAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return user.Id;
        }

        public IActionResult AcceptRequest(Guid requestId)
        {
            //Todo implement this method
            return RedirectToAction("Profile");
        }

        public IActionResult DeclineRequest(Guid requestId)
        {
            var request = _repository.GetRequestByID(requestId);
            _repository.DeleteRequest(request);

            return RedirectToAction("Profile");
            
        }
    }
}
