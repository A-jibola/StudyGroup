using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyGroup.Data;
using StudyGroup.Models;
using StudyGroup.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyGroup.Controllers
{
    [Authorize]
    public class TextController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TextController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("{controller}/{action}/{name}")]
        public IActionResult Index(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            var groupmessages = _context.Text.Where(t => t.GroupId == group.Id).OrderBy(t => t.DateSent);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var textGroup = new Group
            {
                Name = group.Name,
                Id = group.Id,
                GroupAdmin = group.GroupAdmin,
                GroupImage = group.GroupImage
            };

            TextMessageViewModel viewModel = new TextMessageViewModel
            {
                Texts = groupmessages,
                CurrentUser = claim.Value,
                GroupName = group.Name,
                CurrentUserName = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value).UserName,
                Group = textGroup
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var message = Request.Form["newMessage"];
            if (!String.IsNullOrEmpty(message))
            {
                var group = _context.Groups.FirstOrDefault(g => g.Name == name);

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                Text newMessage = new Text
                {
                    Content = message,
                    SenderId = claim.Value,
                    SenderName = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value).UserName,
                    DateSent = DateTime.Now,
                    Group = group,
                    GroupId = group.Id,
                };
                _context.Text.Add(newMessage);
                await _context.SaveChangesAsync();
                return Content("This Action ran to Completion");
            }
            else
            {
                return RedirectToAction(nameof(Index), new { name = name });
            }
        }
    }
}
