using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyGroup.Data;
using StudyGroup.Models;
using StudyGroup.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyGroup.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ViewGroups(string Err)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var user = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value);
            var GroupsUserHas = _context.UserXGroups.Where(u => u.UserId == user.Id);
            var OtherGroups = _context.Groups.Where(g => GroupsUserHas.All(groupUser => groupUser.GroupId != g.Id));
            foreach(var group in OtherGroups)
            {
                group.User = _context.AppUsers.FirstOrDefault(a => a.Id == group.GroupAdmin);
            }
                if (Err == "B")
                {
                    ViewGroupsViewModel viewModel = new ViewGroupsViewModel
                    {
                        Groups = OtherGroups,
                        ErrorMessage = "You have been Blocked from this group"
                    };
                    return View(viewModel);
                }
                else if (Err == "E")
                {
                    ViewGroupsViewModel viewModel = new ViewGroupsViewModel
                    {
                        Groups = OtherGroups,
                        ErrorMessage = "This Group is locked. sorry"
                    };
                    return View(viewModel);
                }
            else
            {
                ViewGroupsViewModel viewModel = new ViewGroupsViewModel
                {
                    Groups = OtherGroups,
                    ErrorMessage = null
                };
                return View(viewModel);
            }
        }

        public IActionResult Create(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (group != null)
            {
                GroupFormViewModel viewModel = new GroupFormViewModel
                {
                    Group = group,
                    ErrorMessage = null,
                    CurrentUser = claim.Value
                };
                return View(viewModel);
            }
            else
            {
                GroupFormViewModel viewModel = new GroupFormViewModel
                {
                    Group = null,
                    ErrorMessage = null
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
            var GroupExisting = _context.Groups.FirstOrDefault(g => g.Name == group.Name);
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
            if (GroupExisting == null)
            {
                group.GroupAdmin = claim.Value;
                group.isClosed = false;
                group.VideoCall = false;
                group.VoiceCall = false;
                group.NumberOfUsers = 1;
                group.DateCreated = DateTime.Now;

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].ContentType.Substring(0, 5) == "image")
                {

                        byte[] file = null;
                        using (var fs1 = files[0].OpenReadStream())
                        {
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                file = ms1.ToArray();
                            }
                        }
                        group.GroupImage = file;
                        group.ImageExtension = files[0].FileName.Split(".")[1].ToString();


                    _context.Groups.Add(group);
                    await _context.SaveChangesAsync();

                    UserXGroups grouping = new UserXGroups
                    {
                        Group = _context.Groups.FirstOrDefault(g => g.Id == group.Id),
                        GroupId = _context.Groups.FirstOrDefault(g => g.Id == group.Id).Id,
                        User = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value),
                        UserId = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value).Id
                    };
                    _context.UserXGroups.Add(grouping);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ViewGroup", "Group", new { name = group.Name });
                }
                else
                {
                    GroupFormViewModel viewModel = new GroupFormViewModel
                    {
                        Group = group,
                        ErrorMessage = "You Have To Input a Group Image. Make sure it is in an Image Format and Try Again"
                    };
                    return View(viewModel);
                }
            }
            else
            {
                if (GroupExisting.GroupAdmin == claim.Value)
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        if (files[0].ContentType.Substring(0, 5) == "image")
                        {

                            byte[] file = null;
                            using (var fs1 = files[0].OpenReadStream())
                            {
                                using (var ms1 = new MemoryStream())
                                {
                                    fs1.CopyTo(ms1);
                                    file = ms1.ToArray();
                                }
                            }
                            GroupExisting.GroupImage = file;
                            GroupExisting.ImageExtension = files[0].FileName.Split(".")[1].ToString();
                        }
                        else
                        {
                            GroupFormViewModel viewModel = new GroupFormViewModel
                            {
                                Group = GroupExisting,
                                ErrorMessage = "The New GroupImage Is Not In the Right Format. Try Inputing an Image"
                            };
                            return View(viewModel);
                        }
                    }
                    else
                    {
                        if (GroupExisting.Name == group.Name &&
                        GroupExisting.Goal == group.Goal &&
                        GroupExisting.Description == group.Description)
                        {
                            GroupFormViewModel viewModel = new GroupFormViewModel
                            {
                                Group = GroupExisting,
                                CurrentUser = claim.Value,
                                ErrorMessage = "You haven't made any changes"
                            };
                            return View(viewModel);
                        }
                        
                    }
                    GroupExisting.Name = group.Name;
                    GroupExisting.Goal = group.Goal;
                    GroupExisting.Description = group.Description;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("ViewGroup", "Group", new { name = group.Name });
                }
                else
                {
                    GroupFormViewModel viewModel = new GroupFormViewModel
                    {
                        Group = group,
                        ErrorMessage = "Group with name" + group.Name + " Already Exists"
                    };
                    return View(viewModel);

                }
            }
        }

        [Route("Group/ViewGroup/{name}")]
        public IActionResult ViewGroup (string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            if (group != null)
            {
                var GroupsUsers = _context.UserXGroups.Where(u => u.GroupId == group.Id);
                var GroupMembers = _context.AppUsers.Where(a => GroupsUsers.Any(users => users.UserId == a.Id));

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if(GroupMembers.Contains(_context.AppUsers.FirstOrDefault(a=>a.Id == claim.Value)))
                {
                    var BlockedUsers = _context.BlockedUsers.Where(b => b.GroupId == group.Id);
                    ViewAGroupViewModel viewModel = new ViewAGroupViewModel
                    {
                        Group = group,
                        GroupMembers = GroupMembers,
                        BlockedUsers = _context.AppUsers.Where(a => BlockedUsers.Any(users => users.UserId == a.Id)),
                        CurrentUser = claim.Value
                    };
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Users");
                }

            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }

        public async Task<IActionResult> JoinGroup(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null && group.isClosed == false)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                //check if user is blocked 
                var BlockedUser = _context.BlockedUsers.Where(b => b.GroupId == group.Id).FirstOrDefault(b => b.UserId == claim.Value);

                if(BlockedUser== null)
                {
                    UserXGroups grouping = new UserXGroups
                    {
                        Group = group,
                        GroupId = group.Id,
                        User = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value),
                        UserId = _context.AppUsers.FirstOrDefault(a => a.Id == claim.Value).Id
                    };
                    _context.UserXGroups.Add(grouping);
                    group.NumberOfUsers += 1;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ViewGroup", "Group", new { name = group.Name });
                }
                return RedirectToAction("ViewGroups", new { Err = "B"});
            }
            else
            {
                if(group.isClosed == true)
                {
                    return RedirectToAction("ViewGroups", new { Err = "E" });
                }
                return RedirectToAction("ViewGroups");
            }
        }

        [Route("Group/BlockUser/{username}/{groupName}")]
        public async Task<IActionResult> BlockUser(string username, string groupName)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == groupName);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(group.GroupAdmin == claim.Value)
            {
                var userToBlock = _context.AppUsers.FirstOrDefault(a => a.UserName == username);
                //ONE- remove their relationship with the group
                var userXGroup = _context.UserXGroups.Where(u => u.GroupId == group.Id).FirstOrDefault(u => u.UserId == userToBlock.Id);
                _context.UserXGroups.Remove(userXGroup);

                //TWO- reduce the number of users the group has
                group.NumberOfUsers -= 1;

                //THREE -Add the Relationship to Blocked Users
                var BlockedUser = new BlockedUsers
                {
                    GroupId = group.Id,
                    UserId = userToBlock.Id,
                    User = userToBlock,
                    Group = group
                };
                _context.BlockedUsers.Add(BlockedUser);

                //FOUR -SAVE ALL THE CHANGES
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewGroup", "Group", new { name = group.Name });

            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = group.Name });

            }

        }

        [Route("Group/UnBlockUser/{username}/{groupName}")]
        public async Task<IActionResult> UnBlockUser(string username, string groupName)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == groupName);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (group.GroupAdmin == claim.Value)
            {
                var userToBlock = _context.AppUsers.FirstOrDefault(a => a.UserName == username);
                //ONE- remove their Blocked relationship with the group
                var BlockedRelation = _context.BlockedUsers.Where(b => b.GroupId == group.Id).FirstOrDefault(b => b.UserId == userToBlock.Id);
                _context.BlockedUsers.Remove(BlockedRelation);

                //TWO- increase the number of users the group has
                group.NumberOfUsers += 1;

                //THREE -Add the Relationship to Group Users
                var GroupMember = new UserXGroups
                {
                    GroupId = group.Id,
                    UserId = userToBlock.Id,
                    User = userToBlock,
                    Group = group
                };
                _context.UserXGroups.Add(GroupMember);

                //FOUR -SAVE ALL THE CHANGES
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewGroup", "Group", new { name = group.Name });

            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = group.Name });

            }

        }

        public async Task<IActionResult> Leave(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            if(group!= null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                //ONE - remove the relationship with the group

                var userXGroup = _context.UserXGroups.Where(g => g.GroupId == group.Id).FirstOrDefault(u => u.UserId == claim.Value);
                _context.UserXGroups.Remove(userXGroup);

                //Reduce number of Users in group
                group.NumberOfUsers -= 1;

                //Save Changes 
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = name });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Lock(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);
            if (group != null)
            {
                //ONE-- Lock the room
                group.isClosed = !(group.isClosed);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewGroup", "Group", new { name = name });
            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = name });
            }
        }

        public async Task<IActionResult> Delete(string name)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Name == name);

            if (group != null)
            {
                //Remove all relationships with members
                var userXGroup = _context.UserXGroups.Where(u => u.GroupId == group.Id);
                _context.UserXGroups.RemoveRange(userXGroup);

                //remove all relationships with blocked members
                var blockedUsers = _context.BlockedUsers.Where(b => b.GroupId == group.Id);
                _context.BlockedUsers.RemoveRange(blockedUsers);

                //remove all documents of the group
                var groupDoc = _context.Documents.Where(d => d.GroupId == group.Id);
                _context.Documents.RemoveRange(groupDoc);

                //remove all reminders of the group
                var groupRem = _context.Reminders.Where(r => r.GroupId == group.Id);
                _context.Reminders.RemoveRange(groupRem);

                //remove all textmessages of the group
                var groupTexts = _context.Text.Where(t => t.GroupId == group.Id);
                _context.Text.RemoveRange(groupTexts);

                //remove the group
                _context.Groups.Remove(group);

                //Save all Changes
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("ViewGroup", "Group", new { name = name });
            }
        }

        //public IActionResult Edit(string name)
        //{
        //    var group = _context.Groups.FirstOrDefault(g => g.Name == name);
        //    if(group != null)
        //    {
        //        Group theGroup = new Group
        //        {
        //            Id = group.Id,
        //            Name = group.Name,
        //            Goal = group.Goal,
        //            Description = group.Description,
        //            GroupImage = group.GroupImage,
        //            GroupAdmin = group.GroupAdmin
        //        };
        //        return View(theGroup);
        //    }
        //    else
        //    {
        //        return RedirectToAction("ViewGroup", "Group", new { name = name });
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Group group)
        //{

        //}
    }
}
