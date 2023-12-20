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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filter)
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
            var userid = claim.Value;

            var user = _context.AppUsers.FirstOrDefault(a => a.Id == userid);
            var GroupUserHas = _context.UserXGroups.Where(u => u.UserId == user.Id);
            var usersGroups = _context.Groups.Where(g => GroupUserHas.Any(group => group.GroupId == g.Id));

            string name;
            if (filter == "Groups" || String.IsNullOrWhiteSpace(filter))
            {
                name = "_Groups";
            }
            else
            {
                name = "_Reminders";
            }
            UserIndexViewModel viewModel = new UserIndexViewModel
            {
                Username = user.UserName,
                UserBio = user.BioInfo,
                Groups = usersGroups,
                Reminders = _context.Reminders.Where(r => r.ReceiverId == userid).Where(r => r.GroupId == null),
                PartialName = name
            };
            return View(viewModel);
        }
    }
}
