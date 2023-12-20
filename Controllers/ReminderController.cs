using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    public class ReminderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ReminderController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [Route("{controller}/{action}/{name?}")]
        public IActionResult Create(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                CreateReminderViewModel viewModel = new CreateReminderViewModel
                {
                    GroupId = 0
                };
                return View(viewModel);
            }
            else
            {
                var group = _context.Groups.FirstOrDefault(g => g.Name == name);
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                Group remGroup = new Group
                {
                    Id = group.Id,
                    Name = group.Name,
                    GroupAdmin = group.GroupAdmin,
                    GroupImage = group.GroupImage
                };
                CreateReminderViewModel viewModel = new CreateReminderViewModel
                {
                    GroupId =group.Id,
                    CurrentUser = claim.Value,
                    Group = remGroup
                };
                return View(viewModel);
            }
        }

        public IActionResult Edit(int id)
        {
            var reminder = _context.Reminders.FirstOrDefault(r => r.Id == id);
            var group = _context.Groups.FirstOrDefault(g => g.Id == reminder.GroupId);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(reminder!= null)
            {
                ReminderDetailsViewModel viewModel = new ReminderDetailsViewModel
                {
                    Reminder = reminder,
                    ErrorMessage = null,
                    Group = group,
                    CurrentUser = claim.Value
                };
                    return View(viewModel);
                
            }
            return RedirectToAction("Index", "Users", new { filter = "_Reminders" });
        }

        [HttpPost]
        public async Task<IActionResult> EditReminder(ReminderDetailsViewModel vm)
        {
            Reminder EdittedReminder = vm.Reminder;
            var reminder = _context.Reminders.FirstOrDefault(r => r.Id == EdittedReminder.Id);
            var groupR = _context.Groups.FirstOrDefault(g => g.Id == reminder.GroupId);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (reminder.Subject == EdittedReminder.Subject &&
                        reminder.Message == EdittedReminder.Message &&
                        reminder.TimeToBeSent == EdittedReminder.TimeToBeSent)
            {
                ReminderDetailsViewModel viewModel = new ReminderDetailsViewModel
                {
                    Reminder = EdittedReminder,
                    ErrorMessage = "You have made no Changes",
                    Group = groupR,
                    CurrentUser = claim.Value
                };
                return View(nameof(Edit), viewModel);
            }
            if(EdittedReminder.TimeToBeSent <= DateTime.Now)
            {
                ReminderDetailsViewModel viewModel = new ReminderDetailsViewModel
                {
                    Reminder = EdittedReminder,
                    ErrorMessage = "Time has passed already",
                    Group = groupR,
                    CurrentUser = claim.Value
                };
                return View(nameof(Edit), viewModel);
            }
            if (reminder!= null && (reminder.TimeToBeSent >= DateTime.Now))
            {
                if (reminder.UniqueTag != null && (reminder.GroupId != 0))
                {
                    // for a group
                    
                    var group = _context.Groups.FirstOrDefault(g => g.Id == reminder.GroupId);
                    if (group.GroupAdmin == claim.Value)
                    {
                        

                        var groupReminders = _context.Reminders.Where(r => r.GroupId == group.Id)
                                                                .Where(r => r.UniqueTag == reminder.UniqueTag);
                        foreach(var rem in groupReminders)
                        {
                            rem.Subject = EdittedReminder.Subject;
                            rem.Message = EdittedReminder.Message;
                            rem.TimeToBeSent = EdittedReminder.TimeToBeSent;
                        };
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(ViewAll), new { name = group.Name });
                    }
                    else
                    {
                        return View(nameof(Edit), reminder.Id);
                    }
                }
                else
                {
                    reminder.Subject = EdittedReminder.Subject;
                    reminder.Message = EdittedReminder.Message;
                    reminder.TimeToBeSent = EdittedReminder.TimeToBeSent;
                   await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Users", new { filter = "_Reminders" });
                }
            }
            else
            {
                return RedirectToAction(nameof(Edit), EdittedReminder.Id);
            }
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var reminder = _context.Reminders.FirstOrDefault(r => r.Id == id);
            if(reminder!= null)
            {
                if(reminder.UniqueTag != null)
                {
                    var group = _context.Groups.FirstOrDefault(g => g.Id == reminder.GroupId);
                    var reminderList = _context.Reminders.Where(r => r.UniqueTag == reminder.UniqueTag);
                    _context.Reminders.RemoveRange(reminderList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ViewAll), new { name = group.Name });
                }
                else
                {
                    _context.Reminders.Remove(reminder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Users", new { filter = "_Reminders" });
                }
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult ViewAll(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            if (group != null)
            {
                var uniqueTags = _context.Reminders.Where(r => r.GroupId == group.Id).Select(r=> r.UniqueTag).Distinct();
                List<Reminder> rem = new List<Reminder>();
                foreach(var tag in uniqueTags)
                {
                    var first = _context.Reminders.Where(r => r.UniqueTag == tag).FirstOrDefault();
                    if (first != null)
                    {
                        rem.Add(first);
                    }
                }

                //var categorizedReminders = uniqueTags.GroupBy(c => c.UniqueTag).FirstOrDefault();

                Group remGroup = new Group
                {
                    GroupAdmin = group.GroupAdmin,
                    Id = group.Id,
                    Name = group.Name,
                    GroupImage = group.GroupImage
                };

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                RemindersListViewModel viewModel = new RemindersListViewModel
                {
                    GroupAdminID = group.GroupAdmin,
                    CurrentUser = claim.Value,
                    Reminders = rem,
                    Group = remGroup
                };
                return View(viewModel);
               
            }
            return RedirectToAction("ViewGroup", "Group", new { name = name });
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder(CreateReminderViewModel Createdreminder)
        {
            var reminder = Createdreminder.Reminder;
            reminder.GroupId = Createdreminder.GroupId;
            if (reminder.GroupId == 0)
            {
                var claimsidentity = (ClaimsIdentity)User.Identity;
                var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

                reminder.SenderId = claim.Value;
                reminder.ReceiverId = claim.Value;
                reminder.IsSent = false;
                reminder.DateCreated = DateTime.Now;
                reminder.GroupId = null;
                _context.Reminders.Add(reminder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Users", new { filter = "_Reminders"});
            }
            else
            {
                var group = _context.Groups.FirstOrDefault(g => g.Id == Createdreminder.GroupId);
                var groupName = _context.Groups.FirstOrDefault(g => g.Id == Createdreminder.GroupId).Name;
                
                    var GroupsUsers = _context.UserXGroups.Where(u => u.GroupId == Createdreminder.GroupId);
                    var GroupMembers = _context.AppUsers.Where(a => GroupsUsers.Any(users => users.UserId == a.Id));
                    List<Reminder> reminders = new List<Reminder>();
                string uniqueTag = Guid.NewGuid().ToString() + DateTime.Now.ToString();
                    //var uniqueTag = Createdreminder.UniqueTag + DateTime.Now.ToString()+ group.GroupAdmin;
                    foreach (var member in GroupMembers)
                    {
                        var reminderEach = new Reminder();
                        reminderEach.Subject = Createdreminder.Reminder.Subject;
                        reminderEach.Message = Createdreminder.Reminder.Message;
                        reminderEach.TimeToBeSent = Createdreminder.Reminder.TimeToBeSent;
                        reminderEach.GroupId = Createdreminder.GroupId;
                        reminderEach.DateCreated = DateTime.Now;
                        reminderEach.SenderId = group.GroupAdmin;
                        reminderEach.ReceiverId = member.Id;
                        reminderEach.IsSent = false;
                        reminderEach.Receiver = member;
                        reminderEach.UniqueTag = uniqueTag;
                        reminders.Add(reminderEach);
                    }

                    foreach (var rem in reminders)
                    {
                        _context.Reminders.Add(rem);
                    }
                    await _context.SaveChangesAsync();
                    

                    return RedirectToAction("ViewAll", "Reminder", new { name = groupName });
                
            }

        }

        //Trigger only once oh!

        //[Route("[Action]")]
        //public IActionResult SendEmails()
        //{
        //    RecurringJob.AddOrUpdate(() => SendEmailEvery2Minutes(), "*/2 * * * * ");
        //    return Ok("SendEmails Triggered");
        //}

        public async Task<IActionResult> SendEmailEvery2Minutes()
        {
            var unsentReminders = _context.Reminders.Where(r => r.IsSent == false);
            List<Reminder> lessThan2MinuteReminders = new List<Reminder>();
            foreach(var reminder in unsentReminders)
            {
                if((reminder.TimeToBeSent - DateTime.Now)<= TimeSpan.FromMinutes(2))
                {
                    lessThan2MinuteReminders.Add(reminder);
                }
            }
            foreach(var reminder in lessThan2MinuteReminders)
            {
                if(reminder != null)
                {
                    reminder.Receiver = _context.AppUsers.FirstOrDefault(a => a.Id == reminder.ReceiverId);
                    if(reminder.Subject == null)
                    {
                        reminder.Subject = " ";
                    }
                    await _emailSender.SendEmailAsync(reminder.Receiver.Email, reminder.Subject, reminder.Message);
                    reminder.IsSent = true;
                    await _context.SaveChangesAsync();
                }
            }
            return Ok();
        }
    }
}
