using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyGroup.Data;
using StudyGroup.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyGroup.Controllers
{
    [Authorize]
    public class CallController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CallController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Video(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(group != null)
            {

                    group.VideoCall = true;

                    CallViewModel viewModel = new CallViewModel
                    {
                        GroupId = group.Id,
                        GroupName = group.Name,
                        Username = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value).UserName,
                        CurrentUser = claim.Value,
                        GroupAdminId = group.GroupAdmin
                    };
                    await _context.SaveChangesAsync();
                    return View(viewModel);

            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = name });

            }
        }

        public async Task<IActionResult> Voice(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (group != null)
            {

                    group.VoiceCall = true;
                    CallViewModel viewModel = new CallViewModel
                    {
                        GroupName = group.Name,
                        GroupId = group.Id,
                        Username = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value).UserName,
                        CurrentUser = claim.Value,
                        GroupAdminId = group.GroupAdmin
                    };
                    await _context.SaveChangesAsync();
                    return View(viewModel);
            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = name });

            }
        }
    }
}
